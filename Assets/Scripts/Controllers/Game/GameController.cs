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
        /// Сумарный бонус к скорости шарика.
        /// </summary>
        private float _speedBallBonus;

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
                HandleBonuses();
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

        private void AcceptBonus(Bonus bonus)
        {
            switch (bonus.bonusType)
            {
                case BonusType.SPEED:
                    if (_gameParams.ballSpeed + _speedBallBonus + bonus.bonusSize > _gameParams.ballSpeedMin)
                    {
                        _speedBallBonus += bonus.bonusSize;
                    }
                    break;

                case BonusType.SPLIT_BALL:
                    var mainBall = _gameFieldView.Balls[0];
                    var ball = _gameFieldView.AddBall(mainBall.GetCenter());
                    ball.LastMoveDir = mainBall.LastMoveDir * -1;
                    break;

                case BonusType.EXPAND_USER_SLIDE:
                    _userSlideView.SizeBonus += bonus.bonusSize;
                    var block = _userSlideView.GetBlock();
                    var fieldBlock = _gameFieldView.FieldBlock;
                    var delta = 0.0f;
                    if (block.BoundMin.x < fieldBlock.BoundMin.x)
                    {
                        delta = fieldBlock.BoundMin.x - block.BoundMin.x;
                    }
                    else if (block.BoundMax.x > fieldBlock.BoundMax.x)
                    {
                        delta = fieldBlock.BoundMax.x - block.BoundMax.x;
                    }

                    _userSlideView.Move(new Vector2(delta, 0));
                    break;
            }
        }

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

                var coliseResult = _coliseController.CheckColise(movementSegment, _gameFieldView.FieldBlock, true);

                if (coliseResult.isColise)
                {
                    if (coliseResult.normal.y == -1)
                    {
                        balls[ballId].Remove();
                        balls.RemoveAt(ballId);
                        ballId--;
                        if (balls.Count == 0)
                        {
                            HandleFailEnd();
                        }
                    }
                    else
                    {
                        var lastDir = _coliseController.CalculateRicochet(balls[ballId].LastMoveDir,
                                                                          coliseResult.normal);
                        balls[ballId].Move(coliseResult.colisePoint - balls[ballId].GetCenter());
                        balls[ballId].LastMoveDir = lastDir;
                    }
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
                    var lastDir =  _coliseController.CalculateRicochet(balls[ballId].LastMoveDir,
                                                                                    coliseResult.normal);
                    balls[ballId].Move(coliseResult.colisePoint - balls[ballId].GetCenter());
                    balls[ballId].LastMoveDir = lastDir;

                    blocks[blockIdColise].Strike();
                    if (!blocks[blockIdColise].IsLive())
                    {
                        if (blocks[blockIdColise].IsHasBonus)
                        {
                            _gameFieldView.AddBonus(blocks[blockIdColise].Bonus, blocks[blockIdColise].GetBlockInfo().GetCenter());
                        }
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
                    balls[ballId].Move(coliseResult.colisePoint - center);
                    balls[ballId].LastMoveDir = ricochet;

                    continue;
                }

                balls[ballId].Move(balls[ballId].LastMoveDir * (_gameParams.ballSpeed + _speedBallBonus));
            }
        }

        /// <summary>
        /// Обработка механици бонусов.
        /// </summary>
        private void HandleBonuses()
        {
            var bonuses = _gameFieldView.Bonuses;
            for (var bonusId = 0; bonusId < bonuses.Count; bonusId++)
            {
                var bonusBlock = bonuses[bonusId].GetBlockInfo();
                var currentPos = bonusBlock.GetCenter();

                if (currentPos.y < _gameFieldView.FieldBlock.BoundMin.y)
                {
                    bonuses[bonusId].Remove();
                    _gameFieldView.Bonuses.RemoveAt(bonusId);
                    bonusId--;
                    continue;
                }

                if (_coliseController.CheckColise(bonusBlock, _userSlideView.GetBlock()))
                {
                    AcceptBonus(bonuses[bonusId].BonusParametrs);
                    bonuses[bonusId].Remove();
                    _gameFieldView.Bonuses.RemoveAt(bonusId);
                    bonusId--;
                    continue;
                }

                bonuses[bonusId].Move(new Vector2(0, -1) * bonuses[bonusId].BonusParametrs.dropSpeed);
            }
        }

        /// <summary>
        /// Обработка проигрыша
        /// </summary>
        private void HandleFailEnd()
        {
            Reset();
            _gameFieldView.Restart();
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
            Reset();
            _gameFieldView.NextLvl();
        }

        /// <summary>
        /// Сброс состояний игры
        /// </summary>
        private void Reset()
        {
            IsStartGame = false;
            _speedBallBonus = 0.0f;
        }

        #endregion Private Methods
    }
}