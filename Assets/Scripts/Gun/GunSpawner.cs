using Core;
using Game.Configs;
using Game.Core;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace Game.Gun
{
    public class GunSpawner
    {
        [Inject] private InjectController _injectController;
        [Inject] private ConfigsLoader _configsLoader;

        private List<ItemPool> _pools = new List<ItemPool>();
        private List<GunConfig> _gunConfigs;
        private ItemPool _currentItem;
        private string _currentId;
        private Transform _playerGunTrans;

        private Vector3 _gunPos = new Vector3(0, 0.2f,0);
        private Vector3 _gunRot = new Vector3(0, 180,-90);

        public async void Init(Transform parent)
        {
            _playerGunTrans = parent;

            var data = await _configsLoader.LoadConfig(Constants.Data) as AllConfig;
            _gunConfigs = new List<GunConfig>(data.Guns);

            var preview = _injectController.GetUIItemById(Constants.GunViewInPauseTrans);

            foreach (var gun in _gunConfigs)
            {
                var gunBtn = _injectController.GetUIItemById(Constants.SelectedGun + gun.Id);

                if (gunBtn != null && preview != null)
                {
                    gunBtn.Btn.onClick.AddListener(() => SpawnIconPreview(gunBtn.Des, preview.Icon));
                }
            }

            var id = PlayerPrefs.GetString(Constants.PlayerGunId);
            if (string.IsNullOrEmpty(id))
            {
                _currentId = _gunConfigs[0].Id;
            }
            else
            {
                _currentId = id;
            }

            SpawnGun(_currentId, parent);
        }

        private void SpawnIconPreview(string id, Image preview)
        {
            var item = _gunConfigs.Find(x => x.Id.Equals(id));
            if (item != null)
            {
                _currentId = item.Id;
                preview.sprite = item.Icon;
            }
        }


        public void SpawnPlayerGun()
        {
            if (_currentItem != null)
            {
                _currentItem.Prefab.SetActive(false);
            }

            var item = _pools.Find(x => x.Id.Equals(_currentId));
            if (item == null)
            {
                var obj = SpawnGun(_currentId, _playerGunTrans);
                var ch = new ItemPool(_currentId, obj.gameObject);
                _currentItem = ch;
                _pools.Add(ch);
            }
            else
            {
                Debug.Log(2);
                item.Prefab.SetActive(true);
                _currentItem = item;
            }
        }

        public GunMesh SpawnGun(string id, Transform parent)
        {
            var gun = _gunConfigs.Find(x => x.Id.Equals(id));
            if (gun == null) return null;
            Debug.Log(1);

            GunMesh obj = Object.Instantiate(gun.Mesh);
            obj.name = id;
            obj.gameObject.SetActive(true);

            obj.transform.SetParent(parent);
            obj.transform.localPosition = _gunPos;
            obj.transform.rotation = parent.rotation;
            obj.transform.Rotate(_gunRot);
            return obj;
        }
    }
}
