using Arcanoid.Models;
using UnityEngine;

namespace Arcanoid.Views
{
    /// <summary>
    /// Интерфейст для представлений бонусов.
    /// </summary>
    public interface IBonusView
    {
        #region Public Properties

        /// <summary>
        /// Параметры бонуса
        /// </summary>
        Bonus BonusParametrs { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Получить информацию для расчета колизии.
        /// </summary>
        /// <returns></returns>
        Block GetBlockInfo();

        /// <summary>
        /// Перемещение объекта.
        /// </summary>
        /// <param name="moveVector">Вектор на который необходимо сместить объект</param>
        void Move(Vector2 moveVector);

        /// <summary>
        /// Удалить бонус с поля.
        /// </summary>
        void Remove();

        #endregion Public Methods
    }
}