using Core.Interfaces;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Serialization;

namespace Domain.Entities
{
    public class HealthComponent : NetworkBehaviour, IHealth
    {
         [SerializeField] private int _maxHealth = 100;
        [SerializeField] private Animator _playerAnimator;
        private NetworkVariable<int> _currentHealth = new NetworkVariable<int>();

        public int CurrentHealth => _currentHealth.Value;

        public override void OnNetworkSpawn()
        {
            if (IsServer)
            {
                _currentHealth.Value = _maxHealth;
            }
        }

        public void TakeDamage(int damage)
        {
            if (!IsServer) return;
            _currentHealth.Value -= damage;
            if (_currentHealth.Value <= 0)
            {
                Die();
            }
        }

        public void Heal(int amount)
        {
            if (!IsServer) return;
            _currentHealth.Value += amount;
            if (_currentHealth.Value > _maxHealth)
            {
                _currentHealth.Value = _maxHealth;
            }
        }

        private void Die()
        {
            gameObject.SetActive(false);
        }
    }
}