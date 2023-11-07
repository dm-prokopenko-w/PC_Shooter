using Game.Core;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.Gun
{
    public class IconGunViewBtn : MonoBehaviour
    {
        [Inject] private InjectController _injectContr;

        [SerializeField] private Button _button;
        [SerializeField] private string _gunID;

        private void Awake()
        {
            _injectContr.AddUIItem(new InjectItem(Constants.SelectedGun + _gunID, _button, _gunID));
        }
    }
}
