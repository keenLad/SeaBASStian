using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class InitialisableBase : MonoBehaviour, IInitialisable
{
    public virtual async UniTask Init(CancellationToken token) {

    }
}
