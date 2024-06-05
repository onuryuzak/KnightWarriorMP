using Application.Managers;
using Core.Interfaces;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Events;

namespace Domain.Entities
{
    public class HealthComponent : NetworkBehaviour, IHealth
    {
        [SerializeField] private PlayerAnimatorController _playerAnimatorController;
        [SerializeField] private int _maxHealth = 100;
        private NetworkVariable<int> _currentHealth = new NetworkVariable<int>();

        public int CurrentHealth => _currentHealth.Value;
        public UnityEvent OnHealthChanged;

        public override void OnNetworkSpawn()
        {
            if (IsServer)
            {
                _currentHealth.Value = _maxHealth;
            }

            _currentHealth.OnValueChanged += OnHealthChangedCallback;

            PlayerHealthRegistry.AddPlayerHealthComponent(this);
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();
            _currentHealth.OnValueChanged -= OnHealthChangedCallback;

            PlayerHealthRegistry.Players.Remove(this);
        }

        [ServerRpc(RequireOwnership = false)]
        public void TakeDamageServerRpc(int damage, ulong attackerId)
        {
            if (!IsServer) return;
            _currentHealth.Value -= damage;
            if (_currentHealth.Value <= 0)
            {
                Die(attackerId);
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

        private void Die(ulong attackerId)
        {
            var playerController = GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.KillPlayerServerRpc(attackerId, NetworkObject.OwnerClientId);
            }

            GetComponent<NetworkObject>().Despawn(true);
        }

        private void OnHealthChangedCallback(int previousValue, int newValue)
        {
            OnHealthChanged?.Invoke();
        }

        public int GetHealth()
        {
            return _currentHealth.Value;
        }

        public float GetHealthPercentage()
        {
            return (float)_currentHealth.Value / _maxHealth;
        }
    }
}