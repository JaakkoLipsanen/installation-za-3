using Flai;
using Flai.Scripts;
using UnityEngine;

namespace Assets.Scripts.General
{
	public class ExecuteResponseOnTrigger : FlaiScript
	{
	    private bool _hasTriggered = false;
	    public Response Response;
	    public bool CanExecuteMultipleTimes = false;
	    protected override void OnTriggerEnter2D(Collider2D other)
	    {
	        if (other.name != "Emily")
	        {
	            return;    
	        }

	        if (!_hasTriggered || this.CanExecuteMultipleTimes)
	        {
	            if (this.Response != null)
	            {
	                this.Response.Execute();
	            }

	            _hasTriggered = true;
	        }
	    }
	}
}