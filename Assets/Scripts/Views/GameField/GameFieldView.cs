using Arcanoid.Models;
using System.Collections.Generic;
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
        /// префаб бонуса.
        /// </summary>
        [SerializeField]
        private BonusView _bonusPrefab;

        /// <summary>
        /// Доступные уровни.
        /// </summary>
        [SerializeField]
        private LvlView[] _lvls;

        /// <summary>
        /// Номер следующего уровня.
        /// </summary>
        private int _nextLvlId;

        /// <summary>
        /// Точка появления плитки игрока.
        /// </summary>
        [SerializeField]
        private Transform _slideSpawn;

        /// <summary>
        /// Плитка игрока.
        /// </summary>
        [SerializeField]
        private UserSlideView _userSlidePrefab;

        #endregion Private Fields

        #region Public Properties

        public List<IBallView> Balls { get; } = new List<IBallView>();

        public List<IBonusView> Bonuses { get; } = new List<IBonusView>();

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
        public IUserSlideView UserSlideView { get; private set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// <see cref="IGameFieldView.AddBall(Vector2)"/>
        /// </summary>
        public IBallView AddBall(Vector2 position)
        {
            var ball = Instantiate(_ballPrefab, position, new Quaternion(), this.transform);
            Balls.Add(ball);
            return ball;
        }

        /// <summary>
        /// <see cref="IGameFieldView.AddBall"/>
        /// </summary>
        public IBallView AddBall()
        {
            return AddBall(_ballSpawn.position);
        }

        /// <summary>
        /// <see cref="IGameFieldView.AddBonus(Bonus, Vector2)"/>
        /// </summary>
        public IBonusView AddBonus(Bonus bonusInfo, Vector2 position)
        {
            var bonusView = Instantiate(_bonusPrefab, position, new Quaternion(), this.transform);
            bonusView.BonusParametrs = bonusInfo;
            Bonuses.Add(bonusView);
            return bonusView;
        }

        /// <summary>
        /// Загрузить выбранный уровень.
        /// </summary>
        /// <param name="idLvl"></param>
        public void LoabLvl(int idLvl)
        {
            if (idLvl >= 0 && idLvl < _lvls.Length)
            {
                _nextLvlId = idLvl;
            }

            NextLvl();
        }

        /// <summary>
        /// <see cref="IGameFieldView.NextLvl"/>
        /// </summary>
        public void NextLvl()
        {
            CurrentLvl?.Close();
            ResetStates();
            CurrentLvl = Instantiate(_lvls[_nextLvlId], this.transform);
            _nextLvlId = (_nextLvlId + 1) % _lvls.Length;
        }

        /// <summary>
        /// <see cref="IGameFieldView.Restart"/>
        /// </summary>
        public void Restart()
        {
            _nextLvlId--;
            if (_nextLvlId < 0)
            {
                _nextLvlId = _lvls.Length - 1;
            }

            NextLvl();
        }

        #endregion Public Methods

        #region Private Methods

        private void Awake()
        {
            var colider = GetComponent<BoxCollider2D>();
            FieldBlock = new Block(colider.bounds);
            _nextLvlId = 0;
            UserSlideView = Instantiate(_userSlidePrefab, _slideSpawn.position, new Quaternion(), this.transform);
            NextLvl();
        }

        private void ResetStates()
        {
            foreach (var ball in Balls)
            {
                ball.Remove();
            }
            Balls.Clear();
            AddBall();
            UserSlideView.ResetState();
        }

        #endregion Private Methods
    }
}