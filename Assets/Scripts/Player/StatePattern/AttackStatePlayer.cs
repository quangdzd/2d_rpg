using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStatePlayer : IStatePlayer
{
    public void Enter(PlayerController player)
    {
        player.EnterAttackState();
    }

    public void Exit(PlayerController player)
    {
        player.ExitAttackState();
    }

    public void FixedUpdate(PlayerController player)
    {
        player.FixedUpdateAttackState();
    }

    public void Update(PlayerController player)
    {
        player.UpdateAttackState();
    }
}
