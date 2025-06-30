using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public BaseStats baseStats = new BaseStats();
    public BuffStats buffStats = new BuffStats();

    public Stats stats = new Stats();

    public void CaculateStats()
    {
        stats.CaculateStats(baseStats, buffStats);


        PlayerController playerController = GetComponent<PlayerController>();
        playerController._playerHpManger.SetMaxHp(stats.maxHp);
        


        
    }
    
    public int CalculateDamageToEnemy()
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

}
