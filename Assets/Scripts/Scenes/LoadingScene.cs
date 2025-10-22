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

public class LoadingScene : MonoBehaviour
{
    private const string LOADING_SCENE_ERROR = "Scene not loaded";

    [SerializeField] private Slider _sliderProgress;
    [SerializeField] private TMP_Text _lblEerror;
    [SerializeField] private string _loadingSceneName = "MainScene";

    private async void Start()
    {
        await UniTask.Delay(2000);

        await LoadSceneAsync(_loadingSceneName);
    }

    private async UniTask LoadSceneAsync(string sceneName)
    {
        _sliderProgress.value = 0;

        _sliderProgress.gameObject.SetActive(true);
        _lblEerror.gameObject.SetActive(false);

        AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        await handle;

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            _sliderProgress.gameObject.SetActive(false);
            _lblEerror.text = LOADING_SCENE_ERROR;
            _lblEerror.gameObject.SetActive(true);
            return;
        }

        _sliderProgress.value = 0.5f;

        SceneInstance scene = handle.Result;


        var loadingScene = SceneManager.GetActiveScene();
        await scene.ActivateAsync();
        SceneManager.SetActiveScene(scene.Scene);

        await UniTask.Delay(1000);

        await SceneManager.UnloadSceneAsync(loadingScene);
    }

}
