using UnityEngine;

namespace Assets.Scripts.InputServices
{
    public class DesktopInput : InputService
    {
        public override bool Tap => MouseTap();

        public override Vector3 TapPlace => TouchPlace();

        private Vector3 TouchPlace() => 
            Input.mousePosition;

        private bool MouseTap() => 
            Input.GetKeyDown(KeyCode.Mouse0);
    }
}
