using Flai;

namespace Assets.Scripts.General
{
	public class JayaTilemap : FlaiScript
	{
	    protected override void Awake()
	    {
	        switch (this.GameObject.name)
	        {
                case "Background":
	                this.renderer.sortingOrder = -1500;
                    break;

                case "Foreground":
	                this.renderer.sortingOrder = 1;
                    break;
	        }
	    }
	}
}