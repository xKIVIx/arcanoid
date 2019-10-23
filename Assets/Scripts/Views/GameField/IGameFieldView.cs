using Arcanoid.Models;
using UnityEngine;

namespace Arcanoid.Views
{
    /// <summary>
    /// Интерфейс для представления игрового поля
    /// </summary>
    public interface IGameFieldView
    {
        #region Public Properties

        /// <summary>
        /// Данные для расчета колизии с границами поля.
        /// </summary>
        Block FieldBlock { get; }

        /// <summary>
        /// Интерфейс представления плитки игрока.
        /// </summary>
        IUserSlideView UserSlideView { get; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Добавление нового шара.
        /// </summary>
        /// <param name="position">Позиция появления шара</param>
        /// <returns></returns>
        IBallView AddBall(Vector2 position);

        /// <summary>
        /// Добавление нового шара. Шар появляется на начальной позиции.
        /// </summary>
        /// <returns></returns>
        IBallView AddBall();

        #endregion Public Methods
    }
}