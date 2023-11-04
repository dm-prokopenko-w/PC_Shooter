using AISystem;
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
        [Inject] private PlayerController _playerController;
        [Inject] private AIController _aiController;

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

                CharacterItem item;
                if (!id.Equals(ch.Id))
                {
                    item = new Enemy(obj);
                    //_aiController.Enemys.Add(item);
                    obj.name = ch.Id + " - Enemy";
                }
                else
                {
                    item = new Player(obj);
                    _playerController.PlayerItem = (Player)item;
                    obj.name = ch.Id + " - Player";
                }

                var mesh = Object.Instantiate(ch.Mesh, _containers[numContainer].position, _containers[numContainer].rotation);
                item.CharacterAnimator = mesh.CharacterAnim;
                item.Init(ch.HP);  
                mesh.transform.SetParent(item.ParentMesh);
                _containers.Remove(_containers[numContainer]);
                countSpawners--;
            }
        }
    }
}