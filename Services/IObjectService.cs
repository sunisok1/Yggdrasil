using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Framework.Yggdrasil.Services
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ObjectAttribute : Attribute
    {
        public string Path { get; }

        public ObjectAttribute(string path)
        {
            Path = path;
        }
    }

    public abstract class CreateArgs
    {
        private class EmptyArg : CreateArgs
        {
            public override string Path => throw new System.NotImplementedException();
        }

        public static CreateArgs Empty => new EmptyArg();
        public abstract string Path { get; }
    }

    public abstract class BaseObject<T> : MonoBehaviour where T : CreateArgs
    {
        public virtual void OnCreated(T createArgs)
        {
        }
    }

    public class FakeObject : BaseObject<FakeObject.FakeArgs>
    {
        public class FakeArgs : CreateArgs
        {
            public override string Path => $"relative/{data}";

            private readonly int data;

            public FakeArgs(int data)
            {
                this.data = data;
            }
        }

        public override void OnCreated(FakeArgs createArgs)
        {
        }
    }

    public interface IObjectService : IService
    {
        T Create<T, TArgs>(Transform parent, TArgs args) where TArgs : CreateArgs where T : BaseObject<TArgs>;
        UniTask<T> CreateAsync<T, TArgs>(Transform parent, TArgs args) where TArgs : CreateArgs where T : BaseObject<TArgs>;
    }
}