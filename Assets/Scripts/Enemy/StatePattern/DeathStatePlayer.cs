using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathStateEnemy : IStateEnemy
{
    public void Enter(AEnemy enemy)
    {
        enemy.EnterDeathState();
    }

    public void Exit(AEnemy enemy)
    {
        enemy.ExitDeathState();
    }

    public void FixedUpdate(AEnemy enemy)
    {
        enemy.FixedUpdateDeathState();
    }

    public void Update(AEnemy enemy)
    {
        enemy.UpdateDeathState();
    }
}
