namespace Arcanoid.Controllers
{
    public interface IGameController
    {
        void OnFixedUpdate();
        bool IsStartGame { get; }

        void StartGame();
    }
}