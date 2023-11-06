using Core;
using Game.Core;
using UnityEngine;
using VContainer;

namespace Game
{
    public class BalletContainerTrans : MonoBehaviour
    {
        [Inject] private InjectController _injectController;

        [SerializeField] private Transform _trans;

        private void Awake()
        {
            _injectController.AddUIItem(new InjectItem(Constants.BulletContainerTrans, _trans));
        }
    }
}