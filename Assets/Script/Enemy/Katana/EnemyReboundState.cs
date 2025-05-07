using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 리바운드 상태 (패링 당한 상태)
public class EnemyReboundState : EnemyAttackAbleState
{
    // 시간
    private float time = 0;

    // 리바운드 지속 시간
    [SerializeField] private float reboundTime;

    public override void EnterState(int state)
    {        
        animator.SetInteger("State", state);
        Debug.Log("패링");
    }


    public override void UpdateState()
    {
        time += Time.deltaTime;
        
        // 대상이 추적 가능 거리 안에 있으면서 공격 시간이 지났을 때
        if (controller.GetPlayerDistance() < detectDistance && time > reboundTime)
        {
            // 공격 가능 거리 안에 있으면
            if (controller.GetPlayerDistance() < attackDistance)
            {
                // 방어 모션 재생
                controller.TransactionToState(7);
            }
            else // 공격 가능 거리에서 벗어 났다면
            {
                // 추적 모션 재생
                controller.TransactionToState(2);
            }
        }
        else if (controller.GetPlayerDistance() >= detectDistance) // 추적 가능 거리에서 벗어났다면
        {
            // 배회
            controller.TransactionToState(4);
            return;
        }
    }


    public override void ExitState()
    {
        
        time = 0;
    }
}
