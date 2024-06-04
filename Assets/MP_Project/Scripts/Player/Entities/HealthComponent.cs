using Application.Managers;
using Core.Interfaces;
using UnityEngine;
using Unity.Netcode;

namespace Domain.Entities
{
    public class HealthComponent : NetworkBehaviour, IHealth
    {
        [SerializeField] private int _maxHealth = 100;
        private NetworkVariable<int> _currentHealth = new NetworkVariable<int>();

        public int CurrentHealth => _currentHealth.Value;

        public override void OnNetworkSpawn()
        {
            if (IsServer)
            {
                _currentHealth.Value = _maxHealth;
            }

            PlayerHealthRegistry.AddPlayerHealthComponent(this);
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();
            PlayerHealthRegistry.Players.Remove(this);
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
            GetComponent<NetworkObject>().Despawn(true);
        }
    }
}