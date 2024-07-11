using UnityEngine;

namespace Assets.Scripts.InputServices
{
    public abstract class InputService : IInputService
    {
        public abstract bool Tap { get; }
        public abstract Vector3 TapPlace { get; }
    }
}
