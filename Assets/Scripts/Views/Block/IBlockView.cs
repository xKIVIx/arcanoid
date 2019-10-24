using Arcanoid.Models;

namespace Arcanoid.Views
{
    /// <summary>
    /// Интерфейс представления блоков.
    /// </summary>
    public interface IBlockView
    {
        #region Public Properties

        /// <summary>
        /// Параметры содержащегося бонуса.
        /// </summary>
        Bonus Bonus { get; }

        /// <summary>
        /// Есть ли бонус в блоке.
        /// </summary>
        bool IsHasBonus { get; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Получить информацию для расчета колизии.
        /// </summary>
        /// <returns></returns>
        Block GetBlockInfo();

        /// <summary>
        /// Жив ли блок?
        /// </summary>
        bool IsLive();

        /// <summary>
        /// Удар о блок шариком.
        /// </summary>
        void Strike();

        #endregion Public Methods
    }
}