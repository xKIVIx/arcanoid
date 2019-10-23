namespace Arcanoid.Controllers
{
    public interface IGameController
    {
        #region Public Properties

        /// <summary>
        /// Признак, что игра запущена.
        /// </summary>
        bool IsStartGame { get; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Обновление состояний объектов.
        /// </summary>
        void OnFixedUpdate();

        /// <summary>
        /// Запуск шарика. Начало игры.
        /// </summary>
        void StartGame();

        #endregion Public Methods
    }
}