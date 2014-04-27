using Flai;
using Flai.Tween;
using UnityEngine;

namespace Assets.Scripts.General
{
    public class Shaker : FlaiScript
    {
        protected override void Awake()
        {
            Tween.Scale(this.GameObject, Vector3.one * 0.75f, 0.25f).SetEase(TweenType.Linear).SetLoopPingPong();
            Tween.RotateZ(this.GameObject, this.Rotation2D + 20, 0.15f).SetEase(TweenType.Linear).SetLoopPingPong();
        }

        protected override void Update()
        {
        }
    }
}