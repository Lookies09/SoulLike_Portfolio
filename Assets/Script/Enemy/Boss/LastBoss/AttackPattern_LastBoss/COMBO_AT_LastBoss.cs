using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COMBO_AT_LastBoss : AttackPatternState
{
    public override void EnterState(int state)
    {
        // 콤보 공격 애니메이션 재생
        animator.SetInteger("AttackPattern", state);

    }


    public override void ExitState()
    {

    }
}
