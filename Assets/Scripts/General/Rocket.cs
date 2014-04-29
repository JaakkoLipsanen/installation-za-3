using Assets.Misc;
using Flai;
using Flai.Input;
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

        private bool _enterable = false;
        private bool _isPlayerInsideTrigger = false;
        private bool _hasEntered = false;

        public GenericEvent EscapeAction;

        public void StartFlight()
        {
            if (_isActive)
            {
                return;
            }

            _isActive = true;
            Camera.main.Get<TargetFollower>().Target = null;
            this.GetComponentsInChildren<ParticleSystem>().ForEach(ps => ps.Play());
        }

        protected override void Update()
        {
            if (_enterable && _isPlayerInsideTrigger && !_hasEntered)
            {
                if (FlaiInput.IsNewKeyPress(KeyCode.E))
                {
                    Scene.Find("Player").renderer.enabled = false;
                    Scene.Find("Emily").renderer.enabled = false;
                    this.StartFlight();
                    _hasEntered = true;
                    this.GetChild("EscapeText").Get<MeshRenderer>().enabled = false;
                }
            }

            if (!_isActive)
            {
                return;
            }

            _timeSinceStart += Time.deltaTime;
            _speed += _timeSinceStart * Time.deltaTime * 0.1f;

            this.Position2D += Vector2f.UnitY * _speed;
            Camera.main.SetPosition2D(Vector2f.Lerp(Camera.main.GetPosition2D(), this.Position2D, 0.016f * 20));
            if (_timeSinceStart > 10.5f && !_isFadingOut)
            {
                SceneFader.Fade(SceneDescription.FromName("EndingScene"), Fade.Create(1f), Fade.Create(2.5f));
                _isFadingOut = true;
            }
        }

        public void SetEnterable()
        {
            _enterable = true;
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (_enterable && JayaHelper.IsPlayer(other.gameObject) && !_hasEntered)
            {
                _isPlayerInsideTrigger = true;
                this.GetChild("EscapeText").Get<MeshRenderer>().enabled = true;
            }
        }

        protected override void OnTriggerExit2D(Collider2D other)
        {
            if (_enterable && JayaHelper.IsPlayer(other.gameObject) && !_hasEntered)
            {
                _isPlayerInsideTrigger = false;
                this.GetChild("EscapeText").Get<MeshRenderer>().enabled = false;
            }
        }
    }
}