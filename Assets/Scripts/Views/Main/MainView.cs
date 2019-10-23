using UnityEngine;

namespace Arcanoid.Views
{
    /// <summary>
    /// Основная реализация главного представления.
    /// </summary>
    public class MainView : MonoBehaviour, IMainView
    {
        #region Private Fields

        /// <summary>
        /// Игровое поле.
        /// </summary>
        [SerializeField]
        private GameFieldView _gameField;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// <see cref="IMainView.GameFieldView"/>
        /// </summary>
        public IGameFieldView GameFieldView => _gameField;

        #endregion Public Properties
    }
}