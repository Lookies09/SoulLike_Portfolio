using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DODGE_B_LastBoss : AttackPatternState
{
    public override void EnterState(int state)
    {
        // �ڷ� ���� �ִϸ��̼� ���
        animator.SetInteger("AttackPattern", state);

    }


    public override void ExitState()
    {

    }
}
