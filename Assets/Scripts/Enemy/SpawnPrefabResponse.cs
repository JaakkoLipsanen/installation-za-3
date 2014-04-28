using Flai.Scripts;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class SpawnPrefabResponse : Response
    {
        public GameObject Prefab;
        public override void Execute()
        {
            this.Prefab.Instantiate(this.Position, this.RotationQuaternion);
            this.DestroyGameObject();
        }
	}
}