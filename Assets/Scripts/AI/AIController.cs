using Game;
using Game.Character;
using Game.Configs;
using Game.Core;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace AISystem
{
    public class AIController : ItemController
    {
        public void Tick()
        {
        }
    }

    public class AIItem
    {
        public string Id;
        private float _minTimeAwait = 2f;
        private float _maxTimeAwait = 4f;
        private float _timeAwait = 0;
        private float _currentTime = 0;
        private List<AIState> _aiStates => new List<AIState>
        {
            new IdleAIState(),
            new AttackAIState(),
            new UpgradeBaseAIState()
        };

        public void UpdateAI()
        {
            _currentTime += Time.deltaTime;

            if (_currentTime > _timeAwait)
            {
                _timeAwait = Random.Range(_minTimeAwait, _maxTimeAwait);
                _currentTime = 0;
                var numActiveState = Random.Range(0, _aiStates.Count);
            }
        }
    }

}
