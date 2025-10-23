using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

public class RequestView : InitialisableBase
{
    [SerializeField] private TMP_Text _label;

    [Inject]
    IAPILoader _loader;
    [Inject]
    ConfigDTO _config;

    override public async UniTask Init(CancellationToken token)
    {
        _label.text = "Initialising...";
        await UniTask.WaitUntil(() => _loader != null, cancellationToken: token);

        Debug.Log($"[RequestView] config loaded: {_config}");
        _label.text = "Requesting...";

        string result;
        try
        {
            result = await _loader.Get(_config.apiUrl, token);
        }
        catch (Exception ex)
        {
            result = $"[RequestView] load from {_config.apiUrl} filed with error: {ex.Message}";
        }

        _label.text = result;

    }
}
