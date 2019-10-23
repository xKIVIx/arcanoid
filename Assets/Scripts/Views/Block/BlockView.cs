using Arcanoid.Models;
using UnityEngine;

namespace Arcanoid.Views
{
    /// <summary>
    /// Основная реализация разбиваемых блоков.
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    internal class BlockView : MonoBehaviour, IBlocksView
    {
        #region Private Fields

        /// <summary>
        /// Информация о блоке, для расчета колизии.
        /// </summary>
        private Block _block;

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        /// <see cref="IBlocksView.GetBlockInfo"/>
        /// </summary>
        public Block GetBlockInfo()
        {
            return _block;
        }

        // <summary>
        /// <see cref="IBlocksView.Remove"/>
        /// </summary>
        public void Remove()
        {
            Destroy(gameObject);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Иницилизация.
        /// </summary>
        private void Setup()
        {
            var colider = GetComponent<BoxCollider2D>();
            _block = new Block(colider.bounds);
        }

        #endregion Private Methods
    }
}