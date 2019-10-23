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
        /// Удалить блок.
        /// </summary>
        void Remove();

        #endregion Public Methods
    }
}