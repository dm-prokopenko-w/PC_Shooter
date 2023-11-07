using Game.Core;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.Character
{
    public class IconCharacterViewBtn : MonoBehaviour
    {
        [Inject] private InjectController _uiController;

        [SerializeField] private Button _button;
        [SerializeField] private string _characterID;

        private void Awake()
        {
            _uiController.AddUIItem(new InjectItem(Constants.GenerateBtn + _characterID, _button, _characterID));
        }
    }
}