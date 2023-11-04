using UnityEngine;

namespace Game.Character
{
    public class CharacterMesh : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _gunTrans;

        public Animator CharacterAnim => _animator;
        public Transform GunTrans => _gunTrans;
    }
}