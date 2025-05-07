using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DASH_B_Pattern : AttackPatternState
{
    public override void EnterState(int state)
    {
        // 백 대쉬 애니메이션 재생
        animator.SetInteger("AttackPattern", state);
    }


    public override void ExitState()
    {

    }
}
