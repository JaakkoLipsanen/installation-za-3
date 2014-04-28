using Assets.Scripts.General;
using Flai;
using Flai.General;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(Health))]
    public class PlayerRegen : FlaiScript
    {
        public float StartTimeAfterDamage = 6;
        private Health _health;
        private Timer _timeUntilNextRegen = new Timer(0.075f);

        public bool IsRegenOn
        {
            get { return _health.TimeSinceDamageTaken > this.StartTimeAfterDamage; }
        }

        protected override void Awake()
        {
            _health = this.Get<Health>();
        }

        protected override void Update()
        {
            if (_health.TimeSinceDamageTaken > this.StartTimeAfterDamage)
            {
                _timeUntilNextRegen.Update();
                if (_timeUntilNextRegen.HasFinished && _health.CurrentHealth != _health.TotalHealth)
                {
                    _health.Heal(1);
                    _timeUntilNextRegen.Restart();
                }
            }
        }
    }
}
