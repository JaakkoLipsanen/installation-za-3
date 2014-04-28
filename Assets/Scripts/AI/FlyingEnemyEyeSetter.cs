using Flai;
using Flai.Diagnostics;
using Flai.Scene;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class FlyingEnemyEyeSetter : FlaiScript
    {
        private GameObject _player;
        public float EyeSize = 0.2f;

        private GameObject _leftEye;
        private GameObject _rightEye;

        private Vector2f _leftEyeCenter;
        private Vector2f _rightEyeCenter;

        protected override void Awake()
        {
            _player = Scene.Find("Player");
            _leftEye = this.GetChild("Left Eye");
            _rightEye = this.GetChild("Right Eye");

            _leftEyeCenter = _leftEye.GetLocalPosition2D();
            _rightEyeCenter = _rightEye.GetLocalPosition2D();
        }

        protected override void LateUpdate()
        {
            var direction = Vector2f.NormalizeOrZero(_player.GetPosition2D() - this.Position2D);
            var localPosition = direction * this.EyeSize / 4f;

            float halfEyeSize = this.EyeSize/2f;
            RectangleF rectangle = RectangleF.CreateCentered(localPosition, halfEyeSize);
            if (rectangle.Right > halfEyeSize)
            {
                rectangle.Right = halfEyeSize + 0.1f;
            }
            else if (rectangle.Left < -halfEyeSize)
            {
                rectangle.Left = -halfEyeSize;
            }

            if (rectangle.Top > halfEyeSize)
            {
                rectangle.Top = halfEyeSize;
            }
            else if (rectangle.Bottom < -halfEyeSize)
            {
                rectangle.Bottom = -halfEyeSize;
            }

            FlaiDebug.DrawRectangleOutlines(rectangle.AsOffsetted(this.Position2D + _leftEyeCenter), ColorF.Red);
            FlaiDebug.DrawRectangleOutlines(RectangleF.CreateCentered(this.Position2D + _leftEyeCenter, this.EyeSize), ColorF.Blue);

            localPosition = rectangle.Center;
            _leftEye.SetLocalPosition2D(_leftEyeCenter + localPosition);
            _rightEye.SetLocalPosition2D(_rightEyeCenter + localPosition);
        }
    }
}