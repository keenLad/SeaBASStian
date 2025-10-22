using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class RequestView : InitialisableBase
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private string _uri;

    override public async UniTask Init(CancellationToken token)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(_uri))
        {
            _label.text = "Requesting...";

            try
            {
                var op = request.SendWebRequest().WithCancellation(token);
                await op;
            }
            catch (Exception ex)
            {
                _label.text = $"Request error: {ex.Message}";
                return;
            }

            if (request.result != UnityWebRequest.Result.Success)
            {
                _label.text = $"Request error: {request.responseCode}";
                return;
            }

            _label.text = request.downloadHandler.text;

        }
    }
}
