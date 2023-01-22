using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        InstallInterfaces();
    }
    private void InstallInterfaces()
    {
        Container.Bind(typeof(IBaseGridFactory)).To<ModelRendererFactory>().AsSingle().NonLazy();
        Container.Bind(typeof(ITurn)).To<Player>().AsSingle().NonLazy();
        Container.Bind<IInputManager>().To<InputManager>().AsSingle();
    }
}