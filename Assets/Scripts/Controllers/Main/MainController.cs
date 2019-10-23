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
        /// <param name="coliseController">Контроллер колизии</param>
        /// <param name="controlController">Контроллер управления</param>
        public MainController(IMainView mainView,
                              IColiseController coliseController,
                              IControlController controlController)
        {
            _mainView = mainView;
            _coliseController = coliseController;
            _controlController = controlController;
        }

        #endregion Public Constructors

        #region Private Fields

        private IColiseController _coliseController;
        private IControlController _controlController;
        private IMainView _mainView;

        #endregion Private Fields

        #region Public Methods

        public void OnFixedUpdate()
        {
            _controlController.HandleUserAction();
        }

        #endregion Public Methods
    }
}