using UnityEngine;

namespace Assets.Misc
{
    public static class JayaHelper
    {
        public static bool IsPlayer(GameObject gameObject)
        {
            return gameObject != null && gameObject.name == "Player";
        }
    }
}
