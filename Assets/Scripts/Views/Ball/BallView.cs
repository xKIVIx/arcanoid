using UnityEngine;

namespace Arcanoid.Views
{
    /// <summary>
    /// Основная реализация отображения шарика.
    /// </summary>
    [RequireComponent(typeof(CircleCollider2D))]
    internal class BallView : MonoBehaviour, IBallView
    {
        #region Private Fields

        /// <summary>
        /// Колайдер шарика.
        /// </summary>
        private CircleCollider2D _colider;

        /// <summary>
        /// Объект уничтожен?
        /// </summary>
        private bool _isDestroy = false;

        #endregion Private Fields

        #region Public Properties

        public Vector2 LastMoveDir { get; set; }

        #endregion Public Properties

        #region Public Methods

        public Vector2 GetCenter()
        {
            return transform.position;
        }

        /// <summary>
        /// <see cref="IBallView.GetRadius"/>
        /// </summary>
        /// <returns></returns>
        public float GetRadius()
        {
            return _colider.bounds.extents.x;
        }

        /// <summary>
        /// <see cref="IBallView.Move(Vector2)"/>
        /// </summary>
        public void Move(Vector2 moveVector)
        {
            LastMoveDir = moveVector.normalized;
            transform.position += new Vector3(moveVector.x, moveVector.y, 0.0f);
        }

        /// <summary>
        /// <see cref="IBallView.Remove"/>
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

        /// <summary>
        /// Иницилизация.
        /// </summary>
        private void Awake()
        {
            _colider = GetComponent<CircleCollider2D>();
        }

        #endregion Private Methods
    }
}