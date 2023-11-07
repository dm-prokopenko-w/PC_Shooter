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
using Game.Configs;
using static UnityEditor.PlayerSettings;

namespace Game
{
    public class CharactersController : IStartable, ITickable
    {
        [Inject] private GameplayManager _gameplayManager;
        [Inject] private GunSpawner _gunSpawner;
        [Inject] private InjectController _injectController;
        [Inject] private ConfigsLoader _configsLoader;
        [Inject] private ControllerVFX _vfx;

        public Player PlayerItem;
        public List<Enemy> EnemyItems = new List<Enemy>();
        public Action<int> OnUpdateCountBullet;
        public Action<float, float> OnUpdatePlayerHP;

        protected float _speedWalk;
        protected float _speedRun;
        protected float _jumpHeight;
        protected float _sensitivity;
        protected LayerMask _groundMask;
        private Vector3 _aimPos;
        private List<Vector3> _points = new List<Vector3>();

        public async void Start()
        {
            var aimTrans = _injectController.GetUIItemById(Constants.AimTrans);
            if (aimTrans != null)
            {
                _aimPos = aimTrans.RectTr.position;
            }

            var data = await _configsLoader.LoadConfig(Constants.Data) as AllConfig;
            var countSpawners = data.CountSpawners;

            for (int i = 1; i <= countSpawners; i++)
            {
                var container = _injectController.GetUIItemById(Constants.CharacterSpawnerTrans + i);
                _points.Add(container.Tr.position);
            }

        }

        public async Task<Transform> AddedPlayer(Player player, PlayerParametrs data, float maxHp)
        {
            PlayerItem = player;

            player.InitHP(maxHp);
            OnUpdatePlayerHP?.Invoke(player.CurrentHP, maxHp);
            PlayerItem.SetParm(data);

            OnUpdateCountBullet?.Invoke(player.CountBullet);

            (Transform, string) value = await _gunSpawner.InitPlayer(PlayerItem.GunTrans);
            PlayerItem.GunID = value.Item2;
            return value.Item1;
        }

        public async Task<Transform> AddedEnemy(Enemy enemy, EnemyParametrs data, float maxHp)
        {
            EnemyItems.Add(enemy);

            enemy.InitHP(maxHp);
            enemy.SetParm(data, _points);

            (Transform, string) value = await _gunSpawner.InitEnemy(enemy.GunTrans);
            enemy.GunID = value.Item2;
            return value.Item1;
        }

        public void AddedBullet(int value)
        {
            PlayerItem.UpdateCountBullet(value);
            OnUpdateCountBullet?.Invoke(PlayerItem.CountBullet);
        }

        private void RemoveEnemy(Enemy enemy)
        {
            enemy.ViewTrans.gameObject.SetActive(false);

            _vfx.SpawnDiedEffect(enemy.CharController.transform.position);
            EnemyItems.Remove(enemy);
            if (EnemyItems.Count <= 0)
            {
                _gameplayManager.Win();
            }
        }

        public void Tick()
        {
            if (_gameplayManager.IsPause) return;

            if (PlayerItem != null)
            {
                PlayerItem.Update();

                if (PlayerItem.CurrentHP <= 0)
                {
                    _gameplayManager.Lose();
                }
            }

            if (EnemyItems != null)
            {
                if (EnemyItems.Count > 0)
                {
                    foreach (Enemy enemy in EnemyItems)
                    {
                        continue;
                        if (enemy.IsShoot)
                        {
                            int dam = _gunSpawner.GetValueDamage(enemy.GunID);
                            PlayerItem.SetHP(-dam);
                            _vfx.SpawnShootEffect(_gunSpawner.GetStartGunPoint(enemy.GunID));
                            OnUpdatePlayerHP?.Invoke(PlayerItem.CurrentHP, PlayerItem.MaxHP);
                            _vfx.SpawnBloodEffect(PlayerItem.CharController.transform.position);
                        }
                        enemy.SetPlayerPos(PlayerItem.Target);
                        enemy.Update();
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (PlayerItem.CountBullet <= 0) return;

                AddedBullet(-1);
                _vfx.SpawnShootEffect(_gunSpawner.GetStartGunPoint(PlayerItem.GunID));

                Ray ray = Camera.main.ScreenPointToRay(_aimPos);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    var enemy = EnemyItems.Find(x => x.CharController == hit.collider);
                    if (enemy != null)
                    {
                        int dam = _gunSpawner.GetValueDamage(PlayerItem.GunID);
                        enemy.SetHP(-dam);
                        _vfx.SpawnBloodEffect(hit.point);

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