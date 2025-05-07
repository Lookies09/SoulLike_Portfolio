using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyDetectState : EnemyAttackAbleState
{
    // 추적 이동 속도
    [SerializeField] protected float detectSpeed;

    // 추적 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        // 추적 애니메이션 재생
        animator.SetInteger("State", (int)state);

        // 공격 대상 추적 처리
        navMeshAgent.isStopped = false;
        navMeshAgent.updateRotation = false;

        // 추적 속도 설정
        navMeshAgent.speed = detectSpeed;

    }

    // 추적 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {   
        
        navMeshAgent.SetDestination(controller.Player.transform.position);
        LookAtTarget(true);

        // 추격 가능거리 안에 있고
        if (controller.GetPlayerDistance() < detectDistance)
        {
            // 공격 가능 거리안에 있으면
            if (controller.GetPlayerDistance() < attackDistance)
            {
                // 방어 상태로 전환함
                controller.TransactionToState(7);
                return;
            }
            // 공격 거리 밖에 있다면
            else
            {
                // 추격 상태로 전환함
                controller.TransactionToState(2);
                return;
            }
            
        }
        // 추격 가능거리 밖에 있다면
        else
        {
            // 기지(배회 위치)로 복귀함
            controller.TransactionToState(4);
            return;
        }
        
       
    }

    // 추적 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
        
    }

    
}
