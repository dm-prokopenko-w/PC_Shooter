using Core;
using Game.Core;
using UnityEngine;
using VContainer;

namespace Game.Character
{
    public class CharacterSpawnerTransform : MonoBehaviour
    {
        [Inject] private InjectController _injectController;

        private void Awake()
        {
            _injectController.AddUIItem(new InjectItem(Constants.CharacterSpawnerTrans + gameObject.name, transform));
        }
    }
}