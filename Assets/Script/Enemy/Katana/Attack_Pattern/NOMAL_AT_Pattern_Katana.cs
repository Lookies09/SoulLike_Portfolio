using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOMAL_AT_Pattern_Katana : AttackPatternState
{
    public override void EnterState(int state)
    {
        // �⺻ ���� �ִϸ��̼� ���
        animator.SetInteger("AttackPattern", state);

    }


    public override void ExitState()
    {

    }
}
