using Arcanoid.Models;
using UnityEngine;

namespace Arcanoid.Views
{
    /// <summary>
    /// Основная реализация разбиваемых блоков.
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    internal class BlockView : MonoBehaviour, IBlockView
    {
        #region Private Fields

        /// <summary>
        /// Информация о блоке, для расчета колизии.
        /// </summary>
        private Block _block;

        /// <summary>
        /// Текущее количество здоровья.
        /// </summary>
        private int _currentHP;

        /// <summary>
        /// Цвета показывающие количество жизней. В конце массива для полностью целого блока.
        /// </summary>
        [SerializeField]
        private Color32[] _lifeColors;

        /// <summary>
        /// Рендер спрайта.
        /// </summary>
        private SpriteRenderer _spriteRender;

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        /// <see cref="IBlockView.GetBlockInfo"/>
        /// </summary>
        public Block GetBlockInfo()
        {
            return _block;
        }

        /// <summary>
        /// <see cref="IBlockView.IsLive"/>
        /// </summary>
        public bool IsLive()
        {
            return _currentHP >= 0;
        }

        /// <summary>
        /// <see cref="IBlockView.Strike"/>
        /// </summary>
        public void Strike()
        {
            _currentHP--;
            UpdateView();
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Иницилизация.
        /// </summary>
        private void Awake()
        {
            var colider = GetComponent<BoxCollider2D>();
            _block = new Block(colider.bounds);
            _currentHP = _lifeColors.Length - 1;
            _spriteRender = GetComponent<SpriteRenderer>();

            UpdateView();
        }

        /// <summary>
        /// Обновление отображения.
        /// </summary>
        private void UpdateView()
        {
            if (_currentHP >= 0)
            {
                _spriteRender.color = _lifeColors[_currentHP];
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        #endregion Private Methods
    }
}