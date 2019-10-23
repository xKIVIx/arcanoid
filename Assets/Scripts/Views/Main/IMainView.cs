namespace Arcanoid.Views
{
    /// <summary>
    /// Интерфейс главного представления для взамодействия с объектами сцены.
    /// </summary>
    public interface IMainView
    {
        #region Public Properties

        /// <summary>
        /// Представление игрового поля.
        /// </summary>
        IGameFieldView GameFieldView { get; }

        #endregion Public Properties
    }
}