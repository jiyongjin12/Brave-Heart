using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack : Enemy
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
        
    }

    public void AcherAttack()
    {
        if(isArcher == true && BattleSystem.instance.enemySlot[MonsterAttackNum].Enemykind == EnemyKind.Slime)
        {
            isArcher = false;
            GameManager.instance.EnemyAttack(MonsterAttackNum);
        }
    }

    public void WizardAttack()
    {
        if (isWizard == true && BattleSystem.instance.enemySlot[MonsterAttackNum].Enemykind == EnemyKind.Slime)
        {
            isWizard = false;
            BattleSystem.instance.enemySlot[MonsterAttackNum].Charge++;
            if (BattleSystem.instance.enemySlot[MonsterAttackNum].Charge >= 3)
            {
                GameManager.instance.EnemyAttack(MonsterAttackNum);
                BattleSystem.instance.enemySlot[MonsterAttackNum].Charge = 0;
            }
        }
    }
}
