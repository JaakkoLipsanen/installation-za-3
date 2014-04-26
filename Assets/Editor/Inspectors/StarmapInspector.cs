using Assets.Scripts.Stars;
using Flai.Editor;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Inspectors
{
    [CustomEditor(typeof(Starmap))]
    public class StarmapInspector : InspectorBase<Starmap>
    {
        public override void OnInspectorGUI()
        {
            this.DrawDefaultInspector();
            if (GUILayout.Button("Generate"))
            {
                this.Target.Generate();
            }

            if (GUILayout.Button("Reset"))
            {
                this.Target.Reset();
            }
        }
    }
}
