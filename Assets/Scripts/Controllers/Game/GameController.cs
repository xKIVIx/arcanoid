using Arcanoid.Models;
using Arcanoid.Views;
using UnityEngine;

namespace Arcanoid.Controllers
{
    /// <summary>
    /// Основная реализация игровго котроллера.
    /// </summary>
    public class GameController : IGameController
    {
        #region Private Structs
        /// <summary>
        /// Структура для хранения данных о шариках.
        /// </summary>
        private struct BallInfo
        {
            #region Public Fields

            public Vector2 lastMoveDir;
            public IBallView view;

            #endregion Public Fields
        }

        #endregion Private Structs

        #region Public Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameFieldView">
        /// Представление игрового поля.
        /// </param>
        /// <param name="coliseController">
        /// Котроллер для расчета колизий.
        /// </param>
        /// <param name="gameParams">
        /// Параметры игры
        /// </param>
        public GameController(IGameFieldView gameFieldView,
                              IColiseController coliseController,
                              GameParams gameParams)
        {
            _gameFieldView = gameFieldView;
            _userSlideView = _gameFieldView.UserSlideView;
            _coliseController = coliseController;
            _gameParams = gameParams;
            IsStartGame = false;

            _balls = new BallInfo[] { new BallInfo { view = gameFieldView.AddBall() } };
            _blocks = new IBlockView[0];
        }

        #endregion Public Constructors

        #region Private Fields
        /// <summary>
        /// Существующие шары.
        /// </summary>
        private BallInfo[] _balls;

        /// <summary>
        /// Все блоки на уровне.
        /// </summary>
        private IBlockView[] _blocks;

        /// <summary>
        /// Контроллер колизий.
        /// </summary>
        private IColiseController _coliseController;

        /// <summary>
        /// Представление игровго поля.
        /// </summary>
        private IGameFieldView _gameFieldView;

        /// <summary>
        /// Игровые параметры.
        /// </summary>
        private GameParams _gameParams;

        /// <summary>
        /// Представление плитки игрока.
        /// </summary>
        private IUserSlideView _userSlideView;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// <see cref="IGameController.IsStartGame"/>
        /// </summary>
        public bool IsStartGame { get; private set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// <see cref="IGameController.OnFixedUpdate"/>
        /// </summary>
        public void OnFixedUpdate()
        {
            if (IsStartGame)
            {
                HandleBalls();
            }
            else
            {
                HandlePrepareFire();
            }
        }

        /// <summary>
        /// <see cref="IGameController.StartGame"/>
        /// </summary>
        public void StartGame()
        {
            if (!IsStartGame)
            {
                var userSlideCenter = _gameFieldView.UserSlideView.GetBlock().GetCenter();
                _balls[0].lastMoveDir = (_balls[0].view.GetCenter() - userSlideCenter).normalized;
                IsStartGame = true;
            }
        }

        #endregion Public Methods

        #region Private Methods
        /// <summary>
        /// Обработка перемещения шаров.
        /// </summary>
        private void HandleBalls()
        {
            for (var i = 0; i < _balls.Length; i++)
            {
                var currentPos = _balls[i].view.GetCenter();
                var movementSegment = new MovementSegment()
                {
                    startPoint = currentPos,
                    endPoint = currentPos + _balls[i].lastMoveDir * _gameParams.ballSpeed
                };

                var coliseResult = _coliseController.CheckColise(movementSegment, _gameFieldView.FieldBlock);

                if (coliseResult.isColise)
                {
                    coliseResult.normal *= -1;
                    _balls[i].lastMoveDir = _coliseController.CalculateRicochet(_balls[i].lastMoveDir, coliseResult.normal);
                    continue;
                }

                var distance = float.MaxValue;
                foreach (var block in _blocks)
                {
                    var coliseInfo = _coliseController.CheckColise(movementSegment, block.GetBlockInfo());
                    if (coliseInfo.isColise)
                    {
                        var newDistance = (coliseInfo.colisePoint - currentPos);
                        if (newDistance.sqrMagnitude < distance)
                        {
                            coliseResult = coliseInfo;
                        }
                    }
                }

                if (coliseResult.isColise)
                {
                    _balls[i].lastMoveDir = _coliseController.CalculateRicochet(_balls[i].lastMoveDir, coliseResult.normal);
                    continue;
                }

                var slideBlock = _userSlideView.GetBlock();
                coliseResult = _coliseController.CheckColise(movementSegment, slideBlock);
                if (coliseResult.isColise)
                {
                    var ricochet = _coliseController.CalculateRicochet(_balls[i].lastMoveDir, coliseResult.normal);
                    var center = slideBlock.GetCenter();
                    ricochet += new Vector2(coliseResult.colisePoint.x - center.x, 0);
                    _balls[i].lastMoveDir = ricochet.normalized;

                    continue;
                }

                _balls[i].view.Move(_balls[i].lastMoveDir * _gameParams.ballSpeed);
            }
        }

        /// <summary>
        /// Обработка подготовки к запуску шарика.
        /// </summary>
        private void HandlePrepareFire()
        {
            var ballView = _balls[0].view;
            var ballCenter = ballView.GetCenter();
            var minX = _userSlideView.GetBlock().BoundMin.x;
            var maxX = _userSlideView.GetBlock().BoundMax.x;

            if (ballCenter.x < minX)
            {
                ballView.Move(new Vector2(minX - ballCenter.x, 0));
            }
            else if (ballCenter.x > maxX)
            {
                ballView.Move(new Vector2(maxX - ballCenter.x, 0));
            }
        }

        #endregion Private Methods
    }
}