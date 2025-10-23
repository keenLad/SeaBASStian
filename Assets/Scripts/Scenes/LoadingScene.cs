using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.UI;
using System.Linq;
using System.Threading;
using UnityEngine.SceneManagement;
using Zenject;

public class LoadingScene : MonoBehaviour
{
    private const string LOADING_SCENE_ERROR = "Scene not loaded";

    [SerializeField] private Slider _sliderProgress;
    [SerializeField] private TMP_Text _lblError;
    [SerializeField] private string _loadingSceneName = "MainScene";

    [Inject]
    private DiContainer _container;

    private async void Start()
    {
        await LoadSceneAsync(_loadingSceneName);
    }

    private async UniTask LoadSceneAsync(string sceneName)
    {
        _sliderProgress.value = 0;

        _sliderProgress.gameObject.SetActive(true);
        _lblError.gameObject.SetActive(false);

        AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!handle.IsDone)
        {
            _sliderProgress.value = handle.PercentComplete * 0.5f;
            await UniTask.Yield();
        }

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            _sliderProgress.gameObject.SetActive(false);
            _lblError.text = LOADING_SCENE_ERROR;
            _lblError.gameObject.SetActive(true);
            return;
        }

        _sliderProgress.value = 0.5f;

        SceneInstance loadedScene = handle.Result;

        var loadingScene = SceneManager.GetActiveScene();
        await loadedScene.ActivateAsync();
        _sliderProgress.value = 0.7f;

        foreach (var rootObject in loadedScene.Scene.GetRootGameObjects())
        {
            _container.InjectGameObject(rootObject);
        }
        _sliderProgress.value = 0.9f;

        SceneManager.SetActiveScene(loadedScene.Scene);

        await UniTask.Delay(1000);
        _sliderProgress.value = 0.9f;

        await SceneManager.UnloadSceneAsync(loadingScene);
    }

}
