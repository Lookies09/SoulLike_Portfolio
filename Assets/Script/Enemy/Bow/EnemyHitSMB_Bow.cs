using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitSMB_Bow : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        animator.GetComponent<EnemyHitState_Bow>().IsHit = true;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        animator.GetComponent<EnemyHitState_Bow>().IsHit = false;
    }
}
