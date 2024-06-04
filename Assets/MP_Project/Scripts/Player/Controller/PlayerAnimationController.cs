using Core.Interfaces;
using Domain.Entities;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public Animator PlayerAnimator => _animator;

    public void SetWalk(float speed)
    {
        _animator.SetFloat("Speed", speed);
    }

    public void SetIsAttacking(bool isAttacking)
    {
        _animator.SetBool("IsAttacking", isAttacking);
    }

    // This method should be called as an Animation Event at the end of the attack animation
    public void OnAttackAnimationFinished()
    {
        var attackComponent = GetComponent<AttackComponent>();
        if (attackComponent != null)
        {
            attackComponent.PerformAttack();
        }
    }
}