namespace Framework.Yggdrasil.Services
{
    public interface IGameService : IService
    {

        void Run();

        void OnApplicationFocus(bool focus)
        {
        }
    }
}