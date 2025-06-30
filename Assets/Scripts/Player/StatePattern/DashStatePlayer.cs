using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashStatePlayer : IStatePlayer
{
    public void Enter(PlayerController player)
    {
        player.EnterDashState();
    }

    public void Exit(PlayerController player)
    {
        player.ExitDashState();
    }

    public void FixedUpdate(PlayerController player)
    {
        player.FixedUpdateDashState();
    }

    public void Update(PlayerController player)
    {
        player.UpdateDashState();
    }
}
