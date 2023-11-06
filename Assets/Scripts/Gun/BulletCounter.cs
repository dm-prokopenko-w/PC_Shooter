using TMPro;
using UnityEngine;
using VContainer;

namespace Game.Gun
{
    public class BulletCounter : MonoBehaviour
    {
        [Inject] private ItemController _itemController;

        [SerializeField] private TextMeshProUGUI _counter;

        private void Start()
        {
            _itemController.OnUpdateCountBullet += UpdateCounter;
        }

        private void OnDestroy()
        {
            _itemController.OnUpdateCountBullet -= UpdateCounter;
        }

        private void UpdateCounter(int count)
        {
            _counter.text = count.ToString();
        }
    }
}
