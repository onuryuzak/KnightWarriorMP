using UnityEngine;
using Core.Interfaces;
using System.Linq;
using System.Collections.Generic;
using Application.Managers;
using Domain.Entities;

namespace Services
{
    public class AttackService : IAttackService
    { public void ExecuteAttack(GameObject attacker, float attackRange, int attackDamage)
        {
            var players = PlayerHealthRegistry.GetPlayersHealthComponent();
            var targets = players.Where(player => player.gameObject != attacker &&
                                                  Vector3.Distance(attacker.transform.position,
                                                      player.transform.position) <= attackRange);

            var target = targets.FirstOrDefault();
            if (target != null)
            {
                target.GetComponent<HealthComponent>().TakeDamage(attackDamage);
            }
        }

        public bool IsTargetInRange(GameObject attacker, float attackRange)
        {
            var players = PlayerHealthRegistry.GetPlayersHealthComponent();
            return players.Any(player => player.gameObject != attacker &&
                                         Vector3.Distance(attacker.transform.position, player.transform.position) <=
                                         attackRange);
        }
    }
}