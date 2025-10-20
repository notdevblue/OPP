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
        }

        public void AddForwardEvent(UnityAction onForward)
        {
            _onForward.AddListener(onForward);
        }
        public void AddLeftEvent(UnityAction onLeft)
        {
            _onLeft.AddListener(onLeft);
        }
        public void AddRightEvent(UnityAction onRight)
        {
            _onRight.AddListener(onRight);
        }
        public void AddBackwardEvent(UnityAction onBackward)
        {
            _onBackward.AddListener(onBackward);
        }

        public void RemoveForwardEvent(UnityAction onForward)
        {
            _onForward.RemoveListener(onForward);
        }
        public void RemoveLeftEvent(UnityAction onLeft)
        {
            _onLeft.RemoveListener(onLeft);
        }
        public void RemoveRightEvent(UnityAction onRight)
        {
            _onRight.RemoveListener(onRight);
        }
        public void RemoveBackwardEvent(UnityAction onBackward)
        {
            _onBackward.RemoveListener(onBackward);
        }

        public void RemoveAllForwardEvents()
        {
            _onForward.RemoveAllListeners();
        }
        public void RemoveAllLeftEvents()
        {
            _onLeft.RemoveAllListeners();
        }
        public void RemoveAllRightEvents()
        {
            _onRight.RemoveAllListeners();
        }
        public void RemoveAllBackwardEvents()
        {
            _onBackward.RemoveAllListeners();
        }

        public void Update()
        {

        }

        private void OnInput()
        {
            if (true) // TODO: 입력키로 바꿔야함
            {
                _onForward.Invoke();
            }
            if (true) // TODO: 입력키로 바꿔야함
            {
                _onLeft.Invoke();
            }
            if (true) // TODO: 입력키로 바꿔야함
            {
                _onRight.Invoke();
            }
            if (true) // TODO: 입력키로 바꿔야함
            {
                _onBackward.Invoke();
            }
        }


        private UnityEvent _onForward;
        private UnityEvent _onLeft;
        private UnityEvent _onRight;
        private UnityEvent _onBackward;
    }
}
