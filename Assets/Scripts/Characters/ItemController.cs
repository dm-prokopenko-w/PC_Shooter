using VContainer.Unity;
using UnityEngine;
using Game.Character;
using System.Collections.Generic;
using VContainer;
using Core;
using Game.Gun;
using System.Threading.Tasks;
using Game.Core;
using System;

namespace Game
{
    public class ItemController : IStartable, ITickable
    {
        [Inject] private GameplayManager _gameplayManager;
        [Inject] private GunSpawner _gunSpawner;
        [Inject] private InjectController _injectController;

        public Player _playerItem;
        public List<Enemy> _enemyItems = new List<Enemy>();
        public Action<int> OnUpdateCountBullet;

        protected float _speedWalk;
        protected float _speedRun;
        protected float _jumpHeight;
        protected float _sensitivity;
        protected LayerMask _groundMask;
        private Vector3 _aimPos;

        public void Start()
        {
            var aimTrans = _injectController.GetUIItemById(Constants.AimTrans);
            if (aimTrans != null)
            {
                _aimPos = aimTrans.RectTr.position;
            }
        }

        public async Task<Transform> AddedPlayer(Player player)
        {
            OnUpdateCountBullet?.Invoke(player.CountBullet);
            _playerItem = player;
            (Transform, string) value = await _gunSpawner.InitPlayer(_playerItem.GunTrans);
            _playerItem.GunID = value.Item2;
            return value.Item1;
        }

        public async Task<Transform> AddedEnemy(Enemy enemy)
        {
            _enemyItems.Add(enemy);
            (Transform, string) value = await _gunSpawner.InitEnemy(enemy.GunTrans);
            enemy.GunID = value.Item2;
            return value.Item1;
        }

        public void AddedBullet(int value)
        {
            _playerItem.UpdateCountBullet(value);
            OnUpdateCountBullet?.Invoke(_playerItem.CountBullet);
        }

        private void RemoveEnemy(Enemy enemy)
        {
            enemy.ViewTrans.gameObject.SetActive(false);

            //добавиить спецефект сметри
            _enemyItems.Remove(enemy);
            if (_enemyItems.Count <= 0)
            {
                _gameplayManager.Win();
            }
        }

        public void Tick()
        {
            if (_gameplayManager.IsPause) return;

            if (_playerItem != null)
            {
                _playerItem.Update();

                if (_playerItem.CurrentHP <= 0)
                {
                    _gameplayManager.Lose();
                }
            }

            if (_enemyItems != null)
            {
                if (_enemyItems.Count > 0)
                {
                    foreach (Enemy enemy in _enemyItems)
                    {
                        enemy.Update();
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (_playerItem.CountBullet <= 0) return;

                AddedBullet(-1);

                Ray ray = Camera.main.ScreenPointToRay(_aimPos);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    var enemy = _enemyItems.Find(x => x.PlayerController == hit.collider);
                    if (enemy != null)
                    {
                        int dam = _gunSpawner.GetValueDamage(_playerItem.GunID);
                        enemy.SetHP(-dam);

                        if (enemy.CurrentHP <= 0)
                        {
                            RemoveEnemy(enemy);
                        }
                    }
                }
            }
        }
    }
}