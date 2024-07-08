using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcAttack : Enemy
{
    public Animator anime;
    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (BattleSystem.instance.enemySlot[MonsterAttackNum].enemyType == EnemyType.Archer == true && BattleSystem.instance.enemySlot[MonsterAttackNum].Enemykind == EnemyKind.Orc && isAttacking == true) 
        //    AcherAttack();
        //if (BattleSystem.instance.enemySlot[MonsterAttackNum].enemyType == EnemyType.Wizard == true && BattleSystem.instance.enemySlot[MonsterAttackNum].Enemykind == EnemyKind.Orc && isAttacking == true) 
        //    WizardAttack();
    }

    public void AcherAttack()
    {
        isAttacking = false;
        if (MonsterAttackNum == this.enemyNum)
        {
            GameManager.instance.EnemyAttack(MonsterAttackNum);
        }
    }

    public void WizardAttack()
    {
        isAttacking = false;
        if (MonsterAttackNum == this.enemyNum)
        {
            BattleSystem.instance.enemySlot[MonsterAttackNum].Charge++;
            if (BattleSystem.instance.enemySlot[MonsterAttackNum].Charge >= 3)
            {
                GameManager.instance.EnemyAttack(MonsterAttackNum);
                BattleSystem.instance.enemySlot[MonsterAttackNum].Charge = 0;
            }
        }
    }
}
