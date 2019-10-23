using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arcanoid.Views;
using Arcanoid.Models;
using UnityEngine;

namespace Arcanoid.Controllers
{
    public class GameController : IGameController
    {

        private struct BallInfo
        {
            public IBallView view;
            public Vector2 lastMoveDir;
        }

        private IGameFieldView _gameFieldView;
        private IUserSlideView _userSlideView;
        private IColiseController _coliseController;
        private GameParams _gameParams;
        private BallInfo[] _balls;
        private IBlockView[] _blocks;
        public bool IsStartGame { get; private set; }


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

        public void OnFixedUpdate()
        {
            if(IsStartGame)
            {
                HandleBalls();
            }
            else
            {
                HandlePrepareFire();
            }
        }

        private void HandleBalls()
        {
            for(var i =0; i<_balls.Length; i++)
            {
                var currentPos = _balls[i].view.GetCenter();
                var movementSegment = new MovementSegment()
                {
                    startPoint = currentPos,
                    endPoint = currentPos + _balls[i].lastMoveDir * _gameParams.ballSpeed
                };

                
                var coliseResult = _coliseController.CheckColise(movementSegment, _gameFieldView.FieldBlock);
                if(!coliseResult.isColise)
                {
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
                }
                else
                {
                    coliseResult.normal *= -1;
                }

                if (coliseResult.isColise)
                {
                    var movementVector = coliseResult.colisePoint - currentPos;
                    _balls[i].lastMoveDir = _coliseController.CalculateRicochet(movementVector, coliseResult.normal);
                    _balls[i].view.Move(movementVector + _balls[i].lastMoveDir * 0.1f);
                }
                else
                {
                    _balls[i].view.Move(_balls[i].lastMoveDir * _gameParams.ballSpeed);
                }
            }
        }

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

        public void StartGame()
        {
            if(!IsStartGame)
            {
                var userSlideCenter = _gameFieldView.UserSlideView.GetBlock().GetCenter();
                _balls[0].lastMoveDir = (_balls[0].view.GetCenter() - userSlideCenter).normalized;
                IsStartGame = true;
            }
        }
    }
}
