using UnityEngine;

namespace Assets.Scripts.StaticData
{
    [CreateAssetMenu(fileName = "playerData", menuName = "StaticData/Player")]
    public class PlayerStaticData : ScriptableObject
    {
        [Range(1, 50)]
        public float Damage;

        [Range(1, 30)]
        public float BulletSpeed;
    }
}
