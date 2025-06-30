using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateEnemy : IStateEnemy
{
    public void Enter(AEnemy enemy)
    {
        enemy.EnterIdleState();
    }

    public void Exit(AEnemy enemy)
    {
        enemy.ExitIdleState();
    }

    public void FixedUpdate(AEnemy enemy)
    {
        enemy.FixedUpdateIdleState();
    }

    public void Update(AEnemy enemy)
    {
        enemy.UpdateIdleState();
    }
}
