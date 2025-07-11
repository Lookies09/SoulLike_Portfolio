using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyAttackAbleState
{
    [SerializeField] protected float time; // 시간 계산용
    [SerializeField] protected float checkTime; // 대기 체크 시간
    [SerializeField] protected Vector2 checkTimeRange; // 대기 체크 시간 (최소,최대)


    // 대기 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        // 상태 체크 주기 시간을 추첨함
        time = 0;
        checkTime = Random.Range(checkTimeRange.x, checkTimeRange.y);

        // 네비게이션 에이전트 이동 정지
        navMeshAgent.isStopped = true;

        // 대기 애니메이션 재생
        animator.SetInteger("State", (int)state);
    }

    // 대기 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {       

        time += Time.deltaTime; // 대기 시간 계산

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
            // 추적 상태로 전환
            controller.TransactionToState(2);
            return;
        }

        // 대기 상태가 지났다면
        if (time > checkTime)
        {
            int selectState = Random.Range(0, 2); // 대기, 배회 상태 추첨

            switch (selectState)
            {
                case 0:
                    // 다시 대기 상태 유지
                    time = 0;
                    checkTime = Random.Range(checkTimeRange.x, checkTimeRange.y);
                    break;
                case 1:
                    // 배회 상태로 전환
                    controller.TransactionToState(1);
                    return;
            }
        }
    }

    // 대기 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
        time = 0;
    }
}
