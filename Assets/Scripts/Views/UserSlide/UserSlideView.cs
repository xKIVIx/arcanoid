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
        /// Рабочая область плитки.
        /// </summary>
        private Bounds _workspace;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// <see cref="IUserSlideView.CountSizeBonus"/>
        /// </summary>
        public uint CountSizeBonus { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// <see cref="IUserSlideView.Speed"/>
        /// </summary>
        public float Speed { get; set; }

        /// <summary>
        /// <see cref="IUserSlideView.Workspace"/>
        /// </summary>
        public Bounds Workspace
        {
            get => _workspace;
            set
            {
                _workspace = value;
                FixOutWorkspace();
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
        /// <see cref="IUserSlideView.MoveLeft"/>
        /// </summary>
        public void MoveLeft()
        {
            transform.TransformDirection(new Vector3(-Speed, 0));
            FixOutWorkspace();
        }

        /// <summary>
        /// <see cref="IUserSlideView.MoveRight"/>
        /// </summary>
        public void MoveRight()
        {
            transform.TransformDirection(new Vector3(Speed, 0));
            FixOutWorkspace();
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Исправление выхода плитки за границы рабочей области.
        /// </summary>
        private void FixOutWorkspace()
        {
            var brickBounds = _colider.bounds;

            var delta = _workspace.min.x - brickBounds.min.x;
            if (delta < 0)
            {
                transform.TransformDirection(new Vector3(delta, 0));
            }

            delta = _workspace.max.x - brickBounds.max.x;
            if (delta < 0)
            {
                transform.TransformDirection(new Vector3(-delta, 0));
            }
        }

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