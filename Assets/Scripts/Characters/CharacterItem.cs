using Game.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Character
{
    public abstract class CharacterItem
    {
        public CharacterController PlayerController => _view.GetCharacterController();
        public Transform CamTrans => _view.GetCameraTransform();
        public Transform GroundCheck => _view.GetGroundCheck();
        public Transform ParentMesh => _view.GetParentMesh();
        public Transform GunTrans => _gunTrans;
        public Transform ViewTrans => _view.transform;
        public Image HPImage => _view.GetHPImage();
        public TextMeshProUGUI HPText => _view.GetHPText();

        protected Animator _anim;
        protected CharacterView _view;
        protected Vector3 _direction;
        protected Quaternion _rot;

        private float _maxHP;
        private float _currentHP;

        protected float _speedWalk;
        protected float _speedRun;
        protected float _jumpHeight;
        protected LayerMask _groundMask;
        protected float _sensitivity;
        protected Transform _gunTrans;

        public CharacterItem(CharacterView view, CharacterMesh mesh, CharactersParm parm)
        {
            _view = view;
            _anim = mesh.CharacterAnim;
            _gunTrans = mesh.GunTrans;
            _speedWalk = parm.SpeedWalk;
            _speedRun = parm.SpeedRun;
            _sensitivity = parm.MouseSensitivity;
            _groundMask = parm.GroundMask;
            _jumpHeight = parm.JumpHeight;

        }

        public void InitHP(float maxHP)
        {
            _maxHP = maxHP;
            _currentHP = maxHP;
            HPImage.fillAmount = 1f;
            HPText.text = _currentHP + "/" + maxHP;
        }

        public void SetHP(float hp)
        {
            _currentHP += hp;
            HPImage.fillAmount = (_currentHP + hp) / _maxHP;
            HPText.text = _currentHP + "/" + _maxHP;
        }

        public virtual void Update()
        {
        }
    }

    public class Player : CharacterItem
    {

        private float _speed;
        private bool _isGround;
        private Vector3 _direction;

        private float _rotationY = 0;

        private float _minimumY = -60f;
        private float _maximumY = 60f;

        public Player(CharacterView view, CharacterMesh mesh, CharactersParm parm) : base(view, mesh, parm)
        {
            Camera.main.transform.SetParent(CamTrans);
            Camera.main.transform.position = CamTrans.position;
            Camera.main.transform.rotation = CamTrans.rotation;
        }


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

            if (PlayerController.isGrounded)
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
                Debug.Log(1);
                _direction.y = Mathf.Sqrt(_jumpHeight * Constants.Gravity);
                _anim.SetTrigger("Jump");

            }

            Vector3 move = ViewTrans.right * hor + ViewTrans.forward * ver;

            PlayerController.Move(move * _speed * Time.deltaTime);

            _direction.y -= Constants.Gravity * Time.deltaTime;

            PlayerController.Move(_direction * Time.deltaTime);


            ViewTrans.Rotate(0, Input.GetAxis("Mouse X") * _sensitivity, 0);

            _rotationY += Input.GetAxis("Mouse Y") * _sensitivity;

            _rotationY = Mathf.Clamp(_rotationY, _minimumY, _maximumY);

            CamTrans.localEulerAngles = new Vector3(-_rotationY, CamTrans.localEulerAngles.y, 0);

        }
    }

    public class Enemy : CharacterItem
    {
        private float _minTimeAwait = 2f;
        private float _maxTimeAwait = 4f;
        private float _timeAwait = 0;
        private float _currentTime = 0;

        public Enemy(CharacterView view, CharacterMesh mesh, CharactersParm parm) : base(view, mesh, parm)
        {
        }

        public override void Update()
        {
            base.Update();

            _currentTime += Time.deltaTime;

            if (_currentTime > _timeAwait)
            {
                _timeAwait = Random.Range(_minTimeAwait, _maxTimeAwait);
                _currentTime = 0;
            }
        }

    }
}
