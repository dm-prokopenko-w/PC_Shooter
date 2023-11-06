using Core;
using Game.Core;
using UnityEngine;
using VContainer;

public class LoseMenu : MonoBehaviour
{
    [Inject] private InjectController _injectController;

    [SerializeField] private Transform _trans;

    private void Awake()
    {
        _injectController.AddUIItem(new InjectItem(Constants.LosePanelTrans, _trans));
    }
}
