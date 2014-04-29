using Assets.Scripts.General;
using Flai;
using Flai.Input;
using Flai.Scene;

namespace Assets.Scripts.Player
{
    public class PlayerLook : FlaiScript
    {
        private bool _isFading = false;
        private Health _health;

        protected override void Awake()
        {
            _health = this.Get<Health>();
        }

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

            if (!_isFading &&  !_health.IsAlive)
            {
                SceneFader.Fade(Scene.CurrentSceneDescription, Fade.Create(), Fade.Create());
                _isFading = true;
            }
        }
    }
}