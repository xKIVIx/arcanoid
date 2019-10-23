﻿using UnityEngine;

namespace Arcanoid.Views
{
    /// <summary>
    /// Интерфейс представления шарика.
    /// </summary>
    public interface IBallView
    {
        #region Public Methods

        /// <summary>
        /// Получить центр шарика.
        /// </summary>
        /// <returns></returns>
        Vector2 GetCenter();

        /// <summary>
        /// Переместить в направлении вектора.
        /// </summary>
        /// <param name="moveVector"></param>
        void Move(Vector2 moveVector);

        /// <summary>
        /// Удалить шарик.
        /// </summary>
        void Remove();

        #endregion Public Methods
    }
}