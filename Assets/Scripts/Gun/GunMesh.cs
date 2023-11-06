using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gun
{
    public class GunMesh : MonoBehaviour
    {
        [SerializeField] private Transform _startShootPoint;

        public Transform StartShootPoint => _startShootPoint;
    }
}
