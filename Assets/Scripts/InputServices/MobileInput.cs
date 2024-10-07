using UnityEngine;

namespace Assets.Scripts.InputServices
{
    public class MobileInput : InputService
    {
        public override bool Tap => MobileTouch();

        public override Vector3 TapPlace => TouchPlace();

        private bool MobileTouch() =>
            Input.touchCount > 0;

        private Vector3 TouchPlace()
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                return touch.position;

            return Vector3.zero;
        }
    }
}