using Arcanoid.Models;
using UnityEngine;

namespace Arcanoid.Views
{
    /// <summary>
    /// Основная реализация представления бонуса.
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    internal class BonusView : MonoBehaviour, IBonusView
    {
        #region Private Fields

        /// <summary>
        /// Параметры бонуса.
        /// </summary>
        private Bonus _bonus;

        /// <summary>
        /// Колайдер бонуса.
        /// </summary>
        private BoxCollider2D _colider;

        /// <summary>
        /// Рендер спрайта бонуса
        /// </summary>
        private SpriteRenderer _spriteRenderer;

        /// <summary>
        /// Уничтожен ли объект?
        /// </summary>
        private bool _isDestroy = false;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// <see cref="IBonusView.BonusParametrs"/>
        /// </summary>
        public Bonus BonusParametrs
        {
            get => _bonus;
            set
            {
                _bonus = value;
                _spriteRenderer.sprite = _bonus.sprite;
                _spriteRenderer.color = _bonus.color;
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// <see cref="IBonusView.GetBlockInfo"/>
        /// </summary>
        /// <returns></returns>
        public Block GetBlockInfo()
        {
            return new Block(_colider.bounds);
        }

        /// <summary>
        /// <see cref="IBonusView.Move(Vector2)"/>
        /// </summary>
        /// <param name="moveVector"></param>
        public void Move(Vector2 moveVector)
        {
            transform.position += new Vector3(moveVector.x, moveVector.y);
        }

        /// <summary>
        /// <see cref="IBonusView.Remove"/>
        /// </summary>
        public void Remove()
        {
            if(!_isDestroy)
            {
                Destroy(gameObject);
                _isDestroy = true;
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void Awake()
        {
            _colider = GetComponent<BoxCollider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        #endregion Private Methods
    }
}