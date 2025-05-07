using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState_MidBoss : EnemyAttackAbleState
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

        // 추적 가능 거리 안에 있고
        if (controller.GetPlayerDistance() < detectDistance)
        {
            // 공격 가능 거리 안에 있다면
            if (controller.GetPlayerDistance() < attackDistance)
            {
                // 방어 상태로 전환
                controller.TransactionToState(7);
                return;
            }
            else // 공격 가능 거리 밖에 있다면
            {
                // 움직임 상태로 전환
                controller.TransactionToState(1);
                return;
            }

        }
        else // 추적 가능 거리 밖에 나갔다면
        {
            //포기
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
