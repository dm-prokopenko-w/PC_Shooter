using VContainer.Unity;
using VContainer;
using Game.Configs;
using Game.Core;
using Core;

namespace Game.BootStarters
{
    public class BootStarter : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<InjectController>(Lifetime.Scoped);
            builder.Register<SaveManager>(Lifetime.Scoped);
            builder.Register<ConfigsLoader>(Lifetime.Scoped);
            builder.Register<SceneLoader>(Lifetime.Scoped);
        }
    }
}