namespace Arcanoid.Views
{
    /// <summary>
    /// Интерфейс для представления игрового поля
    /// </summary>
    public interface IGameFieldView
    {
        /// <summary>
        /// Интерфейс представления плитки игрока.
        /// </summary>
        IUserSlideView UserSlideView { get; }

        /// <summary>
        /// Интерфейс представления шариков.
        /// </summary>
        IBallView BallView { get; }
    }
}
