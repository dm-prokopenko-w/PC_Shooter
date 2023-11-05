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

        public bool IsPause;

        private GameplayMenu _gameplayMenu;

        public void Start()
        {
            _gameplayMenu = new GameplayMenu();

            var resumeGameBtn = _injectController.GetUIItemById(Constants.ResumeGameBtn);
            if (resumeGameBtn != null)
            {
                resumeGameBtn.Btn.onClick.AddListener(Play);
            }

            var aimTrans = _injectController.GetUIItemById(Constants.AimTrans);
            if (aimTrans != null)
            {
                _gameplayMenu.AimTrans = aimTrans.Tr;
                _gameplayMenu.AimTrans.gameObject.SetActive(true);
            }

            var pauseMenuTrans = _injectController.GetUIItemById(Constants.PauseMenuPanelTrans);
            if (pauseMenuTrans != null)
            {
                _gameplayMenu.PauseMenuTrans = pauseMenuTrans.Tr;
                _gameplayMenu.PauseMenuTrans.gameObject.SetActive(false);
            }

        }

        private void Play()
        {
            IsPause = false;
            _gameplayMenu.Play();
            _gunSpawner.SpawnPlayerGun();
        }

        public void Tick()
        {
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                IsPause = true;
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
