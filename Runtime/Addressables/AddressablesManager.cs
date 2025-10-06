using System.Threading.Tasks;
using RedHeadToolz;
using RedHeadToolz.Debugging;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace RedHeadToolz
{
    // [CreateAssetMenu(fileName = "AddressablesManager", menuName = "RedHeadToolz/Managers/AddressablesManager")]
    public class AddressablesManager : BaseManager
    {
        public override async Task<InitializationStatus> Init()
        {
            RHTebug.Log("Initializing Addressables Manager");
            // await Addressables.InitializeAsync().Task;
            return await base.Init();
        }

        public async Task<T> LoadAsset<T>(string name) where T : class
        {
            var handle = UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<T>(name);

            if (handle.Status.Equals(AsyncOperationStatus.Succeeded))
            {
                RHTebug.Log($"Asset {name} already loaded");
#if RELEASE
                return;
#endif
            }

            var result = await handle.Task;
            if (result == null)
            {
                RHTebug.LogError($"Failed to load asset with name {name}!");
            }
            return result;
        }

        public async Task<T> LoadAssetByGUID<T>(string guid) where T : class
        {
            var handle = UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<T>(guid);

            if (handle.Status.Equals(AsyncOperationStatus.Succeeded))
            {
                RHTebug.Log($"Asset with GUID {guid} already loaded");
#if RELEASE
                return;
#endif
            }

            var result = await handle.Task;
            if (result == null)
            {
                RHTebug.LogError($"Failed to load asset with GUID {guid}!");
            }
            return result;
        }

        public async Task ReleaseAsset<T>(T asset) where T : class
        {
            await Task.Run(() => UnityEngine.AddressableAssets.Addressables.Release(asset));
        }

        public async Task<SceneInstance> LoadScene(string sceneName)
        {
            var handle = UnityEngine.AddressableAssets.Addressables.LoadSceneAsync(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive);

#if RELEASE
            if (handle.Status.Equals(AsyncOperationStatus.Succeeded))
            {
                RHTebug.Log($"Scene {sceneName} already loaded");
                return handle.Result;
            }
#endif

            RHTebug.Log($"here");
            return await handle.Task;
        }

        public async Task UnloadScene(SceneInstance scene)
        {
            var handle = UnityEngine.AddressableAssets.Addressables.UnloadSceneAsync(scene);
            await handle.Task;
        }
    }
}