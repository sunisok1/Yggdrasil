namespace Framework.Yggdrasil.Services
{
    public interface IUIService : IService
    {
        void Open<T, TArgs>(TArgs args) where T : BaseUI<TArgs> where TArgs : CreateArgs;
        void Close<T>();
    }


    public abstract class BaseUI<T> : BaseObject<T> where T : CreateArgs
    {
    }

    public class FakeUI : BaseUI<FakeUI.FakeCreateArgs>
    {
        public class FakeCreateArgs : CreateArgs
        {
            public override string Path => "";
        }
    }
}