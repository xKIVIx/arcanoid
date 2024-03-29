﻿using Arcanoid.Controllers;
using NUnit.Framework;
using UnityEngine;

namespace Controllers.ColiseContrller
{
    internal class RicochetTest
    {
        #region Private Fields

        private ColiseController _coliseController;

        #endregion Private Fields

        #region Public Methods

        [Test]
        public void CalculateRicochet()
        {
            var movements = new Vector2[]
            {
                new Vector2(0.5f, -0.5f),
                new Vector2(-1.0f, 0.0f)
            };

            var normals = new Vector2[]
            {
                new Vector2(1.0f, 0.0f),
                new Vector2(1.0f, 0.0f)
            };

            var ricochet = new Vector2[]
            {
                new Vector2(-0.5f, -0.5f),
                new Vector2(1.0f, 0.0f)
            };

            for (var i = 0; i < movements.Length; i++)
            {
                var result = _coliseController.CalculateRicochet(movements[i], normals[i]);
                Assert.AreEqual(ricochet[i].x, result.x, 0.1, $"Uncorrect x coord ricochet vector: move vector{movements[i]}");
                Assert.AreEqual(ricochet[i].y, result.y, 0.1, $"Uncorrect y coord ricochet vector: move vector{movements[i]}");
            }
        }

        /// <summary>
        /// Иницилизация
        /// </summary>
        [SetUp]
        public void CommonInstall()
        {
            _coliseController = new ColiseController();
        }

        #endregion Public Methods
    }
}