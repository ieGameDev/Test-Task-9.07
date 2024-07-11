using UnityEngine;

namespace Assets.Scripts.Enemy.UI
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HPBar _hpBar;

        private IHealth _health;

        private void Start()
        {
            _health = GetComponent<IHealth>();
            _health.HealthChanged += UpdateHpBar;
        }

        private void OnDestroy()
        {
            if (_health != null)
                _health.HealthChanged -= UpdateHpBar;
        }

        private void UpdateHpBar() =>
            _hpBar.SetValue(_health.CurrentHealth, _health.MaxHealth);
    }
}
