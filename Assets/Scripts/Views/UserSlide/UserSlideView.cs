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

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Иницилизация.
        /// </summary>
        private void Start()
        {
            _colider = GetComponent<BoxCollider2D>();
        }

        #endregion Private Methods
    }
}