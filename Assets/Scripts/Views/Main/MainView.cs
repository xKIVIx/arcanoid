using System;
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

        /// <summary>
        /// Для управления отображения туториала для моб. устр.
        /// </summary>
        [SerializeField]
        private GameObject _mobileTutorial;

        /// <summary>
        /// Для управления отображением туториала для пк.
        /// </summary>
        [SerializeField]
        private GameObject _pcTutorial;

        #endregion Private Fields

        #region Public Events

        public event Action OnStartGame;

        public event Action OnStopnGame;

        #endregion Public Events

        #region Public Properties

        /// <summary>
        /// <see cref="IMainView.GameFieldView"/>
        /// </summary>
        public IGameFieldView GameFieldView => _gameField;

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Запуск игры
        /// </summary>
        public void StartGame()
        {
            OnStartGame();
        }

        /// <summary>
        /// Остановка игры
        /// </summary>
        public void StopGame()
        {
            OnStopnGame();
        }

        #endregion Public Methods

        #region Private Methods

        private void Awake()
        {
#if UNITY_ANDROID
            _mobileTutorial.SetActive(true);
            _pcTutorial.SetActive(false);
#else
            _mobileTutorial.SetActive(false);
            _pcTutorial.SetActive(true);
#endif
        }

        #endregion Private Methods
    }
}