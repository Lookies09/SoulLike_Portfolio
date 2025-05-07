using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DASH_F_Pattern : AttackPatternState
{
    public override void EnterState(int state)
    {
        // 앞 대쉬 애니메이션 재생
        animator.SetInteger("AttackPattern", state);
                
    }


    public override void ExitState()
    {

    }
}
