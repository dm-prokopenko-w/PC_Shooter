using Core;
using Game.Configs;
using Game.Core;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using static UnityEditor.PlayerSettings;

namespace Game
{
    public class ControllerVFX : IStartable
    {
        [Inject] private ConfigsLoader _configsLoader;
        [Inject] private InjectController _injectController;

        private List<GameObject> _effects = new List<GameObject>();
        private GameObject _shootPrefab;
        private GameObject _bloodPrefab;
        private GameObject _diedPrefab;
        private Transform _parent;

        public async void Start()
        {
            var data = await _configsLoader.LoadConfig(Constants.Data) as AllConfig;
            _shootPrefab = data.EffectParm.ShootPrefab;
            _bloodPrefab = data.EffectParm.BloodPrefab;
            _diedPrefab = data.EffectParm.DiedPrefab;

            var trans = _injectController.GetUIItemById(Constants.EffectInactiveTrans);

            if (trans != null)
            {
                _parent = trans.Tr;
            }
        }

        public void SpawnShootEffect(Vector3 pos) => Spawn(pos, _shootPrefab);

        public void SpawnBloodEffect(Vector3 pos) => Spawn(pos, _bloodPrefab);
        public void SpawnDiedEffect(Vector3 pos) => Spawn(pos, _diedPrefab);

        private void Spawn(Vector3 pos, GameObject prefab)
        {
            if (_effects.Count > 0)
            {
                var activeObj = _effects.Find(x => prefab.name.Equals(x.name));
                if (activeObj != null && !activeObj.activeSelf)
                {
                    activeObj.transform.position = pos;
                    activeObj.SetActive(true);
                    return;
                }
            }

            var obj = Object.Instantiate(prefab);
            obj.transform.position = pos;
            obj.name = prefab.name;
            _effects.Add(obj);
            obj.transform.SetParent(_parent);
        }
    }
}