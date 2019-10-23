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

        public Vector2 GetCenter()
        {
            throw new System.NotImplementedException();
        }

        #endregion Private Fields

        #region Public Methods

        
        public void Move(Vector2 moveVector)
        {
            transform.TransformDirection(moveVector);
        }

        
        public void Remove()
        {
            Destroy(gameObject);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Иницилизация.
        /// </summary>
        private void Start()
        {
            _colider = GetComponent<CircleCollider2D>();
        }

        #endregion Private Methods
    }
}