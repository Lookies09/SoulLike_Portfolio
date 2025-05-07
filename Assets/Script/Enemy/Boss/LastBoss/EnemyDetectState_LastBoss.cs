using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectState_LastBoss : EnemyAttackAbleState
{
    // 추적 이동 속도
    [SerializeField] protected float detectSpeed = 1;

    // 움직임 방향 (0 = F, 1 = FL, 2 = FR)
    private int direction;

    // 시간
    private float time = 0;

    // 추적 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        // 추적 속도 설정
        //navMeshAgent.speed = detectSpeed;

        // 방향 랜덤 추첨
        direction = Random.Range(0, 3);

        // 추적 애니메이션 재생
        animator.SetInteger("State", (int)state);
        animator.SetInteger("DetectDirec", 0);


    }

    // 추적 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {
        time += Time.deltaTime;
        animator.SetFloat("Time", time);

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
            // 추적 대상이 공격 범위에 안들어오면 그대로 추격
            else
            {
                // 공격 대상 추적 처리
                //navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(controller.Player.transform.position);
            }


        }

    }

    // 추적 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
        time = 0;
    }
}
