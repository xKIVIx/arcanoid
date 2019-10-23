using Arcanoid.Models;
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
        }

        #endregion Public Constructors

        #region Private Fields

        private IControlController _controlController;
        private IMainView _mainView;
        private IGameController _gameController;

        #endregion Private Fields

        #region Public Methods

        public void OnFixedUpdate()
        {
            _controlController.HandleUserAction();
            if(_controlController.CheckPressStartGame())
            {
                _gameController.StartGame();
            }
            _gameController.OnFixedUpdate();
        }

        #endregion Public Methods
    }
}