using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStatePlayer : IStatePlayer
{
    public void Enter(PlayerController player)
    {
        player.EnterIdleState();
    }

    public void Exit(PlayerController player)
    {
        player.ExitIdleState();
    }

    public void FixedUpdate(PlayerController player)
    {
        player.FixedUpdateIdleState();
    }

    public void Update(PlayerController player)
    {
        player.UpdateIdleState();
    }
}
