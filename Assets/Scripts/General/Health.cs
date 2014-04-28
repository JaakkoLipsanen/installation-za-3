using Flai;

namespace Assets.Scripts.General
{
	public class Health : FlaiScript
	{
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

	    public void TakeDamage(int amount)
	    {
	        _currentDamageTaken += amount;
	    }

	    protected override void LateUpdate()
	    {
	        this.RemainingHealthREMOVETHIS = this.CurrentHealth;
	        if (this.DestroyOnDeath && !this.IsAlive)
	        {
	            this.DestroyGameObject();
	        }
	    }
	}
}