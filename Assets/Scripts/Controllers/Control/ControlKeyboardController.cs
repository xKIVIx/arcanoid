using Arcanoid.Models;
using Arcanoid.Views;
using UnityEngine;

namespace Arcanoid.Controllers
{
    /// <summary>
    /// Реализация контроллера управления для клавиатуры.
    /// </summary>
    public class ControlKeyboardController : IControlController
    {
        #region Public Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userSlideView">Плитка, которой управляет игрок</param>
        /// <param name="fieldBlock">Данные о границах поля</param>
        /// <param name="userSlideSpeed">Скорость плитки игрока</param>
        public ControlKeyboardController(IUserSlideView userSlideView,
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
        private float _slideSpeed;
        private IUserSlideView _userSlideView;

        #endregion Private Fields

        #region Public Methods
        /// <summary>
        /// <see cref="IControlController.HandleUserAction"/>
        /// </summary>
        public void HandleUserAction()
        {
            if (Input.GetKey(KeyCode.A))
            {
                var delta = _userSlideView.GetBlock().BoundMin.x - _fieldBlock.BoundMin.x;
                var dist = Mathf.Min(delta, _slideSpeed);
                _userSlideView.Move(new Vector2(-dist, 0));
            }
            if (Input.GetKey(KeyCode.D))
            {
                var delta = _fieldBlock.BoundMax.x - _userSlideView.GetBlock().BoundMax.x;
                var dist = Mathf.Min(delta, _slideSpeed);
                _userSlideView.Move(new Vector2(dist, 0));
            }
        }

        #endregion Public Methods
    }
}