using Arcanoid.Models;
using UnityEngine;

namespace Arcanoid.Views
{
    /// <summary>
    /// Основная реализация компонета управления плиткой игрока.
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    internal class UserSlideView : MonoBehaviour, IUserSlideView
    {
        #region Private Fields

        /// <summary>
        /// Собвстевный колайдер плитки.
        /// </summary>
        private BoxCollider2D _colider;

        /// <summary>
        /// Бонусный размер
        /// </summary>
        private float _sizeBonus;

        /// <summary>
        /// Начальная позиция плитки.
        /// </summary>
        private Vector3 _startPosition;

        /// <summary>
        /// Изначальный размер
        /// </summary>
        private Vector3 _startSize;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// <see cref="IUserSlideView.SizeBonus"/>
        /// </summary>
        public float SizeBonus
        {
            get => _sizeBonus;
            set
            {
                _sizeBonus = value;
                transform.localScale = _startSize + new Vector3(_sizeBonus, 0, 0);
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// <see cref="IUserSlideView.GetBlock"/>
        /// </summary>
        /// <returns></returns>
        public Block GetBlock()
        {
            return new Block(_colider.bounds);
        }

        /// <summary>
        /// <see cref="IUserSlideView.Move(Vector2)"/>
        /// </summary>
        /// <returns></returns>
        public void Move(Vector2 moveVector)
        {
            transform.position += new Vector3(moveVector.x, moveVector.y, 0.0f);
        }

        /// <summary>
        /// <see cref="IUserSlideView.ResetState"/>
        /// </summary>
        public void ResetState()
        {
            transform.position = _startPosition;
            transform.localScale = _startSize;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Иницилизация.
        /// </summary>
        private void Awake()
        {
            _colider = GetComponent<BoxCollider2D>();
            _startPosition = transform.position;
            _startSize = transform.localScale;
        }

        #endregion Private Methods
    }
}