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

            public double deltaX;
            public double deltaY;
            public double K;
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
        public ColiseData CheckColise(MovementSegment segment, double radius, Block block, bool isInBlock = false)
        {

            var startPoint = segment.startPoint;
            var endPoint = segment.endPoint;

            double startX = segment.startPoint.x;
            double startY = segment.startPoint.y;

            double endX = segment.startPoint.x;
            double endY = segment.startPoint.y;

            double minX = Mathf.Min(startPoint.x, endPoint.x);
            double minY = Mathf.Min(startPoint.y, endPoint.y);

            double maxX = Mathf.Max(startPoint.x, endPoint.x);
            double maxY = Mathf.Max(startPoint.y, endPoint.y);

            double segmentDeltaY = startPoint.y - endPoint.y;
            double segmentDeltaX = startPoint.x - endPoint.x;
            double segmentK = startPoint.x * endPoint.y - endPoint.x * startPoint.y;

            var l = isInBlock ? 1.0f : -1.0f;

            var sides = new Side[]
            {
                new Side{ K = block.TopK, deltaX = block.DeltaTop.x, deltaY = block.DeltaTop.y, normal = new Vector2(0.0f, l) },
                new Side{ K = block.RightK, deltaX = block.DeltaRight.x, deltaY = block.DeltaRight.y, normal = new Vector2(l, 0.0f) },
                new Side{ K = block.BottomK, deltaX = block.DeltaBottom.x, deltaY = block.DeltaBottom.y, normal = new Vector2(0.0f, -l) },
                new Side{ K = block.LeftK, deltaX = block.DeltaLeft.x, deltaY = block.DeltaLeft.y, normal = new Vector2(-l, 0.0f) }
            };

            var result = new ColiseData();
            var currSqrDistance = double.MaxValue;

            foreach (var side in sides)
            {
                var d = side.deltaX * segmentDeltaY - side.deltaY * segmentDeltaX;

                /// Отрезок параллелен прямой.
                if (d == 0) continue;

                var pointX = (side.K * segmentDeltaX - side.deltaX * segmentK) / d;
                var pointY = (side.K * segmentDeltaY - side.deltaY * segmentK) / d;
                double e = radius;
                if (pointX >= minX - e &&
                    pointX <= maxX + e &&
                    pointY >= minY - e &&
                    pointY <= maxY + e &&
                    block.IsOnBounds(pointX, pointY, e))
                {
                    var v = (pointX - startX) * (pointX - startX) + (pointY - startY) * (pointY - startY);
                    if (v < currSqrDistance)
                    {
                        currSqrDistance = v;
                        result.sqrDist = v;
                        result.normal = side.normal;
                    }
                }
            }

            result.isColise = currSqrDistance != double.MaxValue;

            return result;
        }

        /// <summary>
        /// <see cref="IColiseController.CheckColise(Block, Block)"/>
        /// </summary>
        /// <param name="blockOne"></param>
        /// <param name="blockTwo"></param>
        /// <returns></returns>
        public bool CheckColise(Block blockOne, Block blockTwo)
        {
            return blockOne.Bounds.Intersects(blockTwo.Bounds);
        }

        #endregion Public Methods
    }
}