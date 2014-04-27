using Flai;
using UnityEngine;

namespace Assets.Scripts.General
{
    [RequireComponent(typeof(BoxCollider2D))]
	public class SetGravityScale : FlaiScript
    {
        public float GravityScale = 0.5f;
	    protected override void OnTriggerEnter2D(Collider2D other)
	    {
	        if (other.rigidbody2D != null)
	        {
	            other.rigidbody2D.gravityScale = this.GravityScale;
	        }
	    }
	}
}