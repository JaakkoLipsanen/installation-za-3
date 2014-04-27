using System.Collections.Generic;
using Assets.Misc;
using Flai;
using Flai.General;
using Flai.Input;
using Flai.Scripts;
using UnityEngine;

namespace Assets.Scripts.General
{
	public class Computer : FlaiScript
	{
	    private GameObject _playerInside;
	    private bool _hasExecuted = false;
	    private GameObject _currentText;

	    public KeyCode InputKey = KeyCode.E;
	    public string InputStringFormat = "Press {0}";
	    public GameObject TextPrefab;
	    public Response Response;

	    public bool ExecuteOff = false;
	    public bool CanExecuteMultipleTimes = false;
	    public bool IsPlayerInArea
	    {
	        get { return _currentText != null; }
	    }

	    private bool CanExecute
	    {
	        get { return this.CanExecuteMultipleTimes || !_hasExecuted; }
	    }

	    protected override void OnTriggerEnter2D(Collider2D other)
	    {
	        if (!JayaHelper.IsPlayer(other.gameObject) || !this.CanExecute)
	        {
	            return;
	        }

	        if (_currentText != null)
	        {
                return;
	        }

	        _playerInside = other.gameObject;
	        _currentText = this.TextPrefab.Instantiate(this.Position2D + Vector2f.UnitY);
	        _currentText.Get<TextMesh>().text = string.Format(this.InputStringFormat, EnumHelper.GetName(this.InputKey));
	    }

	    protected override void OnTriggerExit2D(Collider2D other)
        {
            if (!JayaHelper.IsPlayer(other.gameObject))
            {
                return;
            }

            this.OnExit();
	    }

	    protected override void Update()
	    {
	        if (this.IsPlayerInArea && FlaiInput.IsNewKeyPress(this.InputKey) && this.CanExecute)
	        {
	            if (this.ExecuteOff)
	            {
	                this.Response.ExecuteOff();
	            }
	            else
                {
                    this.Response.Execute(); 
	            }

	            _hasExecuted = true;

	            if (!this.CanExecuteMultipleTimes)
	            {
	                _currentText.DestroyIfNotNull();
	                _currentText = null;
	            }
	        }

	        if (_playerInside != null && !PhysicsHelper.Intersects(_playerInside.collider2D, this.collider2D))
	        {
	            this.OnExit();
	        }
	    }

	    private void OnExit()
	    {
            _playerInside = null;
            _currentText.DestroyIfNotNull();
            _currentText = null;
	    }
	}
}