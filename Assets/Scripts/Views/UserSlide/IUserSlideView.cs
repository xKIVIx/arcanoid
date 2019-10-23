using Arcanoid.Models;
using UnityEngine;

namespace Arcanoid.Views
{
    /// <summary>
    /// Интерфейс для управления отражающим кирпичом.
    /// </summary>
    public interface IUserSlideView
    {
        #region Public Properties

        /// <summary>
        /// Количество бонусов на увелечение размера.
        /// </summary>
        uint CountSizeBonus { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Получить параметры блока.
        /// </summary>
        /// <returns></returns>
        Block GetBlock();

        /// <summary>
        /// Сместить плитку.
        /// </summary>
        /// <param name="moveVector">
        /// Вектор перемещения.
        /// </param>
        void Move(Vector2 moveVector);

        #endregion Public Methods
    }
}