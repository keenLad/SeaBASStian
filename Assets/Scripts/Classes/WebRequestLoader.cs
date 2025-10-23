using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequestLoader : IAPILoader
{
    public async UniTask<string> Get(string path, CancellationToken token = default(CancellationToken))
    {
        string result;

        Debug.Log($"[WebRequestLoader] connecting to {path}");

        var request = UnityWebRequest.Get(path);

        await request.SendWebRequest().WithCancellation(token);

        if(request.result != UnityWebRequest.Result.Success)
        {
            throw new Exception($"[WebRequestLoader] can not load data from {path} error code: {request.responseCode}");
        }

        result = request.downloadHandler.text;

        return result;
    }

    public async UniTask<T> Get<T>(string path, CancellationToken token = default(CancellationToken))
    {
        var result = await Get(path, token);

        T converted = default(T);

        converted = JsonUtility.FromJson<T>(result);

        return converted;
    }
}
