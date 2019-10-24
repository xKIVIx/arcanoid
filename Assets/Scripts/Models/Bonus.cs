using UnityEngine;

namespace Arcanoid.Models
{
    /// <summary>
    /// Возможные типы бонусов.
    /// </summary>
    public enum BonusType
    {
        SPEED,
        SPLIT_BALL,
        EXPAND_USER_SLIDE
    }

    /// <summary>
    /// Параметры бонуса
    /// </summary>
    [CreateAssetMenu(fileName = "Bonus", menuName = "Bonus")]
    public class Bonus : ScriptableObject
    {
        #region Public Fields

        public float bonusSize;
        public BonusType bonusType;
        public Color color;
        public float dropSpeed;
        public Sprite sprite;

        #endregion Public Fields
    }
}