using Game.Configs;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Character
{
    public class Enemy : CharacterItem
    {
        public bool IsShoot => _isShoot;
        public bool IsChangeState { get; set; }

        private List<Vector3> _points = new List<Vector3>();
        private Vector3 _playerPos;
        private float _distanceToPlayer;
        private float _detectionDist;
        private float _awaitSecBeforeShoot;
        private bool _isView = false;

        protected float _realAngel;
        protected float _viewAngel;
        protected float _rotationSpeed;

        private float _minTimeAwait = 5f;
        private float _maxTimeAwait = 15f;
        private float _timeAwait = 0;
        private float _currentTime = 0;
        protected bool _isShoot = false;
        protected bool _isEntTime = true;

        public Enemy(CharacterView view, CharacterMesh mesh, CharactersParametrs parm)
            : base(view, mesh, parm) { }

        public void SetParm(EnemyParametrs p, List<Vector3> points)
        {
            _awaitSecBeforeShoot = p.AwaitSecBeforeShoot;
            _detectionDist = p.DetectionDistance;
            _viewAngel = p.ViewAngel;
            _points = points;
            _rotationSpeed = p.RotationSpeed;
        }

        public void SetPlayerPos(Vector3 p) => _playerPos = p;

        public override void Update()
        {
            base.Update();

            _distanceToPlayer = Vector3.Distance(_playerPos, _agent.transform.position);

            if (_distanceToPlayer <= _detectionDist)
            {
                RotateToPlayer();
            }

            if (IsView())
            {
                if (_isView)
                {
                    if (_isShoot)
                    {
                        _isView = false;
                        _isShoot = false;

                        _isEntTime = true;
                        float awaitTime = UnityEngine.Random.Range(_minTimeAwait, _maxTimeAwait);
                        ResetTimer(awaitTime);
                    }
                    else
                    {
                        _isShoot = UpdateTimer();
                    }
                }
                else
                {
                    _isEntTime = true;
                    float awaitTime = UnityEngine.Random.Range(_minTimeAwait, _maxTimeAwait);
                    ResetTimer(awaitTime);

                    _agent.SetDestination(_agent.transform.position);
                    ResetTimer(_awaitSecBeforeShoot);
                    _isView = true;
                    _anim.SetTrigger("Idle");
                }

                return;
            }

            if (_isEntTime = UpdateTimer())
            {
                SetDestination();
            }
        }

        public bool UpdateTimer()
        {
            _currentTime += Time.deltaTime;

            if (_currentTime > _timeAwait)
            {
                _currentTime = 0;
                return true;
            }
            return false;
        }

        private void ResetTimer(float timeAwait)
        {
            _timeAwait = timeAwait;
            _currentTime = 0;
        }

        private void RotateToPlayer()
        {
            Vector3 lookVector = new Vector3(0, _playerPos.y - ViewTrans.transform.position.y, 0);

            if (lookVector == Vector3.zero) return;

            ViewTrans.LookAt(_playerPos, lookVector);
        }

        private bool IsView()
        {
            _realAngel = Vector3.Angle(ViewTrans.transform.forward, _playerPos - ViewTrans.transform.position);
            RaycastHit hit;

            if (Physics.Raycast(_gunTrans.position, _playerPos - _gunTrans.position, out hit))
            {
                Debug.DrawRay(_gunTrans.position, _playerPos - _gunTrans.position, Color.red);

                if (_realAngel < _viewAngel / 2f && hit.collider.tag.Equals("Player"))
                {
                    return true;
                }
            }

            return false;
        }

        private void SetDestination()
        {
            if (_points.Count > 0)
            {
                _anim.SetTrigger("Walk");

                var pointNum = UnityEngine.Random.Range(0, _points.Count);
                _agent.SetDestination(_points[pointNum]);
                float awaitTime = UnityEngine.Random.Range(_minTimeAwait, _maxTimeAwait);
                ResetTimer(awaitTime);
            }
        }
    }
}
