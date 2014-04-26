using Flai;
using Flai.Input;

namespace Assets.Scripts.Guns
{
	public class WeaponLook : FlaiScript
	{
	    protected override void Update()
	    {
	        this.RotationDirection2D = FlaiInput.MousePositionInWorld2D - this.Position2D;
	        this.Rotation2D = FlaiMath.RealModulus(this.Rotation2D, 360);
	        if (this.Parent.GetScale2D().X < 0) // is the player flipped
	        {
	            if (this.Rotation2D > 90 && this.Rotation2D < 270)
	            {
	                this.Rotation2D = 270 - this.Rotation2D + 90;
	            }
	        } 
	    }
	}
}