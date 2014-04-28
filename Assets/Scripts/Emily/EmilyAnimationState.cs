using Assets.Scripts.Character;
using Flai;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(CharacterController2D))]
	public class EmilyAnimationState : FlaiScript
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
        }
	}
}