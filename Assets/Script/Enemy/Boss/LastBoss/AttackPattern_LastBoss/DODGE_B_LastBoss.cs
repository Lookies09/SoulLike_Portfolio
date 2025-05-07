using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DODGE_B_LastBoss : AttackPatternState
{
    public override void EnterState(int state)
    {
        // 뒤로 닷지 애니메이션 재생
        animator.SetInteger("AttackPattern", state);

    }


    public override void ExitState()
    {

    }
}
