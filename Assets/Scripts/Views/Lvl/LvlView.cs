using UnityEngine;

namespace Arcanoid.Views
{
    /// <summary>
    /// Основная реализация представления уровня.
    /// </summary>
    public class LvlView : MonoBehaviour, ILvlView
    {
        #region Private Fields

        /// <summary>
        /// Закрыт ли уже уровень?
        /// </summary>
        private bool _isClosed;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// <see cref="ILvlView.Blocks"/>
        /// </summary>
        public IBlockView[] Blocks { get; private set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// <see cref="ILvlView.Close"/>
        /// </summary>
        public void Close()
        {
            if (!_isClosed)
            {
                Destroy(gameObject);
                _isClosed = true;
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void Awake()
        {
            Blocks = transform.GetComponentsInChildren<BlockView>();
            _isClosed = false;
        }

        #endregion Private Methods
    }
}