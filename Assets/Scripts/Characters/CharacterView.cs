using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Character
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] private Transform _hpTrans;
        [SerializeField] private Image _hp;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _camPos;
        [SerializeField] private Transform _parentMesh;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private TextMeshProUGUI _hpText;

        public Image GetHPImage() => _hp;
        public TextMeshProUGUI GetHPText() => _hpText;
        public CharacterController GetCharacterController() => _characterController;
        public Transform GetCameraTransform() => _camPos;
        public Transform GetGroundCheck() => _groundCheck;
        public Transform GetParentMesh() => _parentMesh;

        private void Update()
        {
            _hpTrans.rotation = Quaternion.LookRotation(_hpTrans.position - Camera.main.transform.position);
        }
    }
}