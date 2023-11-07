using Game.Core;
using Game.Gun;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class GameplayManager : IStartable, ITickable
    {
        [Inject] private InjectController _injectController;
        [Inject] private GunSpawner _gunSpawner;
        [Inject] private ScoreSystem _scoreSystem;
        [Inject] private SceneLoader _sceneLoader;

        public bool IsPause
        {
            get
            {
                if (_isblockPausemenu) return true;
                if(_isPause) return true;
                return false;
            }
        }

        private bool _isblockPausemenu = false;
        private bool _isPause = false;

        private GameObject _winPanel;
        private GameObject _losePanel;

        private GameplayMenu _gameplayMenu;

        public void Start()
        {
            _gameplayMenu = new GameplayMenu();

            var resumeGameBtn = _injectController.GetUIItemById(Constants.ResumeGameBtn);
            if (resumeGameBtn != null)
            {
                resumeGameBtn.Btn.onClick.AddListener(Play);
            }

            var winPanel = _injectController.GetUIItemById(Constants.WinPanelTrans);
            if (winPanel != null)
            {
                _winPanel = winPanel.Tr.gameObject;
                _winPanel.SetActive(false);
            }

            var losePanel = _injectController.GetUIItemById(Constants.LosePanelTrans);
            if (losePanel != null)
            {
                _losePanel = losePanel.Tr.gameObject;
                _losePanel.SetActive(false);
            }

            var aimTrans = _injectController.GetUIItemById(Constants.AimTrans);
            if (aimTrans != null)
            {
                _gameplayMenu.AimTrans = aimTrans.RectTr;
                _gameplayMenu.AimTrans.gameObject.SetActive(true);
            }

            var pauseMenuTrans = _injectController.GetUIItemById(Constants.PauseMenuPanelTrans);
            if (pauseMenuTrans != null)
            {
                _gameplayMenu.PauseMenuTrans = pauseMenuTrans.Tr;
                _gameplayMenu.PauseMenuTrans.gameObject.SetActive(false);
            }

            Cursor.lockState = CursorLockMode.Locked;
            _injectController.AddedActionOnClick(Constants.SceneLoaderBtn, _sceneLoader.LoadMenuScene);
        }

        public void Win()
        {
            Cursor.lockState = CursorLockMode.Confined;
            _isblockPausemenu = true;
            _winPanel.SetActive(true);
            _scoreSystem.Win();
        }

        public void Lose()
        {
            Cursor.lockState = CursorLockMode.Confined;
            _isblockPausemenu = true;
            _losePanel.SetActive(true);
            _scoreSystem.Lose();
        }

        private void Play()
        {
            _isPause = false;
            _gameplayMenu.Play();
            _gunSpawner.SpawnPlayerGun();
        }

        public void Tick()
        {
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                if (_isblockPausemenu) return;
                _isPause = true;
                _gameplayMenu.Pause();
            }
        }
    }

    public class GameplayMenu
    {
        public Transform AimTrans;
        public Transform PauseMenuTrans;

        public void Pause()
        {
            Cursor.lockState = CursorLockMode.Confined;
            AimTrans.gameObject.SetActive(false);
            PauseMenuTrans.gameObject.SetActive(true);
        }

        public void Play()
        {
            Cursor.lockState = CursorLockMode.Locked;
            AimTrans.gameObject.SetActive(true);
            PauseMenuTrans.gameObject.SetActive(false);
        }
    }
}
