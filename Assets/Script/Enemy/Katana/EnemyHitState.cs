using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState : EnemyAttackAbleState
{
    
    // 피격 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        // 이동 중지
        navMeshAgent.isStopped = true;

        // 피격 애니메이션 재생
        IsHit = true;
        animator.SetInteger("State", (int)state);
    }

    // 피격 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {

        // 한대 맞으면 리턴
        if (IsHit) return;
       
        // 맞고 있는 상태가 아니고 공격가능한 상태면
        if (controller.GetPlayerDistance() <= attackDistance)
        {
            // 방어 상태로 전환
            controller.TransactionToState(7);
            return;
        }
        //  공격가능 거리에서 벗어났지만 추적가능한 거리면
        else if (controller.GetPlayerDistance() <= detectDistance)
        {
            // 추적상태로 전환
            controller.TransactionToState(2);
            return;
        }
        else // 다 아니라면 
        {
            //포기(배회)
            controller.TransactionToState(4);
            return;
        }
    }

    // 피격 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
        IsHit = false;
    }

    
}
