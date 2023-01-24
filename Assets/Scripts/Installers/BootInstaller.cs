using UnityEngine.SceneManagement;
using Zenject;

public class BootInstaller : MonoInstaller, IInitializable
{
    private const string m_kSceneName = "Game";

    public override void InstallBindings()
    {
        BindInstallerInterfaces();
        BindInputService();
    }

    private void BindInstallerInterfaces()
    {
        Container.BindInterfacesTo<BootInstaller>().FromInstance(this).AsSingle();
    }

    private void BindInputService()
    {
        Container.Bind<IInputService>().To<DefaultInputService>().AsSingle().NonLazy();
    }

    public void Initialize()
    {
        if (SceneManager.GetActiveScene().name == m_kSceneName)
            return;

        SceneManager.LoadScene(m_kSceneName);
    }
}
