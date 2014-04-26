using Flai;
using Flai.Input;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerController : FlaiScript
    {
        private float _timeSinceNotInGround = float.MaxValue / 2f; // really big value at the start so that it is larger than JumpTimeBias
        private bool _isJumping = false;

        public bool ShowDebug = false;
        public bool IsControllingEnabled = true;

        public float HorizontalSpeedDrag = 0.85f; // meh name
        public float AccelerationPower = 20;
        public float SpeedAirDrag = 1f;
        public float Speed = 10;
        public float JumpForce = 750;
        public float JumpTimeBias = 0.1f;

        public bool IsOnGround
        {
            get
            {
                BoxCollider2D collider = (BoxCollider2D)this.collider2D;
                Vector2f center = this.Position2D + this.GroundDirection.ToUnitVector() * (collider.size.y / 2f + 0.01f);
                Vector2f left = center - Vector2f.UnitX * collider.size / 2f;
                Vector2f right = center + Vector2f.UnitX * collider.size / 2f;

                const float MaxDistance = 0.01f;
                Vector2f direction = this.GroundDirection.ToUnitVector();
                return Physics2D.Raycast(center, direction, MaxDistance) ||
                    Physics2D.Raycast(left, direction, MaxDistance) ||
                    Physics2D.Raycast(right, direction, MaxDistance);
            }
        }

        public bool IsRunning { get; private set; }

        public bool CanJump
        {
            get { return !_isJumping && (this.IsOnGround || _timeSinceNotInGround <= this.JumpTimeBias); }
        }

        public Vector2f Velocity
        {
            get { return this.rigidbody2D.velocity; }
        }

        public VerticalDirection GroundDirection
        {
            get
            {
                float gravity = Physics2D.gravity.y * this.rigidbody2D.gravityScale;
                return (gravity <= 0) ? VerticalDirection.Down : VerticalDirection.Up;
            }
        }

        public HorizontalDirection FacingDirection
        {
            get { return this.Scale.x > 0 ? HorizontalDirection.Right : HorizontalDirection.Left; }
            set { this.Scale2D = Vector2f.Abs(this.Scale2D) * new Vector2f(value == HorizontalDirection.Right ? 1 : -1, 1); }
        }

        protected override void Update()
        {
            if (this.IsOnGround)
            {
                _isJumping = false;
                _timeSinceNotInGround = 0;
            }
            else
            {
                _timeSinceNotInGround += Time.deltaTime;
            }

            this.Control();
        }

        private void Control()
        {
            rigidbody2D.velocity *= new Vector2f(this.HorizontalSpeedDrag, 1);

            float force = 0;
            if (this.IsControllingEnabled && FlaiInput.IsButtonOrKeyPressed("Move Left", KeyCode.A))
            {
                force -= this.Speed;
            }

            if (this.IsControllingEnabled && FlaiInput.IsButtonOrKeyPressed("Move Right", KeyCode.D))
            {
                force += this.Speed;
            }

            force *= this.IsOnGround ? 1 : this.SpeedAirDrag;
            rigidbody2D.velocity += Vector2f.UnitX.ToVector2() * force * Time.deltaTime * AccelerationPower;
            rigidbody2D.velocity = Vector2f.ClampX(rigidbody2D.velocity, -10, 10);

            if (force == 0 && this.IsOnGround)
            {
                rigidbody2D.velocity *= new Vector2f(0.7f, 1);
            }

            if (this.CanJump && this.IsControllingEnabled)
            {
                if (FlaiInput.IsNewButtonOrKeyPress("Jump", KeyCode.Space))
                {
                    this.Position2D -= this.GroundDirection.ToUnitVector() * 0.01f;
                    rigidbody2D.AddForce(-this.GroundDirection.ToUnitVector() * this.JumpForce);
                    _isJumping = true;
                }
            }

            // set facing direction
            if (force != 0)
            {
                // todo: not setting, messes the PlayerLook
               // this.FacingDirection = (force > 0) ? HorizontalDirection.Right : HorizontalDirection.Left;
            }

            // flip upside down if anti gravity
            int multiplier = (this.GroundDirection == VerticalDirection.Down) ? 1 : -1;
            this.Scale2D = new Vector2f(this.Scale2D.X, FlaiMath.Abs(this.Scale2D.Y) * multiplier);
            this.IsRunning = (force != 0);
        }
    }
}