using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState_LastBoss : EnemyAttackAbleState
{
    // ============================================
    // 공격 중 상태 확인
    private bool isOnAttack = false;

    // 시간
    private float time;

    // 공격 진입 시간
    private float ADelaytime = 0;

    // 랜덤 공격 타입
    private int ranAttackInt;

    private int ranPattern;

    private bool lookOn = true;

    private bool firstAttack = true;

    // 칼 트레일
    [SerializeField] private GameObject swordTrail;

    // 손에 들고있는 카타나
    [SerializeField] private GameObject handKatana;

    // 칼집에 들어있는 카타나
    [SerializeField] private GameObject unEquipKatana;

    // 전투 시 칼집
    [SerializeField] private GameObject scabBard;

    // 손에 들고있는 활
    [SerializeField] private GameObject handBow;

    // 등에 있는 활
    [SerializeField] private GameObject backBow;

    // 껏다 켰다하는 화살
    [SerializeField] private GameObject arrowOnHand;

    // 실제 발사되는 화살 프리펩
    [SerializeField] private GameObject arrowprefab;

    private Vector3 direction;

    // 발사 위치
    [SerializeField] private Transform shootPos;

    // 화살 타겟
    [SerializeField] private Transform Target;

    // ============================================
    // 활 오디로 소스
    [SerializeField] private AudioSource arrowAudio;

    // 칼 오디로 소스
    [SerializeField] private AudioSource katanaAudio;

    // 게임매니저 위험 오디오 소스
    [SerializeField] private AudioSource dangerAudioSource;

    // 목소리 오디로 소스
    [SerializeField] private AudioSource voiceAudioSource;

    // 화살 쏘는 소리
    [SerializeField] private AudioClip arrowShootClip;

    // 활 당기는 소리
    [SerializeField] private AudioClip bowStringClip;

    // 칼 휘두르는 소리
    [SerializeField] private AudioClip katanaSwingClip;

    // 기합 타입 1
    [SerializeField] private AudioClip voiceType1;

    // 기합 타입 2
    [SerializeField] private AudioClip voiceType2;

    // 기합 타입 3
    [SerializeField] private AudioClip voiceType3;

    // 기합 타입 4
    [SerializeField] private AudioClip voiceType4;

    // 기합 타입 5
    [SerializeField] private AudioClip voiceType5;

    // 기합 타입 6
    [SerializeField] private AudioClip voiceType6;

    // ============================================

    // 리바운드 확인
    private bool onRebound = false;
    public bool OnRebound { get => onRebound; set => onRebound = value; }

    // 공격 컨트롤러
    private AttackController attackController;

    [SerializeField] private GameObject dangerInfo;

    public override void Awake()
    {
        base.Awake();

        attackController = GetComponent<AttackController>();
    }

    // 공격 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        time = 0; ADelaytime = 0;

        // 네비게이션 에이전트 이동 정지
        navMeshAgent.isStopped = true;

        // 리바운드 해제
        OnRebound = false;
        isReboundAttack = false;

        // 공격 상태 실행 (3번
        animator.SetInteger("State", state);

        // 게임을 재생하고 첫 공격일 떄
        if (firstAttack)
        {
            // 시간 설정
            ADelaytime = 6.5f;

            // 강 공격 스타트
            attackController.TransactionToState(4);

            // 첫공격 끝
            firstAttack = false;

            return;
        }
        else // 첫공격 이후에는
        {
            // 랜덤 공격 패턴 추첨
            ranAttackInt = Random.Range(0, attackController.attackPatternState1.Length);
            
            // 공격 타입별 시간 입력
            if (ranAttackInt == 0) { ADelaytime = 1.2f; } // 기본 공격 (ㅇ)
            else if (ranAttackInt == 1) { ADelaytime = 3f; } // 기본 찌르기 공격
            else if (ranAttackInt == 2) { ADelaytime = 5f; } // 콤보 공격
            else if (ranAttackInt == 3) { ADelaytime = 5.5f; } // 점프 공격 (ㅇ)
            else if (ranAttackInt == 4) { ADelaytime = 6.5f; } // 활 버스트 공격 (ㅇ)
            else if (ranAttackInt == 5) { ADelaytime = 4.8f; } // 활 강 공격 (ㅇ)
            

            // 공격 컨트롤러에서 랜덤 패턴 실행
            attackController.TransactionToState(ranAttackInt);
        }
    }

    // 공격 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {
        LookAtTarget(lookOn);
        animator.SetFloat("Time", time);
        time += Time.deltaTime;

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
            objectHealth.Posture += 5;

            if (isReboundAttack)
            {
                // 리바운드 상태 전환
                controller.TransactionToState(7);
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
                // 걷기 모션 재생
                controller.TransactionToState(1);
                return;
            }
            else // 공격 가능 거리에서 벗어 났다면
            {
                // 걷기 모션 재생
                controller.TransactionToState(1);
                return;
            }
        }        
    }

    // 공격 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
        // 다른 행동 가능
        isOnAttack = true;
        time = 0;

        // 리바운드 해제
        OnRebound = false;

        isReboundAttack = false;
    }

    public void LookTargetEvent()
    {
        lookOn = true;
    }

    public void NoLookTargetEvent()
    {
        lookOn = false;
    }

    // 칼 장착 이벤트
    public void KatanaEquipEvent()
    {
        handKatana.SetActive(true);
        unEquipKatana.SetActive(false);
        scabBard.SetActive(true);
    }

    // 칼 해제 이벤트
    public void KatanaUnEquipEvent()
    {
        handKatana.SetActive(false);
        unEquipKatana.SetActive(true);
        scabBard.SetActive(false);
    }

    // 활 장착 이벤트
    public void BowEquipEvent()
    {
        handBow.SetActive(true);
        backBow.SetActive(false);        
    }

    // 활 해제 이벤트
    public void BowUnEquipEvent()
    {
        handBow.SetActive(false);
        backBow.SetActive(true);
    }

    // 화살 뽑는 애니메이션 이벤트
    public void ArrowPickup()
    {
        Debug.Log("화살뽑음");
        arrowOnHand.SetActive(true);
        
    }

    // 활 시위 당기는 애니메이션 이벤트
    public void BowStringEvent()
    {
        arrowAudio.clip = bowStringClip;
        arrowAudio.Play();
    }

    // 화살 발사하는 애니메이션 이벤트
    public void ShootArrow()
    {
        direction = (Target.position - shootPos.position).normalized;
        //direction.y = 0;

        Debug.Log("화살발사");
        arrowOnHand.SetActive(false);

        arrowAudio.clip = arrowShootClip;
        arrowAudio.Play();

        GameObject arrow = Instantiate(arrowprefab, shootPos.position, Quaternion.LookRotation(direction));
        arrow.GetComponent<ArrowMovement>().direction = direction;
        
    }


    public void DangerInfoOn()
    {
        // 소리 재생
        dangerAudioSource.Play();
        dangerInfo.SetActive(true);
    }

    public void DangerInfoOff()
    {
        dangerInfo.SetActive(false);
    }

    public void KatanaSwingSoundEvent()
    {
        katanaAudio.clip = katanaSwingClip;
        katanaAudio.Play();

    }

    // 기본 공격
    public void AVoiceType1()
    {
        voiceAudioSource.clip = voiceType1;
        voiceAudioSource.Play();
    }

    // 강 활쏘기 공격
    public void AVoiceType2()
    {
        voiceAudioSource.clip = voiceType2;
        voiceAudioSource.Play();
    }

    // 찌르기 공격
    public void AVoiceType3()
    {
        voiceAudioSource.clip = voiceType3;
        voiceAudioSource.Play();
    }

    // 점프 공격
    public void AVoiceType4()
    {
        voiceAudioSource.clip = voiceType4;
        voiceAudioSource.Play();
    }

    // 특별 공격 1 (베기)
    public void AVoiceType5()
    {
        voiceAudioSource.clip = voiceType5;
        voiceAudioSource.Play();
    }

    // 구르기 공격
    public void AVoiceType6()
    {
        voiceAudioSource.clip = voiceType6;
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
