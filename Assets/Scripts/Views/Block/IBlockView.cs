using Arcanoid.Models;

namespace Arcanoid.Views
{
    /// <summary>
    /// Интерфейс представления блоков.
    /// </summary>
    public interface IBlockView
    {
        #region Public Methods
        /// <summary>
        /// Получить информацию для расчета колизии.
        /// </summary>
        /// <returns></returns>
        Block GetBlockInfo();

        /// <summary>
        /// Удар о блок шариком.
        /// </summary>
        void Strike();

        /// <summary>
        /// Жив ли блок?
        /// </summary>
        bool IsLive();

        #endregion Public Methods
    }
}