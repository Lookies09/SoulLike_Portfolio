using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOW_Strong_AT_LastBoss : AttackPatternState
{
    public override void EnterState(int state)
    {
        // Ȱ ������ �ִϸ��̼� ���
        animator.SetInteger("AttackPattern", state);


    }


    public override void ExitState()
    {

    }
}
