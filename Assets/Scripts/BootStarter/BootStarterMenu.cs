using Game.Character;
using Game.Core;
using VContainer;
using VContainer.Unity;

namespace Game.BootStarters
{
    public class BootStarterMenu : BootStarter
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.Register<CharacterGenerator>(Lifetime.Scoped).As<CharacterGenerator, IStartable>();
            builder.Register<MenuManager>(Lifetime.Scoped).As<MenuManager, IStartable>();
        }
    }
}