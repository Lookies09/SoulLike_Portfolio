using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState_Bow : EnemyAttackAbleState
{

    // 공격 중 상태 확인
    private bool isOnAttack;

    // 시간
    private float time;

    // 공격 진입 시간
    [SerializeField] private float ADelaytime;

    // 목소리 오디오 소스
    [SerializeField] private AudioSource voiceAudio;

    // 기합 소리
    [SerializeField] private AudioClip attackClip;


    // 공격 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        // 공격 애니메이션 재생
        animator.SetInteger("State", 2);

        // 공격을 위해 이동 중지
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0f;
        

    }

    // 공격 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {
        time += Time.deltaTime;
        animator.SetFloat("Time", time);
        LookAtTarget(true);

        // 공격 모션에 들어가고 ADelaytime초가 지나지 않으면
        if (time < ADelaytime)
        {
            // 다른 행동 불가
            isOnAttack = false;
        }
        else
        {
            // 다른 행동 불가
            isOnAttack = true;
        }




        // 공격 대상이 공격 가능 거리보다 멀어졌다면
        if (controller.GetPlayerDistance() > attackDistance && isOnAttack)
        {
            
            // 배회 위치로 복귀
            controller.TransactionToState(3);
            return;
          
        }
        // 공격 대상이 공격 가능 거리에 있다면
        else if(controller.GetPlayerDistance() <= attackDistance && isOnAttack)
        {
            // 재 공격
            controller.TransactionToState(2);            
        }


    }

    // 공격 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
        // 다른 행동 가능
        isOnAttack = true;
        time = 0;
    }


    public void AttackSoundEvent()
    {
        voiceAudio.clip = attackClip;
        voiceAudio.Play();

    }

}
