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

        #endregion Private Fields

        #region Public Methods

        public Vector2 GetCenter()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// <see cref="IBallView.Move(Vector2)"/>
        /// </summary>
        public void Move(Vector2 moveVector)
        {
            transform.position += new Vector3(moveVector.x, moveVector.y, 0.0f);
        }

        /// <summary>
        /// <see cref="IBallView.Remove"/>
        /// </summary>
        public void Remove()
        {
            Destroy(gameObject);
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