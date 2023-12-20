using System;

public interface IAttackCapable
{
    public IDamageable AttackTarget { get; }
    public void SetAttackTarget(IDamageable target);
    public void Attack();
    public Action OnAttack { get; set; }
    public void StartAttack();
    public void StopAttack();
}