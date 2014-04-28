using System.Collections.Generic;
using Flai;
using Flai.Diagnostics;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class CharacterController2D : FlaiScript
    {
        private float _timeInAir = float.MaxValue / 2f; // really big value at the start so that it is larger than JumpTimeBias
        private bool _isJumping = false;

        private int _moveDirection = 0;
        private bool _jumpNextFrame = false;
        private bool _didJumpThisFrame = false;

        public float HorizontalSpeedDrag = 0.85f; // meh name
        public float AccelerationPower = 20;
        public float SpeedAirDrag = 1f;
        public float Speed = 10;
        public float JumpForce = 750;
        public float JumpTimeBias = 0.1f;

   //     public bool UseGameObjectLayerIgnores = false;
        public List<string> IgnoredLayers = new List<string>(); 

        public bool IsOnGround
        {
            get
            {
                LayerMaskF IgnoreMask = LayerMaskF.FromNames(this.IgnoredLayers).Inverse;
                BoxCollider2D boxCollider = this.BoxCollider2D;
                Vector2f center = boxCollider.GetBoundsHack().Center + this.GroundDirection.ToUnitVector() * (boxCollider.size.y / 2f + 0.01f);
                Vector2f left = center - Vector2f.UnitX * boxCollider.size / 2f;
                Vector2f right = center + Vector2f.UnitX * boxCollider.size / 2f;

                const float MaxDistance = 0.01f;
                Vector2f direction = this.GroundDirection.ToUnitVector();
                return Physics2D.Raycast(center, direction, MaxDistance, IgnoreMask) ||
                    Physics2D.Raycast(left, direction, MaxDistance, IgnoreMask) ||
                    Physics2D.Raycast(right, direction, MaxDistance, IgnoreMask);
            }
        }

        public bool IsRunning { get; private set; }

        public bool CanJump
        {
            get { return !_isJumping && !_jumpNextFrame && (this.IsOnGround || _timeInAir <= this.JumpTimeBias); }
        }

        public Vector2f Velocity
        {
            get { return this.Rigidbody2D.velocity; }
        }

        public VerticalDirection GroundDirection
        {
            get
            {
                float gravity = Physics2D.gravity.y * this.Rigidbody2D.gravityScale;
                return (gravity <= 0) ? VerticalDirection.Down : VerticalDirection.Up;
            }
        }

        public HorizontalDirection FacingDirection
        {
            get { return this.Scale.x > 0 ? HorizontalDirection.Right : HorizontalDirection.Left; }
            set { this.Scale2D = Vector2f.Abs(this.Scale2D) * new Vector2f(value == HorizontalDirection.Right ? 1 : -1, 1); }
        }

        public void MoveLeft()
        {
            _moveDirection = FlaiMath.Clamp(_moveDirection - 1, -1, 1);
        }

        public void MoveRight()
        {
            _moveDirection = FlaiMath.Clamp(_moveDirection + 1, -1, 1);
        }

        public bool Jump()
        {
            if (this.CanJump)
            {
                _jumpNextFrame = true;
                return true;
            }

            return false;
        }

        protected override void Update()
        {
            this.UpdateTimeInAir();
            this.Control();
        }

        private void UpdateTimeInAir()
        {
            if (this.IsOnGround && (!_isJumping || this.Velocity.Y <= 0))
            {
                _isJumping = false;
                _timeInAir = 0;
            }
            else
            {
                _timeInAir += Time.deltaTime;
            }
        }

        private void Control()
        {
            this.ApplyHorizontalSpeedDrag();
            float force = this.CalculateHorizontalForce();
            this.ApplyHorizontalVelocity(force);

            if (force == 0 && this.IsOnGround)
            {
                this.Rigidbody2D.velocity *= new Vector2f(0.7f, 1);
            }

            if (_jumpNextFrame)
            {
                this.JumpInner();
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

            _jumpNextFrame = false;
            _moveDirection = 0;
        }

        private void ApplyHorizontalVelocity(float force)
        {
            this.Rigidbody2D.velocity += Vector2f.UnitX.ToVector2() * force * Time.deltaTime * this.AccelerationPower;
            this.Rigidbody2D.velocity = Vector2f.ClampX(this.Rigidbody2D.velocity, -10, 10); // todo: why 10??
        }

        private void ApplyHorizontalSpeedDrag()
        {
            this.Rigidbody2D.velocity *= new Vector2f(this.HorizontalSpeedDrag, 1);
        }

        private float CalculateHorizontalForce()
        {
            float force = _moveDirection * this.Speed;
            force *= this.IsOnGround ? 1 : this.SpeedAirDrag;
            return force;
        }

        private void JumpInner()
        {
            this.Position2D -= this.GroundDirection.ToUnitVector() * 0.01f;
            this.Rigidbody2D.AddForce(-this.GroundDirection.ToUnitVector() * this.JumpForce);
            _isJumping = true;
        }
    }
}