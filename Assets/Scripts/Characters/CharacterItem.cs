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
        public Transform ViewTrans => _view.transform;
        public Image HPImage => _view.GetHPImage();
        public TextMeshProUGUI HPText => _view.GetHPText();

        public Animator CharacterAnimator { get; set; }
        protected CharacterView _view;
        protected Vector3 _direction;
        protected Quaternion _rot;

        private float _maxHP;
        private float _currentHP;

        public CharacterItem(CharacterView view)
        {
            _view = view;
        }

        public void Init(float maxHP)
        {
            _maxHP = maxHP;
            _currentHP = maxHP;
            HPImage.fillAmount = 1f;
            HPText.text = _currentHP + "/" + maxHP;
        }

        public void SetHP(float hp)
        {
            _currentHP += hp;
            HPImage.fillAmount = (_currentHP + hp)/ _maxHP;
            HPText.text = _currentHP + "/" + _maxHP;
        }

        public virtual void Update()
        {
        }
    }

    public class Player : CharacterItem
    {
        public Player(CharacterView view) : base(view)
        {
            Camera.main.transform.SetParent(CamTrans);
            Camera.main.transform.position = CamTrans.position;
            Camera.main.transform.rotation = CamTrans.rotation;
        }

        public override void Update()
        {
        }
    }

    public class Enemy : CharacterItem
    {
        private float _minTimeAwait = 2f;
        private float _maxTimeAwait = 4f;
        private float _timeAwait = 0;
        private float _currentTime = 0;

        public Enemy(CharacterView view) : base(view)
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
