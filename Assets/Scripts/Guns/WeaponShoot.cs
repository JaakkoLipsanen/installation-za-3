
using Flai;
using Flai.General;
using Flai.Input;
using UnityEngine;

namespace Assets.Scripts.Guns
{
	public class WeaponShoot : FlaiScript
	{
	    public GameObject BulletPrefab;

        private readonly Timer _timer = new Timer(0.2f);
	    protected override void Update()
	    {
            _timer.Update();
	        if (_timer.HasFinished && FlaiInput.IsButtonPressed("Shoot"))
	        {
	            _timer.Restart();
	            this.Shoot();
	        }
	    }

	    private void Shoot()
	    {
            // HACKKK... Unfortunately, because of the mirroring hack (scale.X = -1), the Rotation2D is incorrect. so we must adjust it here
	        float rotation = this.Rotation2D < 90 || this.Rotation2D > 270 ? this.Rotation2D : (270 - this.Rotation2D + 90);

            var bullet = this.BulletPrefab.Instantiate(this.Position2D, rotation);
            bullet.SetLayer("PlayerBullets");
	    }
	}
}