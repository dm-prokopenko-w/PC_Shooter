using Core;
using Game.Core;
using UnityEngine;
using VContainer;

namespace Game
{
    public class PauseMenuPanel : MonoBehaviour
    {
        [Inject] private InjectController _injectController;

        private void Awake()
        {
            _injectController.AddUIItem(new InjectItem(Constants.PauseMenuPanelTrans, transform));
        }
    }
}