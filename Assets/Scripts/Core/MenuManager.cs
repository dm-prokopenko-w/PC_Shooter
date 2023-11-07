using Game.Character;
using UnityEngine;
using UnityEngine.UI;
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
            var play = _injectController.GetUIItemById(Constants.SceneLoaderBtn);
            if (play != null)
            {
                _buttonPlay = play.Btn;
                play.Btn.onClick.AddListener(() => LoadScene(play.Num));
                _characterGenerator.OnChangeCharacter += ChangeCharacter;
            }

            var quit = _injectController.GetUIItemById(Constants.QuitBtn);
            if (quit != null)
            {
                quit.Btn.onClick.AddListener(QuitGame);
            }
        }

        private void QuitGame()
        {
             Application.Quit();
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