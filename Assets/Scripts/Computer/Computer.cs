using Assets.Misc;
using Flai;
using Flai.General;
using Flai.Input;
using Flai.Scripts;
using UnityEngine;

namespace Assets.Scripts.Computer
{
	public class Computer : FlaiScript
	{
	    private bool _hasExecuted = false;
	    private GameObject _currentText;

	    public KeyCode InputKey = KeyCode.E;
	    public string InputStringFormat = "Press {0}";
	    public GameObject TextPrefab;
	    public Response Response;

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

	        _currentText = this.TextPrefab.Instantiate(this.Position2D + Vector2f.UnitY);
	        _currentText.Get<TextMesh>().text = string.Format(this.InputStringFormat, EnumHelper.GetName(this.InputKey));
	    }

	    protected override void OnTriggerExit2D(Collider2D other)
        {
            if (!JayaHelper.IsPlayer(other.gameObject))
            {
                return;
            }

            _currentText.DestroyIfNotNull();
	        _currentText = null;
	    }

	    protected override void Update()
	    {
	        if (this.IsPlayerInArea && FlaiInput.IsNewKeyPress(this.InputKey) && this.CanExecute)
	        {
	            this.Response.Execute();
	            _hasExecuted = true;

	            if (!this.CanExecuteMultipleTimes)
	            {
	                _currentText.DestroyIfNotNull();
	                _currentText = null;
	            }
	        }
	    }
	}
}