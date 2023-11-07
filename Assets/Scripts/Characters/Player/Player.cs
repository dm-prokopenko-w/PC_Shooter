using Game.Configs;
using Game.Core;
using UnityEngine;

namespace Game.Character
{
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
}