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
            _curFirstBlock = 0;
        }

        #endregion Public Constructors

        #region Private Fields

        /// <summary>
        /// Контроллер колизий.
        /// </summary>
        private IColiseController _coliseController;

        /// <summary>
        /// Индекс первого "живого" блока
        /// </summary>
        private int _curFirstBlock;

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
                if (_gameFieldView.Balls.Count == 0)
                {
                    Debug.LogError("Game field haven`t ball!");
                    return;
                }
                _gameFieldView.Balls[0].LastMoveDir = (_gameFieldView.Balls[0].GetCenter() - userSlideCenter).normalized;
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
            var balls = _gameFieldView.Balls;
            for (var ballId = 0; ballId < balls.Count; ballId++)
            {
                var currentPos = balls[ballId].GetCenter();
                var movementSegment = new MovementSegment()
                {
                    startPoint = currentPos,
                    endPoint = currentPos + balls[ballId].LastMoveDir * _gameParams.ballSpeed
                };

                var coliseResult = _coliseController.CheckColise(movementSegment, _gameFieldView.FieldBlock);

                if (coliseResult.isColise)
                {
                    coliseResult.normal *= -1;
                    balls[ballId].LastMoveDir = _coliseController.CalculateRicochet(balls[ballId].LastMoveDir, 
                                                                                    coliseResult.normal);
                    continue;
                }

                var distance = float.MaxValue;
                var blocks = _gameFieldView.CurrentLvl.Blocks;
                var blockIdColise = 0;
                for (var blockId = _curFirstBlock; blockId < blocks.Length; blockId++)
                {
                    var coliseInfo = _coliseController.CheckColise(movementSegment, blocks[blockId].GetBlockInfo());
                    if (coliseInfo.isColise)
                    {
                        var newDistance = (coliseInfo.colisePoint - currentPos);
                        if (newDistance.sqrMagnitude < distance)
                        {
                            coliseResult = coliseInfo;
                            blockIdColise = blockId;
                        }
                    }
                }

                if (coliseResult.isColise)
                {
                    balls[ballId].LastMoveDir = _coliseController.CalculateRicochet(balls[ballId].LastMoveDir, 
                                                                                    coliseResult.normal);
                    blocks[blockIdColise].Strike();
                    if (!blocks[blockIdColise].IsLive())
                    {
                        var t = _gameFieldView.CurrentLvl.Blocks[blockIdColise];
                        _gameFieldView.CurrentLvl.Blocks[blockIdColise] = blocks[_curFirstBlock];
                        _gameFieldView.CurrentLvl.Blocks[_curFirstBlock] = t;
                        _curFirstBlock++;
                        if (_curFirstBlock == blocks.Length)
                        {
                            HandleSuccessEnd();
                        }
                    }
                    continue;
                }

                var slideBlock = _userSlideView.GetBlock();
                coliseResult = _coliseController.CheckColise(movementSegment, slideBlock);
                if (coliseResult.isColise)
                {
                    var ricochet = _coliseController.CalculateRicochet(balls[ballId].LastMoveDir, coliseResult.normal);
                    var center = slideBlock.GetCenter();
                    ricochet += new Vector2(coliseResult.colisePoint.x - center.x, 0);
                    balls[ballId].LastMoveDir = ricochet.normalized;

                    continue;
                }

                balls[ballId].Move(balls[ballId].LastMoveDir * _gameParams.ballSpeed);
            }
        }

        /// <summary>
        /// Обработка подготовки к запуску шарика.
        /// </summary>
        private void HandlePrepareFire()
        {
            if (_gameFieldView.Balls.Count == 0)
            {
                Debug.LogError("Game field haven`t ball!");
                return;
            }
            var ballView = _gameFieldView.Balls[0];
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

        /// <summary>
        /// Обработка успешного прохождения уровня.
        /// </summary>
        private void HandleSuccessEnd()
        {
            IsStartGame = false;
            _gameFieldView.NextLvl();
        }

        #endregion Private Methods
    }
}