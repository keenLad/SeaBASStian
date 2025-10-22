using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SceneInitialiser : MonoBehaviour
{
    [SerializeField] private List<InitialisableBase> _items;

    private async void Start()
    {

        var cancelation = new CancellationToken();
        await Init(cancelation);
    }

    public async UniTask Init(CancellationToken cancelation)
    {
        await UniTask.WhenAll(_items.Select(async i => await i.Init(cancelation)));
        Debug.Log("[SceneInitialiser] Scene initied");
    }
}
