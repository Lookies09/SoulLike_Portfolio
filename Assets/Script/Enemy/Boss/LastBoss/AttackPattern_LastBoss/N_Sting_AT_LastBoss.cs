using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_Sting_AT_LastBoss : AttackPatternState
{
    public override void EnterState(int state)
    {
        // �Ϲ� ��� ���� �ִϸ��̼� ���
        animator.SetInteger("AttackPattern", state);

    }


    public override void ExitState()
    {

    }
}
