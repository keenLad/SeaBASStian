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
    [SerializeField] private string _uri;

    [Inject]
    IAPILoader _loader;

    override public async UniTask Init(CancellationToken token)
    {
        _label.text = "Initialising...";
        await UniTask.WaitUntil(() => _loader != null, cancellationToken: token);

        _label.text = "Requesting...";

        string result;
        try
        {
            result = await _loader.Get(_uri, token);
        }
        catch (Exception ex)
        {
            result = $"[RequestView] load from {_uri} filed with error: {ex.Message}";
        }

        _label.text = result;

    }
}
