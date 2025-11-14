using UnityEngine;
using UnityEngine.Events;

namespace OPP.Core.Actor
{
    public class ActorMovementCore : MonoBehaviour
    {
        public void Init()
        {
            // TODO: 요기 콘피그 레퍼 받아야 함

            _onForward.AddListener(() => { });
            _onLeft.AddListener(() => { });
            _onRight.AddListener(() => { });
            _onBackward.AddListener(() => { });
            _onUpdate.AddListener(() => { });
        }

        public void RegietserOnMovementEvent(
            UnityAction onForward, UnityAction onLeft,
            UnityAction onRight, UnityAction onBackward)
        {
            _onForward.AddListener(onForward);
            _onLeft.AddListener(onLeft);
            _onRight.AddListener(onRight);
            _onBackward.AddListener(onBackward);
        }

        public void UnregisterOnMovementEvent(
            UnityAction onForward, UnityAction onLeft,
            UnityAction onRight, UnityAction onBackward)
        {
            _onForward.RemoveListener(onForward);
            _onLeft.RemoveListener(onLeft);
            _onRight.RemoveListener(onRight);
            _onBackward.RemoveListener(onBackward);
        }

        public void RegietserOffMovementEvent(
            UnityAction offForward, UnityAction offLeft,
            UnityAction offRight, UnityAction offBackward)
        {
            _offForward.AddListener(offForward);
            _offLeft.AddListener(offLeft);
            _offRight.AddListener(offRight);
            _offBackward.AddListener(offBackward);
        }

        public void UnregisterOffMovementEvent(
            UnityAction offForward, UnityAction offLeft,
            UnityAction offRight, UnityAction offBackward)
        {
            _offForward.RemoveListener(offForward);
            _offLeft.RemoveListener(offLeft);
            _offRight.RemoveListener(offRight);
            _offBackward.RemoveListener(offBackward);
        }

        public void RegisterUpdateEvent(UnityAction onUpdate) => _onUpdate.AddListener(onUpdate);
        public void UnregisterUpdateEvent(UnityAction onUpdate) => _onUpdate.RemoveListener(onUpdate);

        // TODO: 콘피그 받아야함
        private void OnInput()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                _onForward.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                _onLeft.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                _onRight.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                _onBackward.Invoke();
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                _offForward.Invoke();
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                _offLeft.Invoke();
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                _offRight.Invoke();
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                _offBackward.Invoke();
            }
        }

        private void Update()
        {
            if (Input.anyKey)
            {
                OnInput();
            }

            _onUpdate.Invoke();
        }

        readonly private UnityEvent _onForward = new();
        readonly private UnityEvent _onLeft = new();
        readonly private UnityEvent _onRight = new();
        readonly private UnityEvent _onBackward = new();

        readonly private UnityEvent _offForward = new();
        readonly private UnityEvent _offLeft = new();
        readonly private UnityEvent _offRight = new();
        readonly private UnityEvent _offBackward = new();

        readonly private UnityEvent _onUpdate = new();
    }
}
