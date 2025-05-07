using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefenseState_MidBoss : EnemyAttackAbleState
{
    // 시간
    private float time;    


    // 방어 지속 시간
    private float dftime;

    public override void EnterState(int state)
    {

        dftime = Random.Range(1, 1.5f);

        // 방어를 위해 이동 중지
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0f;

        // 방어 상태 애니메이션 재생
        animator.SetInteger("State", (int)state);


    }

    // 방어 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {
        // 대상을 주시
        LookAtTarget(true);

        time += Time.deltaTime;
        animator.SetFloat("Time", time);

        // 공격 대상이 공격 가능 거리보다 멀어졌다면
        if (controller.GetPlayerDistance() > attackDistance)
        {
            // 공격 대상이 추격 가능거리보다 멀어졌다면
            if (controller.GetPlayerDistance() > detectDistance)
            {
                // 배회 위치로 복귀
                controller.TransactionToState(4);
                return;
            }
            // 공격 대상이 추격 가능거리 안에 있다면
            else
            {                
                // 움직임 상태
                controller.TransactionToState(1);
            }


        }
        // 공격 대상이 공격 가능 거리에 있다면
        else
        {
            // 랜덤 시간 동안 방어
            if (time > dftime)
            {                
                // 공격 상태로 전환
                controller.TransactionToState(3);
            }


        }


    }

    // 방어 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
        time = 0;
    }
}
