using UnityEngine;
using Zenject;

public class LoadingInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.Log("[LoadingInstaller] install bindings");
        Container.Bind<IAPILoader>().To<WebRequestLoader>().AsSingle().NonLazy();
        Debug.Log("[LoadingInstaller] installed bindings");
    }
}