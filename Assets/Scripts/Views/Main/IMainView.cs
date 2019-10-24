using System;

namespace Arcanoid.Views
{
    /// <summary>
    /// Интерфейс главного представления для взамодействия с объектами сцены.
    /// </summary>
    public interface IMainView
    {
        #region Public Events

        /// <summary>
        /// Запуск игры
        /// </summary>
        event Action OnStartGame;

        /// <summary>
        /// Остановка игры.
        /// </summary>
        event Action OnStopnGame;

        #endregion Public Events

        #region Public Properties

        /// <summary>
        /// Представление игрового поля.
        /// </summary>
        IGameFieldView GameFieldView { get; }

        #endregion Public Properties
    }
}