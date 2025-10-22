
using System.Threading;
using Cysharp.Threading.Tasks;

public interface IInitialisable
{
    UniTask Init(CancellationToken token);
}
