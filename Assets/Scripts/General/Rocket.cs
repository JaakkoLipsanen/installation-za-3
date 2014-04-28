using Flai;
using Flai.Scene;
using Flai.Scripts;
using UnityEngine;

namespace Assets.Scripts.General
{
    public class Rocket : FlaiScript
    {
        private bool _isActive = false;
        private float _timeSinceStart = 0;
        private float _speed = 0;

        private bool _isFadingOut = false;
        private TargetFollower _cameraTargetFollower;

        public void StartFlight()
        {
            _isActive = true;
            Camera.main.Get<TargetFollower>().Target = null;
        }

        protected override void Update()
        {
            if (!_isActive)
            {
                return;
            }

            _timeSinceStart += Time.deltaTime;
            _speed += _timeSinceStart * Time.deltaTime * 0.1f;

            this.Position2D += Vector2f.UnitY * _speed;
            Camera.main.SetPosition2D(Vector2f.Lerp(Camera.main.GetPosition2D(), this.Position2D, Time.deltaTime * 20));
            if (_timeSinceStart > 10.5f && !_isFadingOut)
            {
                SceneFader.Fade(SceneDescription.FromName("EndingScene"), Fade.Create(1f), Fade.Create(2.5f));
                _isFadingOut = true;
            }
        }
    }
}