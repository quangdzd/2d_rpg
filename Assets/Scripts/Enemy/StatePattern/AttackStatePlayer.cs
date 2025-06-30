using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateEnemy : IStateEnemy
{
    public void Enter(AEnemy enemy)
    {
        enemy.EnterAttackState();
    }

    public void Exit(AEnemy enemy)
    {
        enemy.ExitAttackState();
    }

    public void FixedUpdate(AEnemy enemy)
    {
        enemy.FixedUpdateAttackState();
    }

    public void Update(AEnemy enemy)
    {
        enemy.UpdateAttackState();
    }
}
