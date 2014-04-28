using Assets.Scripts.Character;
using UnityEngine;

namespace Assets.Scripts.AI
{
    [RequireComponent(typeof(CharacterController2D))]
	public class FlyingEnemyAI : EnemyAI
	{
        protected override void Update()
        {
            this.Control();
        }

        private void Control()
        {
            const float TargetYDelta = 4;
            var deltaPosition = _player.GetPosition2D() - this.Position2D;
        }
	}
}