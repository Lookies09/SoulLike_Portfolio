using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGiveupState_MidBoss : EnemyAttackAbleState
{
    // 복귀 위치 게임오브젝트 참조
    protected Transform targetTransform = null;

    // 배회 위치 (기본 : 무한 위치값)
    public Vector3 targetPosition;

    // 복귀시 이동 속도
    [SerializeField] protected float moveSpeed;

    // 복귀 위치와의 거리
    public float targetDistance = Mathf.Infinity;


    // 배회 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        targetTransform = controller.WanderPoints[0];
        targetPosition = targetTransform.position;
        // 배회 이동 속도를 네비게이션 에이전트에 설정
        navMeshAgent.speed = moveSpeed;

        // 배회 애니메이션 재생
        animator.SetInteger("State", (int)state);

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = moveSpeed;
        navMeshAgent.SetDestination(targetPosition);
    }

    // 배회 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {
        // 플레이어가 공격 가능 거리안에 들어오면
        if (controller.GetPlayerDistance() <= attackDistance)
        {
            // 공격 상태로 전환
            controller.TransactionToState(3);
            return;
        }

        // 플레이어가 추적 가능 거리안에 들어오면
        if (controller.GetPlayerDistance() <= detectDistance)
        {
            // 움직임 상태로 전환
            controller.TransactionToState(1);
            return;
        }

        // 배회할 이동 위치가 존재한다면
        if (targetTransform != null)
        {
            // 복귀 위치 근처에 도달했다면
            targetDistance = Vector3.Distance(transform.position, targetPosition);
            if (targetDistance < 1f)
            {
                // 대기 상태로 전환
                controller.TransactionToState(0);
            }
        }
    }

    // 배회 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
        // 배회 관련 위치 정보들 초기화
        targetTransform = null;
        targetPosition = Vector3.positiveInfinity;
        targetDistance = Mathf.Infinity;

        // 네비게이션 이동 종료
        navMeshAgent.isStopped = true;
    }
}
