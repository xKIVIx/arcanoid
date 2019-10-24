using Arcanoid.Models;
using System;
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
        /// Начальная позиция плитки.
        /// </summary>
        private Vector3 _startPosition;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// <see cref="IUserSlideView.CountSizeBonus"/>
        /// </summary>
        public uint CountSizeBonus { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
        }

        #endregion Private Methods
    }
}