using Game.Core;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class ScoreSystem : IStartable
    {
        [Inject] private SaveManager _saveManager;

        private ScoreSave _save;

        public void Start()
        {
            _save = _saveManager.Load<ScoreSave>(Constants.ScoreKey);

            if (_save == null)
            {
                _save = new ScoreSave();
                _saveManager.Save(Constants.ScoreKey, _save);
            }
        }

        public ScoreSave GetSaveScore() => _save;

        public void Win()
        {
            _save.WinScore++;
            _saveManager.Save(Constants.ScoreKey, _save);
        }

        public void Lose()
        {
            _save.LoseScore++;
            _saveManager.Save(Constants.ScoreKey, _save);
        }
    }

    public class ScoreSave
    {
        public ScoreSave()
        {
            LoseScore = 0;
            WinScore = 0;
        }

        public int LoseScore;
        public int WinScore;
    }
}
