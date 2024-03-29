﻿using Arcanoid.Models;
using System.Collections.Generic;
using UnityEngine;

namespace Arcanoid.Views
{
    /// <summary>
    /// Интерфейс для представления игрового поля
    /// </summary>
    public interface IGameFieldView
    {
        #region Public Properties

        /// <summary>
        /// Список существующих шаров
        /// </summary>
        List<IBallView> Balls { get; }

        /// <summary>
        /// Бонусы на поле.
        /// </summary>
        List<IBonusView> Bonuses { get; }

        /// <summary>
        /// Текущий уровень.
        /// </summary>
        ILvlView CurrentLvl { get; }

        /// <summary>
        /// Данные для расчета колизии с границами поля.
        /// </summary>
        Block FieldBlock { get; }

        /// <summary>
        /// Интерфейс представления плитки игрока.
        /// </summary>
        IUserSlideView UserSlideView { get; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Добавление нового шара.
        /// </summary>
        /// <param name="position">Позиция появления шара</param>
        /// <returns></returns>
        IBallView AddBall(Vector2 position);

        /// <summary>
        /// Добавление нового шара. Шар появляется на начальной позиции.
        /// </summary>
        /// <returns></returns>
        IBallView AddBall();

        /// <summary>
        /// Добавить бонус на поле.
        /// </summary>
        /// <param name="bonusInfo">
        /// Информация о бонусе.
        /// </param>
        /// <param name="position">
        /// Точка появления
        /// </param>
        /// <returns></returns>
        IBonusView AddBonus(Bonus bonusInfo, Vector2 position);

        /// <summary>
        /// Запустить следующий уровень
        /// </summary>
        void NextLvl();

        /// <summary>
        /// Перезагрузка поля.
        /// </summary>
        void Restart();

        #endregion Public Methods
    }
}