using UnityEngine;

namespace Arcanoid.Models
{
    [CreateAssetMenu(fileName = "Game parametrs", menuName = "Game parametrs")]
    public class GameParams : ScriptableObject
    {
        #region Public Fields

        public float ballSpeed = 1.0f;
        public float userSpeed = 1.0f;

        #endregion Public Fields
    }
}