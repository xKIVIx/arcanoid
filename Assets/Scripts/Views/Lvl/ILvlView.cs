namespace Arcanoid.Views
{
    /// <summary>
    /// Интерфейс для представлений уровней.
    /// </summary>
    public interface ILvlView
    {
        #region Public Properties

        /// <summary>
        /// Блоки на уровне.
        /// </summary>
        IBlockView[] Blocks { get; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Закрыть уровень.
        /// </summary>
        void Close();

        #endregion Public Methods
    }
}