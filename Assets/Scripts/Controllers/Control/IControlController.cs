namespace Arcanoid.Controllers
{
    /// <summary>
    /// Интерфейс для контроллера управления.
    /// </summary>
    public interface IControlController
    {
        #region Public Methods

        /// <summary>
        /// Проверяет было ли нажатие начала игры в промежутке между этим и прошлым вызовом данной функции.
        /// </summary>
        /// <returns></returns>
        bool CheckPressStartGame();

        /// <summary>
        /// Обработка действий пользователя.
        /// </summary>
        void HandleUserAction();

        #endregion Public Methods
    }
}