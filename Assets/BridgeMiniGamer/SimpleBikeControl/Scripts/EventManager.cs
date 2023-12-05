using System;
using UnityEngine;

namespace KikiNgao.SimpleBikeControl
{
    public class EventManager : MonoBehaviour
    {
        public Action onEnter, onExit;
        public Action onSpeedUp, onNormalSpeed;
        public Action onRiding;

        public void OnEnter() => onEnter?.Invoke();
        public void OnExit() => onExit?.Invoke();
        public void OnSpeedUp() => onSpeedUp?.Invoke();
        public void OnNormalSpeed() => onNormalSpeed?.Invoke();
        public void OnRiding() => onRiding?.Invoke();

    }
}
