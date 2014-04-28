using Assets.Scripts.Character;
using Flai;
using Flai.Scene;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public abstract class EnemyAI : FlaiScript
    {
        protected CharacterController2D _characterController;
        protected GameObject _player;

        protected override void Awake()
        {
            _characterController = this.Get<CharacterController2D>();
            _player = Scene.Find("Player");
        }
    }
}
