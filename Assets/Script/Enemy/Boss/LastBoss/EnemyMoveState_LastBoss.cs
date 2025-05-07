using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState_LastBoss : EnemyAttackAbleState
{
    // 여기서 옆으로 달리는 것도 신경 써줘야 함


    // 이동 속도
    [SerializeField] protected float moveSpeed;

    // 시간
    private float time;

    // 랜덤 걷기 시간
    private float wTime;


    // 움직임 방향 (0 = L, 1 = R)
    private int direction;

    // 이동 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        // 시간 랜덤 추첨
        wTime = Random.Range(1f, 2f);
        // 방향 랜덤 추첨
        direction = Random.Range(0, 2);

        // 이동 속도를 네비게이션 에이전트에 설정
        navMeshAgent.speed = moveSpeed;

        // 이동 애니메이션 재생
        animator.SetInteger("State", (int)state);
        animator.SetInteger("MoveDirec", direction);

    }

    // 이동 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {
        // 시간 흐르기 시작
        time += Time.deltaTime;
                
        animator.SetFloat("Time", time);

        // 대상 바라봄
        LookAtTarget(true);

        // 플레이어가 추적가능 거리에 들어오고
        if (controller.GetPlayerDistance() < detectDistance)
        {
            // 공격가능 거리에 들어오고
            if (controller.GetPlayerDistance() < attackDistance)
            {                
                // 플레이어가 공격 한다면
                if (Input.GetMouseButtonDown(0))
                {
                    controller.TransactionToState(6);
                    return;
                }
                

            }

            if (time > wTime) // 랜덤초가 지났다면
            {
                // 추적 상태로 전환
                controller.TransactionToState(2);

                return;

            }

        }        


    }

    // 배회 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
        time = 0;        
    }
}
