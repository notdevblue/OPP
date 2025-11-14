using OPP.Core.Actor;
using UnityEngine;

namespace OPP.Core.Character
{
    public class CharacterMovementCore : MonoBehaviour
    {
        [SerializeField] private ActorMovementCore _actorMovementCore;
        [SerializeField] private float _speed = 5.0f;
        [SerializeField] private Rigidbody2D _rigid2D;

        private Vector2 _movementVector2 = new();

        public void Awake()
        {
            _actorMovementCore.Init();
            _actorMovementCore.RegietserOnMovementEvent(
                onForward: OnMoveUp, onLeft: OnMoveLeft,
                onRight: OnMoveRight, onBackward: OnMoveDown);
            _actorMovementCore.RegietserOffMovementEvent(
                offForward: OffMoveUp, offLeft: OffMoveLeft,
                offRight: OffMoveRight, offBackward: OffMoveDown);

            _actorMovementCore.RegisterUpdateEvent(OnUpdate);
        }

        private void OnMoveUp()
        {
            _movementVector2.y = _speed;
        }
        private void OnMoveDown()
        {
            _movementVector2.y = -_speed;
        }
        private void OnMoveLeft()
        {
            _movementVector2.x = -_speed;
        }
        private void OnMoveRight()
        {
            _movementVector2.x = _speed;
        }

        private void OffMoveUp()
        {
            _movementVector2.y = 0;
        }
        private void OffMoveDown()
        {
            _movementVector2.y = 0;
        }
        private void OffMoveLeft()
        {
            _movementVector2.x = 0;
        }
        private void OffMoveRight()
        {
            _movementVector2.x =0;
        }

        private void OnUpdate()
        {
            _rigid2D.linearVelocity = _movementVector2;
        }

    }
}