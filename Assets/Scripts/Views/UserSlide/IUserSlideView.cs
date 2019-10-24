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
        /// Величина изменения размера за счет бонусов.
        /// </summary>
        float SizeBonus { get; set; }

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

        /// <summary>
        /// Вернуть плитку в изначальное состояние.
        /// </summary>
        void ResetState();

        #endregion Public Methods
    }
}