using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunStatePlayer : IStatePlayer
{
    public void Enter(PlayerController player)
    {
        player.EnterRunState();
    }

    public void Exit(PlayerController player)
    {
        player.ExitRunState();
    }

    public void FixedUpdate(PlayerController player)
    {
        player.FixedUpdateRunState();
    }

    public void Update(PlayerController player)
    {
        player.UpdateRunState();
    }
}
