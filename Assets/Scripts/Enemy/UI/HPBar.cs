using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Enemy.UI
{
    public class HPBar : MonoBehaviour
    {
        [SerializeField] private Image _imageCurrent;

        public void SetValue(float current, float max) =>
            _imageCurrent.fillAmount = current / max;
    }
}
