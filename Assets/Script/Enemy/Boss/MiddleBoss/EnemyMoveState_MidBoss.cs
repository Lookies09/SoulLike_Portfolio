using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState_MidBoss : EnemyAttackAbleState
{
    // 이동 속도
    [SerializeField] protected float moveSpeed;

    // 시간
    private float time;

    // 랜덤 달리기 시간
    private float wTime;


    // 움직임 방향 (0 = L, 1 = R)
    private int direction;

    // 이동 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        wTime = Random.Range(1.5f, 3.3f);
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
        objectHealth.Posture -= 10 * Time.deltaTime;

        // 시간 흐르기 시작
        time += Time.deltaTime;

        // 대상 바라봄
        LookAtTarget(true);

        // 플레이어가 추적가능 거리에 들어오고
        if (controller.GetPlayerDistance() < detectDistance)
        {
            if (time > wTime) // 랜덤초가 지났다면
            {
                // 추적 상태로 전환
                controller.TransactionToState(2);
                return;
            }

            // 공격 범위 안에 들어왔고 공격한다면
            if (controller.GetPlayerDistance() < attackDistance && Input.GetMouseButtonDown(0))
            {
                // 방어
                controller.TransactionToState(7);
                return;
            }

            

        }
        // 추격거리에서 벗어났다면
        else 
        {
            // 귀환
            controller.TransactionToState(4);
            
        }
                       

    }

    // 배회 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
        time = 0;
        
    }


}
