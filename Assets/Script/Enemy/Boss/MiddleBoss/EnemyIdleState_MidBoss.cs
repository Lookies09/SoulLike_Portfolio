using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState_MidBoss : EnemyAttackAbleState
{
    // 대기 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {        
        // 네비게이션 에이전트 이동 정지
        navMeshAgent.isStopped = true;

        // 대기 애니메이션 재생
        animator.SetInteger("State", (int)state);
    }

    // 대기 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {
        objectHealth.Posture -= 10 * Time.deltaTime;

        // 플레이거 공격 가능 거리안에 들어왔다면
        if (controller.GetPlayerDistance() <= attackDistance)
        {
            // 공격 상태로 전환
            controller.TransactionToState(3);
            return;
        }

        // 플레이어가 추적 가능 거리안에 들어왔다면
        if (controller.GetPlayerDistance() <= detectDistance)
        {
            // 움직임 상태로 전환
            controller.TransactionToState(1);
            return;
        }

        
    }

    // 대기 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
        
    }
}
