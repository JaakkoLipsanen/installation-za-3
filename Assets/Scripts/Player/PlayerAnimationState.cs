using Assets.Scripts.Character;
using Flai;
using Flai.Diagnostics;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(CharacterController2D))]
	public class PlayerAnimationState : FlaiScript
	{
        private CharacterController2D Controller
	    {
            get { return this.Get<CharacterController2D>(); }
	    }

        protected override void Awake()
        {
        }

        protected override void LateUpdate()
        {
            this.Get<Animator>().SetBool("IsRunning", this.Controller.IsRunning);
            this.Get<Animator>().SetBool("IsOnGround", this.Controller.IsOnGround);
            this.Get<Animator>().SetFloat("VelocityY", this.Controller.Velocity.Y);
        }
	}
}