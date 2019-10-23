using Arcanoid.Models;
using UnityEngine;

namespace Arcanoid.Controllers
{
    /// <summary>
    /// Основная реализация контроллера колизий.
    /// </summary>
    public class ColiseController : IColiseController
    {
        #region Private Structs

        /// <summary>
        /// Структура для описание стороны блока.
        /// </summary>
        private struct Side
        {
            #region Public Fields

            public float deltaX;
            public float deltaY;
            public float K;
            public Vector2 normal;

            #endregion Public Fields
        }

        #endregion Private Structs

        #region Public Methods

        /// <summary>
        /// <see cref="IColiseController.CalculateRicochet(Vector2, Vector2)"/>
        /// </summary>
        public Vector2 CalculateRicochet(Vector2 movementVector, Vector2 normal)
        {
            var v = movementVector * -1.0f;
            var a = normal * v;
            var k = (a.x + a.y) / normal.sqrMagnitude * 2.0f;
            var result = k * normal - v;
            return result.normalized;
        }

        /// <summary>
        /// <see cref="IColiseController.CheckColise(MovementSegment, Block)"/>
        /// </summary>
        public ColiseData CheckColise(MovementSegment segment, Block block)
        {
            var startPoint = segment.startPoint;
            var endPoint = segment.endPoint;

            var minPoint = new Vector2(Mathf.Min(startPoint.x, endPoint.x),
                                       Mathf.Min(startPoint.y, endPoint.y));
            var maxPoint = new Vector2(Mathf.Max(startPoint.x, endPoint.x),
                                       Mathf.Max(startPoint.y, endPoint.y));

            var segmentDeltaY = startPoint.y - endPoint.y;
            var segmentDeltaX = startPoint.x - endPoint.x;
            var segmentK = startPoint.x * endPoint.y - endPoint.x * startPoint.y;

            var sides = new Side[]
            {
                new Side{ K = block.TopK, deltaX = block.DeltaTop.x, deltaY = block.DeltaTop.y, normal = new Vector2(0.0f, 1.0f) },
                new Side{ K = block.RightK, deltaX = block.DeltaRight.x, deltaY = block.DeltaRight.y, normal = new Vector2(1.0f, 0.0f) },
                new Side{ K = block.BottomK, deltaX = block.DeltaBottom.x, deltaY = block.DeltaBottom.y, normal = new Vector2(0.0f, -1.0f) },
                new Side{ K = block.LeftK, deltaX = block.DeltaLeft.x, deltaY = block.DeltaLeft.y, normal = new Vector2(-1.0f, 0.0f) }
            };

            var result = new ColiseData();
            var currSqrDistance = float.MaxValue;

            foreach (var side in sides)
            {
                var d = side.deltaX * segmentDeltaY - side.deltaY * segmentDeltaX;

                /// Отрезок параллелен прямой.
                if (d == 0) continue;

                var point = new Vector2((side.K * segmentDeltaX - side.deltaX * segmentK) / d,
                                        (side.K * segmentDeltaY - side.deltaY * segmentK) / d);

                if (point.x >= minPoint.x &&
                    point.x <= maxPoint.x &&
                    point.y >= minPoint.y &&
                    point.y <= maxPoint.y &&
                    block.IsOnBounds(point))
                {
                    var v = point - startPoint;
                    if (v.sqrMagnitude < currSqrDistance)
                    {
                        currSqrDistance = v.sqrMagnitude;
                        result.colisePoint = point;
                        result.normal = side.normal;
                    }
                }
            }

            result.isColise = currSqrDistance != float.MaxValue;

            return result;
        }

        #endregion Public Methods
    }
}