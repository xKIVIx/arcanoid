﻿using Arcanoid.Models;
using UnityEngine;

namespace Arcanoid.Controllers
{
    /// <summary>
    /// Контроллер вычисляющий колизии объектов.
    /// </summary>
    public interface IColiseController
    {
        #region Public Methods

        /// <summary>
        /// Расчет вектора рекошета.
        /// </summary>
        /// <param name="movementVector">Вектор движения объекта</param>
        /// <param name="normal">Нормаль точки рекошета</param>
        /// <returns></returns>
        Vector2 CalculateRicochet(Vector2 movementVector, Vector2 normal);

        /// <summary>
        /// Проверяет колизиию отрезка движения шарика и блока.
        /// </summary>
        /// <param name="segment">Отрезок перемещения</param>
        /// <param name="block">Информация о границах блока</param>
        /// <param name="radius"> Радиус шарика</param>
        /// <param name="isInBlock">Должен находится внутри блока?</param>
        /// <returns>
        /// </returns>
        ColiseData CheckColise(MovementSegment segment, double radius, Block block, bool isInBlock = false);

        /// <summary>
        /// Проверяет колизиию двух блоков.
        /// </summary>
        /// <param name="blockOne"></param>
        /// <param name="blockTwo"></param>
        /// <returns>
        /// </returns>
        bool CheckColise(Block blockOne, Block blockTwo);

        #endregion Public Methods
    }
}