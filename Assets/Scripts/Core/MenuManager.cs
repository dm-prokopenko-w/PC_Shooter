using Core;
using Game.Character;
using UnityEngine.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Core
{
    public class MenuManager : IStartable
    {
        [Inject] private SceneLoader _sceneLoader;
        [Inject] private CharacterGenerator _characterGenerator;
        [Inject] private InjectController _injectController;

        private Button _buttonPlay;

        public void Start()
        {
            var item = _injectController.GetUIItemById(Constants.SceneLoaderBtn);
            if (item != null)
            {
                _buttonPlay = item.Btn;
                item.Btn.onClick.AddListener(() => LoadScene(item.Num));
                _characterGenerator.OnChangeCharacter += ChangeCharacter;
            }
        }

        private void ChangeCharacter(CharacterSave character)
        {
            _buttonPlay.interactable = character != null;
        }

        private void LoadScene(int sceneIndex)
        {
            _sceneLoader.LoadScene(sceneIndex);
        }
    }
}