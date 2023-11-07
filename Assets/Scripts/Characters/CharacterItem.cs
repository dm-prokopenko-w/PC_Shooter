using Game.Configs;
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
}