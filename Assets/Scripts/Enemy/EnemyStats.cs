using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    public EnemyBaseStats baseStats = new EnemyBaseStats();

    public BuffStats buffStats = new BuffStats();

    public Stats stats = new Stats();

    public void CaculateStats()
    {
        stats.CaculateStats(baseStats, buffStats);
        AEnemy aEnemy = GetComponent<AEnemy>();
        aEnemy.enemyHPManager.SetMaxHp(stats.maxHp);
    }

    public int CalculateDamage()
    {

        int baseDamage = Random.Range(stats.attack.x, stats.attack.y + 1);

        // Tính chí mạng
        bool isCritical = Random.value < (stats.criticalChance / 100f);

        if (isCritical)
        {
            baseDamage = Mathf.FloorToInt(baseDamage * stats.criticalMultiplier);
        }

        return baseDamage;
    }

    public float GetAttack_Thredhold()
    {
        return baseStats.attack_threshold;
    }

}
