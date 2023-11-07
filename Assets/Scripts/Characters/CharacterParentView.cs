using VContainer;
using UnityEngine;
using Game.Core;

namespace Game.Character
{
    public class CharacterParentView : MonoBehaviour
    {
        [Inject] private InjectController _injectController;

        private void Awake()
        {
            _injectController.AddUIItem(new InjectItem(Constants.CharacterViewTrans, transform));
        }
    }
}