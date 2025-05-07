using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_AT_Pattern : AttackPatternState
{
    public override void EnterState(int state)
    {
        // 강 공격 애니메이션 재생
        animator.SetInteger("AttackPattern", state);
    }


    public override void ExitState()
    {

    }
}
