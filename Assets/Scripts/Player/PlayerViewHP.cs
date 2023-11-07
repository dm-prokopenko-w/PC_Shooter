using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class PlayerViewHP : MonoBehaviour
{
    [Inject] private CharactersController _chController;

    [SerializeField] private Image _hp;
    [SerializeField] private TextMeshProUGUI _hpText;

    private void Start()
    {
        _chController.OnUpdatePlayerHP += UpdateHP;
    }

    private void OnDestroy()
    {
        _chController.OnUpdatePlayerHP -= UpdateHP;
    }

    private void UpdateHP(float cur, float max)
    {
        _hp.fillAmount = cur / max;
        _hpText.text = cur + "/" + max;
    }
}
