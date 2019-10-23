using Arcanoid.Controllers;
using Arcanoid.Models;
using Arcanoid.Views;
using UnityEngine;

public class Init : MonoBehaviour
{
    #region Private Fields
    
    /// <summary>
    /// Параметры игры.
    /// </summary>
    [SerializeField]
    private GameParams _gameParams;

    /// <summary>
    /// Главный контроллер.
    /// </summary>
    private IMainController _mainController;

    /// <summary>
    /// Главное вывод.
    /// </summary>
    [SerializeField]
    private MainView _mainView;

    #endregion Private Fields

    #region Private Methods

    private void FixedUpdate() => _mainController.OnFixedUpdate();

    // Start is called before the first frame update
    private void Start()
    {
        IControlController controlController = new ControlKeyboardController(_mainView.GameFieldView.UserSlideView,
                                                                             _mainView.GameFieldView.FieldBlock,
                                                                             _gameParams.userSpeed);

        _mainController = new MainController(_mainView,
                                             new ColiseController(),
                                             controlController);
    }

    #endregion Private Methods
}