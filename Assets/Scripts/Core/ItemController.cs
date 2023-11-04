using Game.Configs;
using Game.Core;
using VContainer;
using VContainer.Unity;
using UnityEngine;

namespace Game
{
    public class ItemController :  IStartable, ITickable
    {
        [Inject] private ConfigsLoader _configsLoader;

        protected float _speedWalk;
        protected float _speedRun;
        protected float _jumpHeight;
        protected float _sensitivity;
        protected LayerMask _groundMask;

        public async void Start()
        {
            var data = await _configsLoader.LoadConfig(Constants.Data) as AllConfig;
            _speedWalk = data.SpeedWalk;
            _speedRun = data.SpeedRun;
            _sensitivity = data.MouseSensitivity;
            _groundMask = data.GroundMask;
            _jumpHeight = data.JumpHeight;
        }

        public virtual void Tick()
        {
        }
    }
}
