using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class ConfigLoader : MonoBehaviour
{
    [Inject] private IAPILoader _loader;

    public async UniTask<ConfigDTO> LoadConfig(string configName)
    {
        await UniTask.WaitUntil(() => _loader != null);
        string configPath = Path.Combine(Application.streamingAssetsPath, configName);

#if UNITY_EDITOR
        configPath = "file://" + configPath;
#endif

        var configContent = await _loader.Get<ConfigDTO>(configPath);
        return configContent;
    }
}
