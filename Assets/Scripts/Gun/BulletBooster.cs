using Game.Character;
using Game.Configs;
using Game.Core;
using UnityEngine;
using VContainer;

namespace Game.Gun
{
    public class BulletBooster : MonoBehaviour
    {
        [Inject] private CharactersController _chController;
        [Inject] private ConfigsLoader _configsLoader;

        [SerializeField] private GameObject _view;

        private Vector3 _rot = new Vector3(0,1,0);

        private int _minTimeAwait;
        private int _maxTimeAwait;
        private int _countAdded;

        private float _timeAwait = 0;
        private float _currentTime = 0;

        private bool _isUpdateView = false;

        private async void Start()
        {
            var data = await _configsLoader.LoadConfig(Constants.Data) as AllConfig;

            _minTimeAwait = data.BulParm.MinSecRespawn;
            _maxTimeAwait = data.BulParm.MaxSecRespawn;
            _countAdded = data.BulParm.CountAddedBullet;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_isUpdateView) return;

            if (other.tag.Equals("Player"))
            {
                _isUpdateView = true;
                _chController.AddedBullet(_countAdded);
                _view.SetActive(false);
            }
        }

        private void Update()
        {
            transform.Rotate(_rot);

            if (!_isUpdateView) return;
            _currentTime += Time.deltaTime;

            if (_currentTime > _timeAwait)
            {
                _isUpdateView = false;
                _view.SetActive(true);

                _timeAwait = UnityEngine.Random.Range(_minTimeAwait, _maxTimeAwait);
                _currentTime = 0;
            }
        }
    }
}
