using Flai;
using UnityEngine;

namespace Assets.Scripts.General
{
    public class Health : FlaiScript
    {
        public GameObject InstantiateOnDeath;

        private float _timeSinceDamage = int.MaxValue;
        private bool _hasDied = false;
        private int _currentDamageTaken = 0;
        public int TotalHealth = 40;
        public int RemainingHealthREMOVETHIS;

        public bool DestroyOnDeath = true;

        public int CurrentHealth
        {
            get { return this.TotalHealth - _currentDamageTaken; }
        }

        public bool IsAlive
        {
            get { return this.CurrentHealth > 0; }
        }

        public float TimeSinceDamageTaken
        {
            get { return _timeSinceDamage; }
        }

        public bool IsFullHealth
        {
            get { return this.CurrentHealth == this.TotalHealth; }
        }

        public void TakeDamage(int amount)
        {
            Ensure.True(amount >= 0);
            _currentDamageTaken = FlaiMath.Min(_currentDamageTaken + amount, this.TotalHealth);
            _timeSinceDamage = 0;
        }

        public void Heal(int amount)
        {
            Ensure.True(amount > 0);
            _currentDamageTaken = FlaiMath.Max(0, _currentDamageTaken - amount);
        }

        protected override void LateUpdate()
        {
            _timeSinceDamage += Time.deltaTime;
            this.RemainingHealthREMOVETHIS = this.CurrentHealth;
            if (!this.IsAlive && !_hasDied)
            {
                this.InstantiateOnDeath.InstantiateIfNotNull(this.Position2D, this.Rotation2D);
                if (this.DestroyOnDeath)
                {
                    this.DestroyGameObject();
                }

                _hasDied = true;
            }
        }
    }
}