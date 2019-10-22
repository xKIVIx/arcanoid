using UnityEngine;

namespace Arcanoid.Models
{
    /// <summary>
    /// Данные о рекошете от блоков.
    /// </summary>
    public struct ColiseData
    {
        #region Public Fields

        public Vector2 colisePoint;
        public bool isColise;
        public Vector2 normal;

        #endregion Public Fields
    }
}