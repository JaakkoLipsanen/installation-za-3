using Assets.Misc;
using Flai;
using Flai.Diagnostics;
using Flai.Scripts;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    [ExecuteInEditMode]
	public class EnemySpawnTrigger : FlaiScript
	{
	    private bool _hasTriggered = false;
	    protected override void OnTriggerEnter2D(Collider2D other)
	    {
	        if (_hasTriggered)
	        {
	            return;
	        }

	        if (JayaHelper.IsPlayer(other.gameObject))
	        {
	            foreach (Response response in this.GetComponentsInChildren<Response>())
	            {
	                if (response != null)
	                {
	                    response.Execute();
	                }
	            }

	            _hasTriggered = true;
	        }
	    }

	    protected override void Update()
	    {
	        if (Application.isEditor)
	        {
	            FlaiDebug.DrawRectangleOutlines(this.BoxCollider2D.GetBoundsHack(), ColorF.DodgerBlue);
	        }
	    }
	}
}