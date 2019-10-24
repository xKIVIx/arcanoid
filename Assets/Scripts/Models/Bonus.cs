
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
        public Sprite sprite;
        public Color color;
        public float dropSpeed;
        public BonusType bonusType;
        public float bonusSize;
    }
}
