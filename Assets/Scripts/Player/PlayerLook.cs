using Flai;
using Flai.Input;

namespace Assets.Scripts.Player
{
    public class PlayerLook : FlaiScript
    {
        protected override void Update()
        {
            Vector2f delta = FlaiInput.MousePositionInWorld2D - this.Position2D;
            if (delta.X > 0)
            {
                this.Scale2D = Vector2f.Abs(this.Scale2D);
            }
            else
            {
                this.Scale2D = Vector2f.Abs(this.Scale2D) * new Vector2f(-1, 1);
            }
        }
    }
}