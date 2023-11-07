using Game.Core;
using UnityEngine;
using VContainer;

namespace Game
{
    public class PauseMenuPanel : MonoBehaviour
    {
        [Inject] private InjectController _injectController;

        [SerializeField] private Transform _trans;

        private void Awake()
        {
            _injectController.AddUIItem(new InjectItem(Constants.PauseMenuPanelTrans, _trans));
        }
    }
}