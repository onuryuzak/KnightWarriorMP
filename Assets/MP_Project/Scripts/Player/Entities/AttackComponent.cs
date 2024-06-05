using Application.Managers;
using Core.Interfaces;
using UnityEngine;
using Unity.Netcode;
using Services;

namespace Domain.Entities
{
    public class AttackComponent : NetworkBehaviour
    {
        [SerializeField] private float attackRange = 2.0f;
        [SerializeField] private int attackDamage = 10;
        [SerializeField] private float attackCooldown = 1.0f;
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private PlayerAnimatorController _playerAnimatorController;
        private float lastAttackTime;
        private IAttackService _attackService;
        private bool _isInRange;

        public void Start()
        {
            _attackService = new AttackService();
        }

        private void Update()
        {
            if (_attackService == null) return;
            _isInRange = _attackService.IsTargetInRange(gameObject, attackRange);
            _playerAnimatorController.SetIsAttacking(_isInRange);

            if (!_isInRange) return;
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                lastAttackTime = Time.time;
                PerformAttack();
            }
        }

        public void PerformAttack()
        {
            if (_isInRange)
            {
                _attackService.ExecuteAttack(gameObject, attackRange, attackDamage);
            }
        }
    }
}