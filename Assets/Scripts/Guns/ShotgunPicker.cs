using Assets.Misc;
using Assets.Scripts.Guns;
using Flai;
using UnityEngine;

namespace Assets.Scripts
{
	public class ShotgunPicker : FlaiScript
    {
	    protected override void OnTriggerEnter2D(Collider2D other)
	    {
	        if (JayaHelper.IsPlayer(other.gameObject))
	        {
	            other.GetChild("Gun").Get<WeaponShoot>().IsShotgun = true;
                this.DestroyGameObject();
	        }
	    }
	}
}