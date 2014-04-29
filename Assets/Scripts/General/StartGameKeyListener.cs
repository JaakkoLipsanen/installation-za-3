using Flai;
using Flai.Input;
using Flai.Scene;
using UnityEngine;

namespace Assets.Scripts
{
	public class StartGameKeyListener : FlaiScript
	{
	    private bool _isFading = false;
	    protected override void Update()
	    {
	        if ((FlaiInput.IsNewAnyKeyPress || FlaiInput.IsNewKeyPress(KeyCode.Space)) && !_isFading)
	        {
	            SceneFader.Fade(SceneDescription.FromName("GameScene"), Fade.Create(), Fade.Create());
	            _isFading = true;
	        }
	    }
	}
}