using Flai;
using Flai.Diagnostics;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(PlayerController))]
	public class PlayerAnimationState : FlaiScript
	{
	    private PlayerController Controller
	    {
	        get { return this.Get<PlayerController>(); }
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