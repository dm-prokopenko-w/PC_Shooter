using AISystem;
using Game.Character;
using VContainer;
using VContainer.Unity;

namespace Game.BootStarters
{
    public class BootStarterGame : BootStarter
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.Register<PlayerController>(Lifetime.Scoped).As<PlayerController, IStartable, ITickable>();
            builder.Register<AIController>(Lifetime.Scoped).As<AIController, IStartable, ITickable>();
            builder.Register<CharactersSpawner>(Lifetime.Scoped).As<CharactersSpawner, IStartable>();
        }
    }
}