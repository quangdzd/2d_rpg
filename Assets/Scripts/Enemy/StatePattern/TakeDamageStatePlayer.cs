using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageStateEnemy : IStateEnemy
{
    public void Enter(AEnemy enemy)
    {
        enemy.EnterTakedamageState();
    }

    public void Exit(AEnemy enemy)
    {
        enemy.ExitTakedamageState();
    }

    public void FixedUpdate(AEnemy enemy)
    {
        enemy.FixedUpdateTakedamageState();
    }

    public void Update(AEnemy enemy)
    {
        enemy.UpdateTakedamageState();
    }
}
