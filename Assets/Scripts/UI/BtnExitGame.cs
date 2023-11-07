using Game.Core;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.UI
{
    [RequireComponent(typeof(Button))]
    public class BtnExitGame : MonoBehaviour
    {
        [Inject] private InjectController _injectController;

        [SerializeField] private Button _button;

        private void Awake()
        {
            _injectController.AddUIItem(new InjectItem(Constants.ExitBtn, _button));
        }
    }
}