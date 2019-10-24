using UnityEngine;

namespace Arcanoid.Models
{
    /// <summary>
    /// Параметры игры
    /// </summary>
    [CreateAssetMenu(fileName = "Game parametrs", menuName = "Game parametrs")]
    public class GameParams : ScriptableObject
    {
        #region Public Fields

        public float ballSpeed = 1.0f;
        public float ballSpeedMin = 1.0f;
        public float userSpeed = 1.0f;

        #endregion Public Fields
    }
}