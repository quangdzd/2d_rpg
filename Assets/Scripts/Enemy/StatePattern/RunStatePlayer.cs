using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunStateEnemy : IStateEnemy
{
    public void Enter(AEnemy enemy)
    {
        enemy.EnterRunState();
    }

    public void Exit(AEnemy enemy)
    {
        enemy.ExitRunState();
    }

    public void FixedUpdate(AEnemy enemy)
    {
        enemy.FixedUpdateRunState();
    }

    public void Update(AEnemy enemy)
    {
        enemy.UpdateRunState();
    }
}
