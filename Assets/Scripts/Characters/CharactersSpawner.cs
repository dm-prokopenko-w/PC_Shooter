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

        public async void Start()
        {
            var data = await _configsLoader.LoadConfig(Constants.Data) as AllConfig;
            var countSpawners = data.CountSpawners;

            List<Transform> containers = new List<Transform>();

            for (int i = 1; i <= countSpawners; i++)
            {
                var container = _injectController.GetUIItemById(Constants.CharacterSpawnerTrans + i);
                containers.Add(container.Tr);
            }

            var save = _saveManager.Load<CharacterSave>(Constants.CharacterKey);
            string id = save == null ? data.Characters[0].Id : save.Id;

            foreach (var ch in data.Characters)
            {
                int numContainer = Random.Range(0, countSpawners);

                CharacterView obj = Object.Instantiate(data.CharacterPrefab, containers[numContainer].position, containers[numContainer].rotation);
                obj.transform.SetParent(containers[numContainer]);
                var mesh = Object.Instantiate(ch.Mesh, containers[numContainer].position, containers[numContainer].rotation);

                CharacterItem item;
                Transform startPoint;

                if (!id.Equals(ch.Id))
                {
                    item = new Enemy(obj, mesh, data.CharactersParm);
                    startPoint = await _chController.AddedEnemy((Enemy)item, data.EnemyParm, ch.HP);
                    obj.name = ch.Id + " - " + Constants.EnemyTag;
                    obj.tag = Constants.EnemyTag;
                }
                else
                {
                    item = new Player(obj, mesh, data.CharactersParm);
                    startPoint = await _chController.AddedPlayer((Player)item, data.PlayerParm, ch.HP);
                    obj.name = ch.Id + " - " + Constants.PlayerTag;
                    obj.tag = Constants.PlayerTag;
                }

                item.StartShoot = startPoint;

                mesh.transform.SetParent(item.ParentMesh);

                containers.Remove(containers[numContainer]);
                countSpawners--;
            }
        }
    }
}