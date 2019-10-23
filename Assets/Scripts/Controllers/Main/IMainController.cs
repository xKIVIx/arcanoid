namespace Arcanoid.Controllers
{
    /// <summary>
    /// Интерфейс главного контроллера.
    /// </summary>
    public interface IMainController
    {
        #region Public Methods

        /// <summary>
        /// Обработка обновления окружения.
        /// </summary>
        void OnFixedUpdate();

        #endregion Public Methods
    }
}