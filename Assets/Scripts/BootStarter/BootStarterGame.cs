using Core;
using Game.Character;
using Game.Gun;
using VContainer;
using VContainer.Unity;

namespace Game.BootStarters
{
    public class BootStarterGame : BootStarter
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.Register<GameplayManager>(Lifetime.Scoped).As<GameplayManager, IStartable, ITickable>();
            builder.Register<ItemController>(Lifetime.Scoped).As<ItemController, ITickable>();
            builder.Register<CharactersSpawner>(Lifetime.Scoped).As<CharactersSpawner, IStartable>();
            builder.Register<GunSpawner>(Lifetime.Scoped);
        }
    }
}