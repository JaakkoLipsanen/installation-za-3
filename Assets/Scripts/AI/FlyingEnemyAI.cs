using Flai;
using Flai.Diagnostics;
using Flai.General;
using Flai.Scene;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class FlyingEnemyAI : FlaiScript
    {
        public GameObject BulletPrefab;

        private GameObject _player;
        private Vector2f _velocity = Vector2f.Zero;

        private Timer _shootTimer = new Timer(1.5f);
        private Timer _generateNewTargetPositionTimer = new Timer(5);
        private Vector2f _targetPosition;

        protected override void Awake()
        {
            _generateNewTargetPositionTimer = new Timer(5 + Global.Random.NextFloat(-1.5f, 1.5f));
            _player = Scene.Find("Player");
            this.GenerateTargetPosition();
        }

        protected override void Update()
        {
            this.Control();
            this.UpdateTargetIfNeeded();
            this.ShootIfPossible();
        }

        private void Control()
        {
            const float MaxSpeed = 3f;

            Vector2f direction = Vector2f.NormalizeOrZero(_targetPosition - this.Position2D);
            _velocity += direction * Time.deltaTime * 4;
            _velocity = Vector2f.ClampLength(_velocity, MaxSpeed);

            this.Position2D += _velocity * Time.deltaTime;
        }

        private void ShootIfPossible()
        {
            const float MaxShootDistance = 7;
            _shootTimer.Update();
            if (_shootTimer.HasFinished && Vector2f.Distance(this.Position2D, _player.GetPosition2D()) < MaxShootDistance)
            {
                _shootTimer.Restart();
                this.Shoot();
            }
        }

        private void Shoot()
        {
            var target = _player.GetPosition2D() + _player.rigidbody2D.velocity.ToVector2f() * 0.1f;
            float rotation = FlaiMath.GetAngleDeg(target - this.Position2D);
            var bullet = this.BulletPrefab.Instantiate(this.Position2D, rotation);
            bullet.SetLayer("EnemyBullets");
        }

        private void UpdateTargetIfNeeded()
        {
            _generateNewTargetPositionTimer.Update();
            if (_generateNewTargetPositionTimer.HasFinished)
            {
                this.GenerateTargetPosition();
                _generateNewTargetPositionTimer.Restart();
            }
        }

        private void GenerateTargetPosition()
        {
            const int TestCounts = 5;
            LayerMaskF ignoreLayers = LayerMaskF.FromNames("Enemies", "Player", "EnemyBullets", "PlayerBullets").Inverse;
            for (int i = 0; i < TestCounts; i++)
            {
                _targetPosition = _player.GetPosition2D() + new Vector2f(Global.Random.NextFloat(-7, 7), Global.Random.NextFloat(3f, 5f));
                if (!Physics2D.Raycast(this.Position2D, _targetPosition, Vector2f.Distance(this.Position2D, _targetPosition) + 2f, ignoreLayers))
                {
                    return;
                }
            }
        }
    }
}