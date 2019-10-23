using Arcanoid.Models;
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
        /// Плитка игрока.
        /// </summary>
        [SerializeField]
        private UserSlideView _userSlide;

        #endregion Private Fields

        #region Public Properties

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

        #endregion Public Methods

        #region Private Methods

        private void Awake()
        {
            var colider = GetComponent<BoxCollider2D>();
            FieldBlock = new Block(colider.bounds);
        }

        #endregion Private Methods
    }
}