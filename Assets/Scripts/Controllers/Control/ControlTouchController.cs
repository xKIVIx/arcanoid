using Arcanoid.Models;
using Arcanoid.Views;
using UnityEngine;

namespace Arcanoid.Controllers
{
    public class ControlTouchController : IControlController
    {
        #region Public Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="userSlideView">Плитка, которой управляет игрок</param>
        /// <param name="fieldBlock">Данные о границах поля</param>
        /// <param name="userSlideSpeed">Скорость плитки игрока</param>
        public ControlTouchController(IUserSlideView userSlideView,
                                         Block fieldBlock,
                                         float userSlideSpeed)
        {
            _userSlideView = userSlideView;
            _slideSpeed = userSlideSpeed;
            _fieldBlock = fieldBlock;
        }

        #endregion Public Constructors

        #region Private Fields

        private Block _fieldBlock;
        private bool _isPressStartGame = false;
        private float _slideSpeed;
        private IUserSlideView _userSlideView;

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        /// <see cref="IControlController.CheckPressStartGame"/>
        /// </summary>
        /// <returns></returns>
        public bool CheckPressStartGame()
        {
            var result = _isPressStartGame;
            _isPressStartGame = false;
            return result;
        }

        /// <summary>
        /// <see cref="IControlController.HandleUserAction"/>
        /// </summary>
        public void HandleUserAction()
        {
            float width = Screen.width;
            var touchs = Input.touches;
            if (touchs.Length != 0)
            {
                if (touchs[0].position.x < width / 3.0f)
                {
                    var delta = _userSlideView.GetBlock().BoundMin.x - _fieldBlock.BoundMin.x;
                    var dist = Mathf.Min(delta, _slideSpeed);
                    _userSlideView.Move(new Vector2(-dist, 0));
                }
                else if (touchs[0].position.x < width * 2.0f / 3.0f)
                {
                    _isPressStartGame = true;
                }
                else
                {
                    var delta = _fieldBlock.BoundMax.x - _userSlideView.GetBlock().BoundMax.x;
                    var dist = Mathf.Min(delta, _slideSpeed);
                    _userSlideView.Move(new Vector2(dist, 0));
                }
            }
        }

        #endregion Public Methods
    }
}