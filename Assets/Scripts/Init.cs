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
#if UNITY_ANDROID
        IControlController controlController = new ControlTouchController(_mainView.GameFieldView.UserSlideView,
                                                                          _mainView.GameFieldView.FieldBlock,
                                                                          _gameParams.userSpeed);
#else
        IControlController controlController = new ControlKeyboardController(_mainView.GameFieldView.UserSlideView,
                                                                             _mainView.GameFieldView.FieldBlock,
                                                                             _gameParams.userSpeed);
#endif
        IGameController gameController = new GameController(_mainView.GameFieldView,
                                                            new ColiseController(),
                                                            _gameParams);

        _mainController = new MainController(_mainView,
                                             controlController,
                                             gameController);
    }

    #endregion Private Methods
}