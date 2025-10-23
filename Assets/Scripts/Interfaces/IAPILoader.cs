
using System.Threading;
using Cysharp.Threading.Tasks;

public interface IAPILoader
{
    UniTask<string> Get(string path, CancellationToken token = default(CancellationToken));
    UniTask<T> Get<T>(string path, CancellationToken token = default(CancellationToken));
}

