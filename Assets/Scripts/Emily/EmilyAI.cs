using Assets.Scripts.Character;
using Flai;
using Flai.Scene;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.NPC
{
    [RequireComponent(typeof(CharacterController2D))]
    public class EmilyAI : FlaiScript
    {
        public enum EmilyState
        {
            Idle,
            Running,
            LookingAtJack,
        }

        private GameObject _player;
        private CharacterController2D _characterController;
        private EmilyState _state = EmilyState.Idle;

        public void StartRunning()
        {
            _state = EmilyState.Running;
        }

        public void LookAtJack()
        {
            _state = EmilyState.LookingAtJack;
        }

        public void Stop()
        {
            _state = EmilyState.Idle;
        }

        protected override void Awake()
        {
            _player = Scene.Find("Player");
            _characterController = this.Get<CharacterController2D>();
        }

        protected override void Update()
        {
            if (_state == EmilyState.Idle)
            {
                return;
            }
            else if (_state == EmilyState.Running)
            {
                if (_player.GetPosition2D().X - this.Position2D.X > 3.5f)
                {
                    _characterController.FacingDirection = HorizontalDirection.Right;
                    if (_player.GetPosition2D().X - this.Position2D.X > 4f)
                    {
                        _characterController.MoveRight();
                    }
                }
                else
                {
                    _characterController.FacingDirection = HorizontalDirection.Left;
                    _characterController.MoveLeft();
                }
            }
            else if (_state == EmilyState.LookingAtJack)
            {
                float delta = _player.GetPosition2D().X - this.Position2D.X;
                if (FlaiMath.Abs(delta) > 0.2f)
                {
                    if (delta > 0)
                    {
                        _characterController.FacingDirection = HorizontalDirection.Right;
                    }
                    else
                    {
                        _characterController.FacingDirection = HorizontalDirection.Left;
                    }
                }
            }
        }
    }
}