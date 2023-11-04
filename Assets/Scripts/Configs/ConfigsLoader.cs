using Game.Core;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Game.Configs
{
    public class ConfigsLoader
    {
        public async Task<Config> LoadConfig(string configId)
        {
            var handle = Addressables.LoadAssetAsync<Config>(Constants.ConfigsPath + configId);
            await handle.Task;
            return handle.Result;
        }
    }
}