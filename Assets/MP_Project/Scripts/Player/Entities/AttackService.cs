using UnityEngine;
using Core.Interfaces;
using System.Linq;
using System.Collections.Generic;
using Application.Managers;
using Domain.Entities;
using Unity.Netcode;

namespace Services
{
    public class AttackService : IAttackService
    {
        public void ExecuteAttack(GameObject attacker, float attackRange, int attackDamage)
        {
            if (attacker.GetComponent<Rigidbody2D>().velocity.sqrMagnitude <= 0)
            {
                return;
            }

            var players = PlayerHealthRegistry.GetPlayersHealthComponent();
            var targets = players.Where(player =>
                player.gameObject != attacker &&
                Vector3.Distance(attacker.transform.position, player.transform.position) <= attackRange);

            var target = targets.FirstOrDefault();
            if (target != null)
            {
                var ownerId = attacker.GetComponent<NetworkObject>().OwnerClientId;
                target.GetComponent<HealthComponent>().TakeDamageServerRpc(attackDamage, ownerId);
            }
        }

        public bool IsTargetInRange(GameObject attacker, float attackRange)
        {
            if (attacker.GetComponent<Rigidbody2D>().velocity.sqrMagnitude <= 0)
            {
                return false;
            }

            var players = PlayerHealthRegistry.GetPlayersHealthComponent();
            return players.Any(player => player.gameObject != attacker &&
                                         Vector3.Distance(attacker.transform.position, player.transform.position) <=
                                         attackRange);
        }
    }
}