using Assets.Scripts.General;
using Flai;
using Flai.Editor;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Inspectors
{
    [CustomEditor(typeof(Elevator))]
    public class ElevatorInspector : InspectorBase<Elevator>
    {
        public override void OnInspectorGUI()
        {
            this.DrawDefaultInspector();
            if (GUILayout.Button("Flip"))
            {
                this.Target.Position2D += this.Target.Direction.ToUnitVector()*this.Target.Distance;
                this.Target.Direction = this.Target.Direction.Opposite();
            }
        }
    }
}
