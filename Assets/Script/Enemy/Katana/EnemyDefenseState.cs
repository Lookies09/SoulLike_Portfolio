using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefenseState : EnemyAttackAbleState
{

    private float time;


    public override void EnterState(int state)
    {

        // 방어를 위해 이동 중지
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0f;

        // 방어 상태 애니메이션 재생
        animator.SetInteger("State", (int)state);
    }

    // 방어 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {
        time += Time.deltaTime;

        LookAtTarget(true);

        // 추격 가능거리 안에 있고
        if (controller.GetPlayerDistance() < detectDistance)
        {
            // 공격 가능 거리안에 있으면
            if (controller.GetPlayerDistance() < attackDistance)
            {
                // 시간이 0.8초 지났다면
                if (time > 0.8f)
                {
                    // 공격 상태로 전환함
                    controller.TransactionToState(3);
                    return;
                }                
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

    // 방어 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
        time = 0;
    }


}
