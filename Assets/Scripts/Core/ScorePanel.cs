using Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VContainer;

namespace Game
{

    public class ScorePanel : MonoBehaviour
    {
        [Inject] private ScoreSystem _scoreSystem;

        [SerializeField] private TextMeshProUGUI _win;
        [SerializeField] private TextMeshProUGUI _lose;

        private void Start()
        {
            var score = _scoreSystem.GetSaveScore();
            if (score != null)
            {
                _win.text = "Win - " + score.WinScore;
                _lose.text = "Lose - " + score.LoseScore;
            }
            else
            {
                _win.text = "Win - " + 0;
                _lose.text = "Lose - " + 0;
            }
        }
    }
}
