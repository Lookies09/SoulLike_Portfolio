using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DASH_B_Pattern : AttackPatternState
{
    public override void EnterState(int state)
    {
        // �� �뽬 �ִϸ��̼� ���
        animator.SetInteger("AttackPattern", state);
    }


    public override void ExitState()
    {

    }
}
