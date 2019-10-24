using Arcanoid.Models;
using UnityEngine;

namespace Arcanoid.Views
{
    public interface IBonusView
    {
        /// <summary>
        /// Параметры бонуса
        /// </summary>
        Bonus BonusParametrs { get; set; }

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
    }
}
