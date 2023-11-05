using VContainer.Unity;
using UnityEngine;
using Game.Character;
using System.Collections.Generic;
using VContainer;
using Core;
using Game.Gun;

namespace Game
{
    public class ItemController : ITickable
    {
        [Inject] private GameplayManager _gameplayManager;
        [Inject] private GunSpawner _gunSpawner;

        public Player _playerItem;
        public List<Enemy> _enemyItems = new List<Enemy>();

        protected float _speedWalk;
        protected float _speedRun;
        protected float _jumpHeight;
        protected float _sensitivity;
        protected LayerMask _groundMask;

        public void AddedPlayer(Player player)
        {
            _playerItem = player;
            _gunSpawner.Init(_playerItem.GunTrans);
        }

        public void AddedEnemy(Enemy enemy)
        {
            _enemyItems.Add(enemy);
            _gunSpawner.Init(enemy.GunTrans);
        }

        public virtual void Tick()
        {
            if(_gameplayManager.IsPause) return;

            if (_playerItem != null)
            {
                _playerItem.Update();
            }

            if(_enemyItems != null)
            {
                if(_enemyItems.Count > 0)
                {
                    foreach (Enemy enemy in _enemyItems)
                    {
                        enemy.Update();
                    }
                }
            }
        }
    }
}