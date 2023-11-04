using Core;
using Game.Core;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.UI
{
    [RequireComponent(typeof(Button))]
    public class BtnGenerate : MonoBehaviour
    {
        [Inject] private InjectController _injectController;

        [SerializeField] private Button _button;

        private void Awake()
        {
            _injectController.AddUIItem(new InjectItem(Constants.GenerateBtn, _button));
        }
    }
}