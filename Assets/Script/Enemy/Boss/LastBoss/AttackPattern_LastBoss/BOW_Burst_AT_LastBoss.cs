using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOW_Burst_AT_LastBoss : AttackPatternState
{
    public override void EnterState(int state)
    {
        // Ȱ ����Ʈ ���� �ִϸ��̼� ���
        animator.SetInteger("AttackPattern", state);
                

    }


    public override void ExitState()
    {

    }
}
