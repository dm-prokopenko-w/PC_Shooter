using Game.Core;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Game.Character
{
    public abstract class CharacterItem
    {
        public CharacterController CharController => _view.GetCharacterController();
        public Transform CamTrans => _view.GetCameraTransform();
        public Transform GroundCheck => _view.GetGroundCheck();
        public Transform ParentMesh => _view.GetParentMesh();
        public Transform GunTrans => _gunTrans;
        public Transform ViewTrans => _view.transform;
        public Image HPImage => _view.GetHPImage();
        public TextMeshProUGUI HPText => _view.GetHPText();

        public Transform StartShoot { get; set; }
        public string GunID { get; set; }
        public float CurrentHP;
        public float MaxHP;

        protected Animator _anim;
        protected CharacterView _view;
        protected Vector3 _direction;
        protected Quaternion _rot;

        protected float _speedWalk;
        protected float _speedRun;
        protected float _jumpHeight;
        protected LayerMask _groundMask;
        protected Transform _gunTrans;
        protected NavMeshAgent _agent;

        public CharacterItem(CharacterView view, CharacterMesh mesh, CharactersParametrs parm)
        {
            _view = view;
            _anim = mesh.CharacterAnim;
            _gunTrans = mesh.GunTrans;
            _speedWalk = parm.SpeedWalk;
            _speedRun = parm.SpeedRun;
            _groundMask = parm.GroundMask;
            _jumpHeight = parm.JumpHeight;
            _agent = view.GetNavMeshAgent();
        }

        public void InitHP(float maxHP)
        {
            MaxHP = maxHP;
            CurrentHP = maxHP;
            SetHP(0);
        }

        public void SetHP(float hp)
        {
            CurrentHP += hp;
            HPImage.fillAmount = (CurrentHP + hp) / MaxHP;
            HPText.text = CurrentHP + "/" + MaxHP;
        }

        public virtual void Update()
        {
        }
    }

    public class Player : CharacterItem
    {
        public int CountBullet => _countBullet;
        public Vector3 Target => new Vector3(CharController.transform.position.x, CharController.center.y, CharController.transform.position.z);
        private int _countBullet;

        private float _speed;
        private bool _isGround;
        private Vector3 _direction;

        private float _rotationY = 0;
        private float _sensitivity;

        private float _minimumY = -60f;
        private float _maximumY = 60f;

        public Player(CharacterView view, CharacterMesh mesh, CharactersParametrs parm) : base(view, mesh, parm)
        {
            Camera.main.transform.SetParent(CamTrans);
            Camera.main.transform.position = CamTrans.position;
            Camera.main.transform.rotation = CamTrans.rotation;
            _agent.enabled = false;
        }

        public void SetParm(PlayerParametrs p)
        {
            _sensitivity = p.MouseSensitivity;
            _countBullet = p.BulletOnStart;
        }

        public void UpdateCountBullet(int count) => _countBullet += count;

        public override void Update()
        {
            if (Input.GetKey("left shift"))
            {
                _anim.SetFloat("Y", 2);

                _speed = _speedRun;
            }
            else
            {
                _speed = _speedWalk;
            }

            _isGround = Physics.CheckSphere(GroundCheck.position, Constants.GroundDistance, _groundMask);

            if (CharController.isGrounded)
            {
                _anim.SetFloat("X", Input.GetAxis("Horizontal"));
                _anim.SetFloat("Y", Input.GetAxis("Vertical"));
            }

            if (_isGround && _direction.y < 0)
            {
                _direction.y = -2f;
            }

            float hor = Input.GetAxis("Horizontal");
            float ver = Input.GetAxis("Vertical");


            if (Input.GetKeyDown(KeyCode.Space) && _isGround)
            {
                _direction.y = Mathf.Sqrt(_jumpHeight * Constants.Gravity);
                _anim.SetTrigger("Jump");
            }

            Vector3 move = ViewTrans.right * hor + ViewTrans.forward * ver;

            CharController.Move(move * _speed * Time.deltaTime);

            _direction.y -= Constants.Gravity * Time.deltaTime;

            CharController.Move(_direction * Time.deltaTime);


            ViewTrans.Rotate(0, Input.GetAxis("Mouse X") * _sensitivity, 0);

            _rotationY += Input.GetAxis("Mouse Y") * _sensitivity;

            _rotationY = Mathf.Clamp(_rotationY, _minimumY, _maximumY);

            CamTrans.localEulerAngles = new Vector3(-_rotationY, CamTrans.localEulerAngles.y, 0);
        }
    }

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
            : base(view, mesh, parm){}

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
