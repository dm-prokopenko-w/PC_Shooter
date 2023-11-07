using Core;
using Game.Configs;
using Game.Core;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Character
{
    public class CharactersSpawner : IStartable
    {
        [Inject] private ConfigsLoader _configsLoader;
        [Inject] private SaveManager _saveManager;
        [Inject] private InjectController _injectController;
        [Inject] private CharactersController _chController;

        private List<Transform> _containers = new List<Transform>();

        public async void Start()
        {
            var data = await _configsLoader.LoadConfig(Constants.Data) as AllConfig;
            var countSpawners = data.CountSpawners;

            for (int i = 1; i <= countSpawners; i++)
            {
                var container = _injectController.GetUIItemById(Constants.CharacterSpawnerTrans + i);
                _containers.Add(container.Tr);
            }

            var save = _saveManager.Load<CharacterSave>(Constants.CharacterKey);
            string id = save == null ? data.Characters[0].Id : save.Id;

            foreach (var ch in data.Characters)
            {
                int numContainer = Random.Range(0, countSpawners);
                CharacterView obj = Object.Instantiate(data.CharacterPrefab, _containers[numContainer].position, _containers[numContainer].rotation);
                obj.transform.SetParent(_containers[numContainer]);
                var mesh = Object.Instantiate(ch.Mesh, _containers[numContainer].position, _containers[numContainer].rotation);

                CharacterItem item;
                Transform startPoint;
                if (!id.Equals(ch.Id))
                {
                    item = new Enemy(obj, mesh, data.CharactersParm);
                    startPoint = await _chController.AddedEnemy((Enemy)item, data.EnemyParm, ch.HP);
                    obj.name = ch.Id + " - Enemy";
                    obj.tag = "Enemy";
                }
                else
                {
                    item = new Player(obj, mesh, data.CharactersParm);
                    startPoint = await _chController.AddedPlayer((Player)item, data.PlayerParm, ch.HP);
                    obj.name = ch.Id + " - Player";
                    obj.tag = "Player";
                }
                item.StartShoot = startPoint;
                mesh.transform.SetParent(item.ParentMesh);
                _containers.Remove(_containers[numContainer]);
                countSpawners--;
            }
        }
    }
}