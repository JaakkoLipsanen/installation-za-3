
using System.Runtime.InteropServices;
using Flai;
using Flai.General;
using Flai.Input;
using UnityEngine;

namespace Assets.Scripts.Guns
{
	public class WeaponShoot : FlaiScript
	{
	    public GameObject BulletPrefab;
	    public bool IsShotgun = false;

	    private float _timeSinceShot = 0f;
	    protected float ShotTime
	    {
	        get { return this.IsShotgun ? 0.35f : 0.2f; }
	    }

	    protected override void Update()
	    {
	        _timeSinceShot = FlaiMath.Min(this.ShotTime, _timeSinceShot + Time.deltaTime);
	        if (_timeSinceShot >= this.ShotTime && FlaiInput.IsButtonPressed("Shoot"))
	        {
	            _timeSinceShot -= this.ShotTime;
	            this.Shoot();
	        }
	    }

	    private void Shoot()
	    {
	        const float ShotgunRotationStep = 10;

	        int count = this.IsShotgun ? 3 : 1;
            // HACKKK... Unfortunately, because of the mirroring hack (scale.X = -1), the Rotation2D is incorrect. so we must adjust it here
	        float rotation = this.Rotation2D < 90 || this.Rotation2D > 270 ? this.Rotation2D : (270 - this.Rotation2D + 90);
	        if (this.IsShotgun)
	        {
	            rotation -= ShotgunRotationStep;
	        }

	        for (int i = 0; i < count; i++)
	        {
	            var bullet = this.BulletPrefab.Instantiate(this.Position2D, rotation);
	            bullet.SetLayer("PlayerBullets");
	            rotation += ShotgunRotationStep;
	        }
	    }
	}
}