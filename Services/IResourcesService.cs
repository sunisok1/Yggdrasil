using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Framework.Yggdrasil.Services
{
    public interface IResourcesService : IService
    {
        T Load<T>(string path) where T : Object;

        UniTask<T> LoadAsync<T>(string path) where T : Object;
    }
}