using Arcanoid.Views;

namespace Arcanoid.Controllers
{
    /// <summary>
    /// Основная реализация главного контроллера.
    /// </summary>
    public class MainController : IMainController
    {
        #region Public Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="mainView">Главный представление для взаимодействия с объектами на сцене.</param>
        /// <param name="controlController">Контроллер управления</param>
        /// <param name="gameController">Контроллер основной игровой механики</param>
        public MainController(IMainView mainView,
                              IControlController controlController,
                              IGameController gameController)
        {
            _mainView = mainView;
            _controlController = controlController;
            _gameController = gameController;
            _isStartGame = false;
            _mainView.OnStartGame += () => _isStartGame = true;
            _mainView.OnStopnGame += () =>
                {
                    _gameController.StopGame();
                    _isStartGame = false;
                };
        }

        #endregion Public Constructors

        #region Private Fields

        private IControlController _controlController;
        private IGameController _gameController;
        private bool _isStartGame;
        private IMainView _mainView;

        #endregion Private Fields

        #region Public Methods

        public void OnFixedUpdate()
        {
            if (_isStartGame)
            {
                _gameController.OnFixedUpdate();

                _controlController.HandleUserAction();
                if (_controlController.CheckPressStartGame())
                {
                    _gameController.StartGame();
                }
            }
        }

        #endregion Public Methods
    }
}