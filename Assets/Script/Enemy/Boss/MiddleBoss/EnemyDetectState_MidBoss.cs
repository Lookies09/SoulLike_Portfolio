using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectState_MidBoss : EnemyAttackAbleState
{
    // 추적 이동 속도
    [SerializeField] protected float detectSpeed = 1;


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


        // 추적 대상이 추적 가능거리 안에 있고
        if (controller.GetPlayerDistance() < detectDistance)
        {
            // 추적 대상이 공격 가능 거리안으로 들어왔다면
            if (controller.GetPlayerDistance() < attackDistance)
            {

                // 공격 상태 전환
                controller.TransactionToState(3);
                return;
            }
            else
            {
                // 공격 대상 추적 처리
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(controller.Player.transform.position);
            }
            

        }
        else // 추적 대상이 추적 가능거리 밖으로 나갔다면
        {
            // 초기 Idle 위치로 복귀함 - GiveUp
            controller.TransactionToState(4);
            return;
        }

    }

    // 추적 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {

    }

    
}
