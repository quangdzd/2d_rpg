using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPManager : AHpManager
{

    public override void TakeDMG(float dmg)
    {
        GetHealthBar();
        AEnemy aEnemy = GetComponent<AEnemy>();
        if (currentHp <= 0)
        {
            aEnemy.ChangeToDeathState();
        }

        currentHp = Mathf.Max(0, currentHp - dmg);
        aEnemy.GetDamage(dmg);
        
        if (healthBarCoroutine != null)
    {
        StopCoroutine(healthBarCoroutine);
    }

    // Khởi chạy mới
        healthBarCoroutine = StartCoroutine(HealthBarChange());

    }

}
