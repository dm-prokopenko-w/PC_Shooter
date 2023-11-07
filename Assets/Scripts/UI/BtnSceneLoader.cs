using Game.Core;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.UI
{
    [RequireComponent(typeof(Button))]
    public class BtnSceneLoader : MonoBehaviour
    {
        [Inject] private InjectController _injectController;

        [SerializeField] private int _sceneIndex;
        [SerializeField] private Button _button;

        private void Awake()
        {
            _injectController.AddUIItem(new InjectItem(Constants.SceneLoaderBtn, _button, _sceneIndex));
        }
    }
}