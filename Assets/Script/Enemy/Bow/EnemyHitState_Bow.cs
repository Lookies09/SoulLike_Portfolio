using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState_Bow : EnemyAttackAbleState
{
    // 피격 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        Debug.Log("히트 상태 On");
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
            Debug.Log("공격상태 가능");
            // 대기 상태로 전환
            controller.TransactionToState(0);
            return;
        }
        // 맞고 있는 상태가 아니고 공격거리에서 벗어났다면
        else
        {
            Debug.Log("배회상태 가능");
            // 포기(배회) 상태로 전환
            controller.TransactionToState(3);
            return;
        }
    }

    // 피격 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
        IsHit = false;
    }
}
