using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_Sting_AT_LastBoss : AttackPatternState
{
    public override void EnterState(int state)
    {
        // 일반 찌르기 공격 애니메이션 재생
        animator.SetInteger("AttackPattern", state);

    }


    public override void ExitState()
    {

    }
}
