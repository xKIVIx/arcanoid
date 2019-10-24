using UnityEngine;

namespace Arcanoid.Models
{
    /// <summary>
    /// Сегмент перемещения. Содержит точку начала и конца пути.
    /// </summary>
    public struct MovementSegment
    {
        #region Public Fields

        public Vector2 endPoint;
        public Vector2 startPoint;

        #endregion Public Fields
    }
}