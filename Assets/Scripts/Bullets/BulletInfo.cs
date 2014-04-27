using Assets.Scripts.General;
using Flai;
using UnityEngine;

namespace Assets.Scripts.Bullets
{
	public class BulletInfo : ExtendedMonoBehaviour
	{
	    public int MinDamage = 5;
	    public int MaxDamage = 10;
	    public GameObject Explosion;

	    protected void OnCollisionEnter2D(Collision2D collision)
	    {
	        Health health = collision.gameObject.Get<Health>();
	        if (health)
	        {
	            health.TakeDamage(Global.Random.Next(this.MinDamage, this.MaxDamage));
	        }

	        if (this.Explosion != null)
	        {
	            this.Explosion.Instantiate(this.Position2D, this.Rotation2D);
	        }
         
            this.DestroyGameObject();
	    }
	}
}