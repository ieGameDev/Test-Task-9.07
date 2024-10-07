using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class BulletComponent
    {
        public GameObject BulletObject { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public Bullet Bullet { get; private set; }
        public TrailRenderer TrailRenderer { get; private set; }

        public BulletComponent(GameObject bulletObject, Bullet bullet, Rigidbody rigidBody, TrailRenderer trailRenderer)
        {
            BulletObject = bulletObject;
            Bullet = bullet;
            Rigidbody = rigidBody;
            TrailRenderer = trailRenderer;
        }
    }
}
