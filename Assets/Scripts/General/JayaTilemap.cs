using Flai;
using Flai.Diagnostics;
using UnityEngine;

namespace Assets.Scripts.General
{
    [ExecuteInEditMode]
	public class JayaTilemap : FlaiScript
	{
        protected override void Update()
        {
            switch (this.GameObject.name)
            {
                case "Background":
                    this.renderer.sortingOrder = -1500;
                    this.Transform.SetPositionZ(0f);
                    break;

                case "Foreground":
                    this.renderer.sortingOrder = 1;
                    this.Transform.SetPositionZ(-0.05f);
                    break;
            }
        }
	}
}