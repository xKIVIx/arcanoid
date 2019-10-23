namespace Arcanoid.Controllers
{
    /// <summary>
    /// Интерфейс для контроллера управления.
    /// </summary>
    public interface IControlController
    {
        #region Public Methods

        /// <summary>
        /// Обработка действий пользователя.
        /// </summary>
        void HandleUserAction();

        #endregion Public Methods
    }
}