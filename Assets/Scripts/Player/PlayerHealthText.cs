using Assets.Scripts.General;
using Flai;
using Flai.Scene;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(TextMesh))]
    public class PlayerHealthText : FlaiScript
    {
        private TextMesh _textMesh;
        private Health _playerHealth;
        private PlayerRegen _regen;
        private float _alpha = 0f;
        public string Format = "Health: {0}%";

        private bool IsVisible
        {
            get { return !_playerHealth.IsFullHealth || _playerHealth.TimeSinceDamageTaken < 5; }
        }

        private ColorF TargetColor
        {
            get { return _regen.IsRegenOn ? ColorF.LightGreen : new ColorF(255, 66, 66); }
        }

        protected override void Awake()
        {
            _textMesh = this.Get<TextMesh>();
            _playerHealth = Scene.Find("Player").Get<Health>();
            _regen = _playerHealth.Get<PlayerRegen>();
        }

        protected override void LateUpdate()
        {
            _alpha = FlaiMath.Clamp(_alpha + (this.IsVisible ? 1 : -1) * Time.deltaTime, 0, 1);
            _textMesh.text = string.Format(this.Format, _playerHealth.CurrentHealth * 100 / _playerHealth.TotalHealth);
            _textMesh.color = ColorF.Lerp(_textMesh.color, this.TargetColor, 0.1f) * _alpha;
        }
    }
}