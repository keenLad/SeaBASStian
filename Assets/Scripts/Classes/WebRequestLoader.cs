using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

public class WebRequestLoader : IAPILoader
{
    public async UniTask<string> Get(string path, CancellationToken token = default(CancellationToken))
    {
        string result;

        var request = UnityWebRequest.Get(path);

        await request.SendWebRequest().WithCancellation(token);

        if(request.result != UnityWebRequest.Result.Success)
        {
            throw new Exception($"[WebRequestLoader] can not load data from {path} error code: {request.responseCode}");
        }

        result = request.downloadHandler.text;

        return result;
    }
}
