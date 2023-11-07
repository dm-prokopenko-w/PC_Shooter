using VContainer.Unity;
using VContainer;
using Game.Configs;
using Game.Core;

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
            builder.Register<ScoreSystem>(Lifetime.Scoped).As<ScoreSystem, IStartable>(); 
        }
    }
}