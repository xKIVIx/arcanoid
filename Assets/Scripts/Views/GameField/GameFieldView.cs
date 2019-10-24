﻿using Arcanoid.Models;
using UnityEngine;

namespace Arcanoid.Views
{
    /// <summary>
    /// Основная реализация представления игрового поля.
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    public class GameFieldView : MonoBehaviour, IGameFieldView
    {
        #region Private Fields

        /// <summary>
        /// префаб игрового шарика.
        /// </summary>
        [SerializeField]
        private BallView _ballPrefab;

        /// <summary>
        /// Точка появления первого шара.
        /// </summary>
        [SerializeField]
        private Transform _ballSpawn;

        /// <summary>
        /// Доступные уровни.
        /// </summary>
        [SerializeField]
        private LvlView[] _lvls;

        /// <summary>
        /// Плитка игрока.
        /// </summary>
        [SerializeField]
        private UserSlideView _userSlide;

        /// <summary>
        /// Номер следующего уровня.
        /// </summary>
        private int _nextLvlId;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// <see cref="IGameFieldView.CurrentLvl"/>
        /// </summary>
        public ILvlView CurrentLvl { get; private set; }
        /// <summary>
        /// <see cref="IGameFieldView.FieldBlock"/>
        /// </summary>
        public Block FieldBlock { get; private set; }

        /// <summary>
        /// <see cref="IGameFieldView.UserSlideView"/>
        /// </summary>
        public IUserSlideView UserSlideView => _userSlide;

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// <see cref="IGameFieldView.AddBall(Vector2)"/>
        /// </summary>
        public IBallView AddBall(Vector2 position)
        {
            return Instantiate(_ballPrefab, position, new Quaternion(), this.transform);
        }

        /// <summary>
        /// <see cref="IGameFieldView.AddBall"/>
        /// </summary>
        public IBallView AddBall()
        {
            return Instantiate(_ballPrefab, _ballSpawn.position, new Quaternion(), this.transform);
        }

        /// <summary>
        /// <see cref="IGameFieldView.NextLvl"/>
        /// </summary>
        public void NextLvl()
        {
            CurrentLvl?.Close();
            CurrentLvl = Instantiate(_lvls[_nextLvlId]);
            _nextLvlId = (_nextLvlId + 1) % _lvls.Length;
        }

        #endregion Public Methods

        #region Private Methods

        private void Awake()
        {
            var colider = GetComponent<BoxCollider2D>();
            FieldBlock = new Block(colider.bounds);
            _nextLvlId = 0;
            NextLvl();
        }

        #endregion Private Methods
    }
}