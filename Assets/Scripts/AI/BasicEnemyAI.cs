using Assets.Scripts.Character;
using Assets.Scripts.General;
using Flai;
using Flai.General;
using UnityEngine;

namespace Assets.Scripts.AI
{
    [RequireComponent(typeof(CharacterController2D))]
    public class BasicEnemyAI : EnemyAI
    {
        private Timer _attackTimer = new Timer(0.75f);
        private Timer _timeUntilNextJump = new Timer(0.1f);

        protected override void Update()
        {
            this.Control();
            this.AttackIfPossible();
        }

        private void Control()
        {
            var deltaPosition = _player.GetPosition2D() - this.Position2D;
            if (FlaiMath.Abs(deltaPosition.X) > 0.05f)
            {
                if (deltaPosition.X > 0)
                {
                    _characterController.MoveRight();
                    _characterController.FacingDirection = HorizontalDirection.Right;
                }
                else
                {
                    _characterController.MoveLeft();
                    _characterController.FacingDirection = HorizontalDirection.Left;
                }
            }

            _timeUntilNextJump.Update();
            if (deltaPosition.Y > 1 && Global.Random.NextFromOdds(0.1f)) // || Physics2D.Raycast(this.Position2D, _player.GetPosition2D(), float.MaxValue, LayerMaskF.FromNames("Bullets").Inverse))
            {
                if (_timeUntilNextJump.HasFinished)
                {
                    _characterController.Jump();
                    _timeUntilNextJump.Restart();
                }
            }
        }

        private void AttackIfPossible()
        {
            _attackTimer.Update();
            if (_attackTimer.HasFinished && PhysicsHelper.Intersects(this.collider2D, _player.collider2D))
            {
                _attackTimer.Restart();
                _player.Get<Health>().TakeDamage(3);
            }
        }
    }
}