using Flai;
using Flai.Diagnostics;
using Flai.General;
using Flai.Scripts;
using UnityEngine;

namespace Assets.Scripts.General
{
    [RequireComponent(typeof(OnTopMover))]
    public class Elevator : FlaiScript
    {
        private Timer _timeSinceOnTop = new Timer(1f);
        private OnTopMover _onTopMover;
        private float _progress = 0;
        private float _initialY;
        public VerticalDirection Direction = VerticalDirection.Up;
        public float Distance = 5;
        public float Speed = 5;

        private float _currentSpeed = 0f;

        protected override void Awake()
        {
            _onTopMover = this.Get<OnTopMover>();
            _initialY = this.Position2D.Y;
            Ensure.True(this.Distance > 0);
        }

        protected override void FixedUpdate()
        {
            float targetSpeed = 0f;
            if (_onTopMover.HasAny && _progress < this.Distance)
            {
                FlaiDebug.Log("h");
                _timeSinceOnTop.Restart();
                targetSpeed = this.Speed;
            }
            else if (!_onTopMover.HasAny && _progress > 0)
            {
                _timeSinceOnTop.Update();
                if (_timeSinceOnTop.HasFinished)
                {
                    targetSpeed = -this.Speed;
                }
            }

            FlaiDebug.Log("TS: " + targetSpeed);
            _currentSpeed += Time.deltaTime * 4 * FlaiMath.Sign(targetSpeed - _currentSpeed);
            _currentSpeed = FlaiMath.Clamp(_currentSpeed, -this.Speed, this.Speed);
            _progress = FlaiMath.Clamp(_progress + _currentSpeed * Time.deltaTime, 0, this.Distance);
            this.Position2D = new Vector2f(this.Position2D.X, _initialY + _progress * this.Direction.ToInt());
        }
    }
}