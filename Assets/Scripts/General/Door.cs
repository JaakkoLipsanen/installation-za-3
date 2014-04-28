using Flai;
using Flai.Diagnostics;
using Flai.Scripts;
using UnityEngine;

namespace Assets.Scripts.General
{
    public class Door : Response
    {
        private bool _isOn = false;
        public override void Execute()
        {
            _isOn = true;
        }

        public override void ExecuteOff()
        {
            _isOn = false;
        }

        protected override void Update()
        {
            if (_isOn)
            {
                float targetLocalY = this.BoxCollider2D.size.y;
                if (this.LocalPosition2D.Y < targetLocalY)
                {
                    float newY = this.LocalPosition2D.Y + Time.deltaTime * 2f * FlaiMath.Sign(targetLocalY - this.LocalPosition2D.Y);
                    if (newY > targetLocalY)
                    {
                        newY = targetLocalY;
                    }

                    this.LocalPosition2D = new Vector2f(this.LocalPosition2D.X, newY);
                }
            }
            else
            {
                float targetLocalY = 0;
                if (this.LocalPosition2D.Y > targetLocalY)
                {
                    float newY = this.LocalPosition2D.Y - Time.deltaTime*2f; 
                    if (newY < targetLocalY)
                    {
                        newY = targetLocalY;
                    }

                    this.LocalPosition2D = new Vector2f(this.LocalPosition2D.X, newY);
                }
            }
        }
    }
}