using System.Collections.Generic;
using System.Linq;
using Flai;
using Flai.Scripts;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class FinalWave : FlaiScript
    {
        private const float WaveTime = 20;
        private bool _isStarted = false;
        private float _timeSinceStart = 0f;

        private bool _hasEnded = false;
        private bool _hasWentPastMidtime = false;

        private List<Response> _responses = new List<Response>();
        private int _currentIndex = -1;

        public event GenericEvent Midtime;
        public event GenericEvent Ended;

        private float EndTime
        {
            get { return (_responses.Count) * WaveTime - 5; }
        }

        public void StartWave()
        {
            _isStarted = true;
            _responses = this.GetAllChildren().Where(go => go.Has<Response>()).Select(go => go.Get<Response>()).OrderBy(r => r.GameObject.name).ToList();
        }

        protected override void Update()
        {
            if (!_isStarted)
            {
                return;
            }

            _timeSinceStart += Time.deltaTime;
            if (_timeSinceStart > this.EndTime && !_hasEnded)
            {
                _hasEnded = true;
                this.Ended.InvokeIfNotNull();
            }

            if (_timeSinceStart > this.EndTime/2f && !_hasWentPastMidtime)
            {
                _hasWentPastMidtime = true;
                this.Midtime.InvokeIfNotNull();
            }

            int realIndex = (int)(_timeSinceStart / WaveTime);
            while (_currentIndex < _responses.Count - 1 && _currentIndex < realIndex)
            {
                _currentIndex++;
                _responses[_currentIndex].Execute();
            }
        }
    }
}