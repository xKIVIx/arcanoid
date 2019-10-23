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

        /// <summary>
        /// Скорость перемещения плитки
        /// </summary>
        float Speed { get; set; }

        /// <summary>
        /// Пространство в котором может перемещаться плитка.
        /// </summary>
        Bounds Workspace { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Получить параметры блока.
        /// </summary>
        /// <returns></returns>
        Block GetBlock();

        /// <summary>
        /// Сместить плитку влево.
        /// </summary>
        void MoveLeft();

        /// <summary>
        /// Сместить плитку вправо.
        /// </summary>
        void MoveRight();

        #endregion Public Methods
    }
}