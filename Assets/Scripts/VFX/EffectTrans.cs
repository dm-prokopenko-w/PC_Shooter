using Core;
using Game.Core;
using UnityEngine;
using VContainer;

namespace Game
{
    public class EffectTrans : MonoBehaviour
    {
        [Inject] private InjectController _injectController;

        private void Awake()
        {
            _injectController.AddUIItem(new InjectItem(Constants.EffectInactiveTrans, transform));
        }
    }
}