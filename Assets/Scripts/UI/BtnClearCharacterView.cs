using Core;
using Game.Core;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.UI
{
    [RequireComponent(typeof(Button))]
    public class BtnClearCharacterView : MonoBehaviour
    {
        [Inject] private InjectController _injectController;

        [SerializeField] private Button _button;

        private void Awake()
        {
            _injectController.AddUIItem(new InjectItem(Constants.ClearCharacterViewBtn, _button));
        }
    }
}