using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : EnemyAttackAbleState
{
    [SerializeField] protected float smoothValue; // 회전 보간 수치

    // 공격 중 상태 확인
    private bool isOnAttack;

    // 시간
    private float time = 0;

    // 공격 진입 시간
    private float ADelaytime;

    // 랜덤 공격 타입
    private int ranInt = 0;

    // 칼 트레일
    [SerializeField] private GameObject swordTrail;
        

    // 목소리 오디오 소스
    [SerializeField] private AudioSource voiceAudioSource;

    // 기합 타입 1
    [SerializeField] private AudioClip voiceType1;

    // 기합 타입 2
    [SerializeField] private AudioClip voiceType2;

    // 기합 타입 3
    [SerializeField] private AudioClip voiceType3;

    // 리바운드 확인
    private bool onRebound = false;
    public bool OnRebound { get => onRebound; set => onRebound = value; }

    

    // 공격 컨트롤러
    private AttackController attackController;

    public override void Awake()
    {
        base.Awake();

        attackController = GetComponent<AttackController>();
    }

    // 공격 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        // 공격 상태 실행 (3번
        animator.SetInteger("State", state);

        // 공격을 위해 이동 중지
        navMeshAgent.isStopped = true;
        
        // 랜덤 공격 애니메이션 뽑기
        ranInt = Random.Range(0, 10);

        if (ranInt != 0)
        {
            if (ranInt > 3)
            {
                // 일반 공격
                attackController.TransactionToState(0);
                ADelaytime = 1.5f;
                return;
            }
            else
            {
                // 찌르기 공격
                attackController.TransactionToState(1);
                ADelaytime = 2f;
                return;
            }
        }
        

        OnRebound = false;

    }

    // 공격 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {
        time += Time.deltaTime;

        LookAtTarget(true);

        // 공격 지속시간이 지났다면
        if (time > ADelaytime)
        {
            isOnAttack = true;

        }
        else // 아니면
        {

            isOnAttack = false;
        }

        // 리바운드 체크
        if (onRebound)
        {
            // 여기서 체간 게이지만 증가 여기 업데이트여서 조심해야함            
            objectHealth.Posture += 15;

            if (isReboundAttack)
            {
                // 리바운드 상태 전환
                controller.TransactionToState(8);                
            }


            onRebound = false;
            return;
        }

        // 대상이 추적 가능 거리 안에 있으면서 공격 시간이 지났을 때
        if (controller.GetPlayerDistance() < detectDistance && isOnAttack)
        {
            // 공격 가능 거리 안에 있으면
            if (controller.GetPlayerDistance() < attackDistance)
            {
                // 방어 모션 재생
                controller.TransactionToState(7);
            }
            else // 공격 가능 거리에서 벗어 났다면
            {
                // 추적 모션 재생
                controller.TransactionToState(2);
            }
        }
        else if (controller.GetPlayerDistance() >= detectDistance && isOnAttack) // 추적 가능 거리에서 벗어났다면
        {
            // 배회
            controller.TransactionToState(4);
            
        }

        
    }

    // 공격 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
        // 다른 행동 가능
        isOnAttack = true;
        time = 0;

        OnRebound = false;
        ranInt = 0;

        isReboundAttack = false;
    }
    

    public void AVoiceType1()
    {
        voiceAudioSource.clip = voiceType1;
        voiceAudioSource.Play();
    }

    public void AVoiceType2()
    {
        voiceAudioSource.clip = voiceType2;
        voiceAudioSource.Play();
    }

    public void AVoiceType3()
    {
        voiceAudioSource.clip = voiceType3;
        voiceAudioSource.Play();
    }

    public void SwordTrailEventOn()
    {
        swordTrail.SetActive(true);
    }

    public void SwordTrailEventEnd()
    {
        swordTrail.SetActive(false);
    }
}
