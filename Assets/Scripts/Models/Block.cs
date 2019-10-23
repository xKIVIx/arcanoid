using UnityEngine;

namespace Arcanoid.Models
{
    public struct Block
    {
        #region Public Constructors

        public Block(Bounds bounds)
        {
            _bounds = bounds;

            var boundMax = _bounds.max;
            var boundMin = _bounds.min;

            DeltaTop = new Vector2(boundMin.x - boundMax.x, 0);
            DeltaRight = new Vector2(0, boundMax.y - boundMin.y);
            DeltaBottom = new Vector2(boundMin.x - boundMax.x, 0);
            DeltaLeft = new Vector2(0, boundMin.y - boundMax.y);

            TopK = (boundMin.x - boundMax.x) * boundMax.y;
            BottomK = (boundMin.x - boundMax.x) * boundMin.y;

            RightK = (boundMin.y - boundMax.y) * boundMax.x;
            LeftK = (boundMax.y - boundMin.y) * boundMin.x;
        }

        #endregion Public Constructors

        #region Private Fields

        private Bounds _bounds;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// x1y2 - x2y2 для верхней границы
        /// </summary>
        public float BottomK { get; }

        /// <summary>
        /// Максимальные координаты границ.
        /// </summary>
        public Vector3 BoundMax => _bounds.max;

        /// <summary>
        /// Минимальные координаты границ.
        /// </summary>
        public Vector3 BoundMin => _bounds.min;

        /// <summary>
        /// (x1 - x2, y1 - y2)
        /// </summary>
        public Vector2 DeltaBottom { get; }

        /// <summary>
        /// (x1 - x2, y1 - y2)
        /// </summary>
        public Vector2 DeltaLeft { get; }

        /// <summary>
        /// (x1 - x2, y1 - y2)
        /// </summary>
        public Vector2 DeltaRight { get; }

        /// <summary>
        /// (x1 - x2, y1 - y2)
        /// </summary>
        public Vector2 DeltaTop { get; }

        /// <summary>
        /// x1y2 - x2y2 для верхней границы
        /// </summary>
        public float LeftK { get; }

        /// <summary>
        /// x1y2 - x2y1 для верхней границы
        /// </summary>
        public float RightK { get; }

        /// <summary>
        /// x1y2 - x2y1 для верхней границы
        /// </summary>
        public float TopK { get; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Получить центр блока.
        /// </summary>
        /// <returns></returns>
        public Vector2 GetCenter()
        {
            return _bounds.center;
        }

        /// <summary>
        /// Проверка принадлежности точки блоку.
        /// </summary>
        /// <param name="point"></param>
        /// <returns>
        /// True - если точка содержится в границах блока.
        /// </returns>
        public bool IsOnBounds(Vector2 point, float e)
        {
            return point.x >= _bounds.min.x - e &&
                   point.x <= _bounds.max.x + e &&
                   point.y >= _bounds.min.y - e &&
                   point.y <= _bounds.max.y + e;
        }

        #endregion Public Methods
    }
}