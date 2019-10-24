using UnityEngine;

namespace Arcanoid.Views
{
    public class LvlView : MonoBehaviour, ILvlView
    {
        #region Public Properties

        public IBlockView[] Blocks { get; private set; }

        private bool _isClosed;

        public void Close()
        {
            if (!_isClosed)
            {
                Destroy(gameObject);
                _isClosed = true;
            }
        }

        #endregion Public Properties

        #region Private Methods

        private void Awake()
        {
            Blocks = transform.GetComponentsInChildren<BlockView>();
            _isClosed = false;
        }

        #endregion Private Methods
    }
}