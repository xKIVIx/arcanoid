using Arcanoid.Controllers;
using Arcanoid.Models;
using NUnit.Framework;
using UnityEngine;

namespace Controllers.ColiseContrller
{
    public class ColiseTests
    {
        #region Private Fields

        private Block _block;
        private IColiseController _coliseController;

        #endregion Private Fields

        #region Public Methods

        [Test]
        public void Colise()
        {
            var movements = new MovementSegment[]
            {
                new MovementSegment{ startPoint = new Vector2(-12.5f, 3f), endPoint = new Vector2(-10.7f, 1.5f) },
                new MovementSegment{ startPoint = new Vector2(-10.41489f, 1.61148f), endPoint = new Vector2(-9.71394f, 3.34835f) },
                new MovementSegment{ startPoint = new Vector2(-7.5f, 3.3f), endPoint = new Vector2(-12.43711f, 3.59647f) },
                new MovementSegment{ startPoint = new Vector2(-11.17787f, 4.60138f), endPoint = new Vector2(-11.12205f, 3.31733f) },
                new MovementSegment{ startPoint = new Vector2(-10.3f, 3.9f), endPoint = new Vector2(-11.8f, 4.1f) },
            };

            var normals = new Vector2[]
            {
                new Vector2(-1.0f, 0.0f),
                new Vector2(0.0f, -1.0f),
                new Vector2(1.0f, 0.0f),
                new Vector2(0.0f, 1.0f),
                new Vector2(0.0f, 1.0f)
            };

            var colisePoints = new Vector2[]
            {
                new Vector2(-12.0f, 2.6f),
                new Vector2(-10.3f, 2.0f),
                new Vector2(-10.0f, 3.5f),
                new Vector2(-11.2f, 4.0f),
                new Vector2(-11.3f, 4.0f)
            };

            for (var i = 0; i < movements.Length; i++)
            {
                var result = _coliseController.CheckColise(movements[i], 0.0, _block);
                Assert.IsTrue(result.isColise, $"Fail movement vector: start point: {movements[i].startPoint} end point: {movements[i].endPoint}");
                Assert.AreEqual(normals[i], result.normal, $"Uncorrect normal: start point: {movements[i].startPoint} end point: {movements[i].endPoint}");
                Assert.AreEqual(colisePoints[i].x, result.colisePoint.x, 0.3, $"Uncorrect x coord colise point: start point: {movements[i].startPoint} end point: {movements[i].endPoint}");
                Assert.AreEqual(colisePoints[i].y, result.colisePoint.y, 0.3, $"Uncorrect y coord colise point: start point: {movements[i].startPoint} end point: {movements[i].endPoint}");
            }
        }

        /// <summary>
        /// Иницилизация
        /// </summary>
        [SetUp]
        public void CommonInstall()
        {
            _coliseController = new ColiseController();

            var bounds = new Bounds(new Vector3(-11.0f, 3.0f, 0.0f),
                                    new Vector3(2.0f, 2.0f, 0.0f));

            _block = new Block(bounds);
        }

        [Test]
        public void DontColise()
        {
            var movements = new MovementSegment[]
            {
                new MovementSegment{ startPoint = new Vector2(-10.06561f, 4.32486f), endPoint = new Vector2(-9.1461f, 3.67668f) },
                new MovementSegment{ startPoint = new Vector2(-11.2275f, 1.79137f), endPoint = new Vector2(-11.31434f, 0.89192f) },
                new MovementSegment{ startPoint = new Vector2(-12.74106f, 3.38557f), endPoint = new Vector2(-12.4185f, 1.2579f) },
                new MovementSegment{ startPoint = new Vector2(-12.54256f, 4.52694f), endPoint = new Vector2(-8.78347f, 4.55175f) },
            };

            foreach (var movement in movements)
            {
                var result = _coliseController.CheckColise(movement, 0.0, _block);
                Assert.IsFalse(result.isColise, $"Fail movement vector: start point: {movement.startPoint} end point: {movement.endPoint}");
            }
        }

        #endregion Public Methods
    }
}