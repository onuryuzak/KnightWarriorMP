using UnityEngine;

namespace Core.Interfaces
{
    public interface IAttackService
    {
        void ExecuteAttack(GameObject attacker, float attackRange, int attackDamage);
        bool IsTargetInRange(GameObject attacker, float attackRange);
        
    }
}