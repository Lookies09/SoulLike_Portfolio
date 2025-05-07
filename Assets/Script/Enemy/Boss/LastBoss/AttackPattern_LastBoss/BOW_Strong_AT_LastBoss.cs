using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOW_Strong_AT_LastBoss : AttackPatternState
{
    public override void EnterState(int state)
    {
        // 활 강공격 애니메이션 재생
        animator.SetInteger("AttackPattern", state);


    }


    public override void ExitState()
    {

    }
}
