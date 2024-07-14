using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSystem : MonoBehaviour
{
    private Vector3 newEnemyPos = new Vector3(8, 4f);
    public static BossSystem instance { get; private set; }
    private void Awake()
    {
        instance = this;
    }
    public IEnumerator BossTurn()
    {
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                FirstBossSkill();
                break;
            case 1:
                SecondBossSkill();
                break;
            case 2:
                ThirdBossSkill();
                break;
        }
        yield return null;
        Debug.Log("Boss");
        BattleSystem.instance.state = BattleSystem.State.playerTurn;
    }

    public void FirstBossSkill()
    {
        BattleSystem.instance.curEnemy++;
        BattleSystem.instance.minusNum++;
        BattleSystem.instance.SpawnEnemy(newEnemyPos);
    }

    public void SecondBossSkill()
    {
        BattleSystem.instance.curEnemy++;
        BattleSystem.instance.minusNum++;
        BattleSystem.instance.SpawnEnemy(newEnemyPos);
    }

    public void ThirdBossSkill()
    {
        BattleSystem.instance.curEnemy++;
        BattleSystem.instance.minusNum++;
        BattleSystem.instance.SpawnEnemy(newEnemyPos);
    }
}
