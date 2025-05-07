using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_FightAction : MonoBehaviour
{
    // 캐릭터 컨트롤러
    private CharacterController cc;

    // 움직임 컴포넌트
    private Movement movement;

    // 애니메이터
    private Animator animator;

    // 공격 계산 시간
    private float Atime;

    // 콤버 공격 번호
    private int attack;

    // 공격 타겟 중심점 위치
    [SerializeField] private Transform atteckTransform;

    // 공격 대상 레이어
    [SerializeField] private LayerMask targetLayer;

    // 공격 범위
    [SerializeField] private float atteckRadius;

    // 공격 범위 각도
    [SerializeField] private float hitAngle;

    // 칼 이펙트 생성 위치
    [SerializeField] private Transform sowrdEffectPos;

    // 적 히트 이펙트
    [SerializeField] private GameObject hitEffect;

    // 공격력
    [SerializeField] private int DMG;

    // 처형 카메라
    [SerializeField] private GameObject excutionCam;

    // 칼 트레일
    [SerializeField] private GameObject swordTrail;

    // 오디오 소스
    [SerializeField] private AudioSource audioSource;

    // 공격 오디오클립
    [SerializeField] private AudioClip attackClip;

    // 특별공격 오디오클립
    [SerializeField] private AudioClip spAttackClip;

    // 공격 후 오디오클립
    [SerializeField] private AudioClip affterAttackClip;

    // 공격 가능 판별
    private bool canAttack = true;

    // 처형 중 피격 확인
    private bool canHitOnExecution = true;

    // 특별 공격 판별
    private bool onSpAt;

    public bool CanAttack { get => canAttack; set => canAttack = value; }
    public bool OnSpAt { get => onSpAt; set => onSpAt = value; }
    public bool CanHitOnExecution { get => canHitOnExecution; set => canHitOnExecution = value; }

    private bool onExecution;

    // 보스 처형 이벤트
    private bool isBoss;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        Atime += Time.deltaTime;

        Attack();


    }


    private void Attack()
    {
        if (Input.GetMouseButton(0) && Input.GetMouseButton(1) && canAttack && movement.Grounded)
        {
            OnSpAt = true;

            // 움직임 정지
            StopMovement();
            return;
        }

        if (Input.GetMouseButtonDown(0) && canAttack && movement.Grounded && !OnSpAt)
        {
            animator.SetTrigger("Attack");

            gameObject.GetComponent<Character_Defense>().CanDefense = false;

            Collider[] enemys = Physics.OverlapSphere(atteckTransform.position, atteckRadius, targetLayer);

            foreach (Collider enemy in enemys)
            {
                // 처형 이벤트
                if (enemy.GetComponent<ObjectHealth>().Health <= 0 || enemy.GetComponent<ObjectHealth>().Posture >= 100)
                {
                    if (enemy.GetComponent<ObjectHealth>().IsDead) { return; }
                    if (enemy.name == "Samurai_LastBoss")
                    {
                        excutionCam.SetActive(true);
                        isBoss = true;
                    }
                    else
                    {
                        isBoss = false;
                    }
                    onExecution = true;
                    canAttack = false;                    
                    
                    // 피격 불가능
                    CanHitOnExecution = false;

                    // 움직임 정지
                    StopMovement();

                    // 애니메이션 재생
                    animator.SetBool("Execution", true);
                    enemy.GetComponent<ObjectHealth>().Death();
                                        
                    return;                    
                }

            }


            if (attack == 0 && Atime >= 0.3f && onExecution  == false)
            {
                attack++;
                Atime = 0;

                // 움직임 정지
                StopMovement();


            }
            else if (attack == 1 && Atime >= 0.5f)
            {
                Atime = 0;
                attack++;
            }
            else if (attack == 2 && Atime >= 0.5f)
            {
                Atime = 0;
                attack++;
            }
            else if (attack == 3 && Atime >= 0.5f)
            {
                Atime = 0;
                attack++;
            }

        }
        if (attack > 0 && Atime >= 0.9f)
        {
            attack = 0;
            gameObject.GetComponent<Character_Defense>().CanDefense = true;

            // 움직임 풀기
            PlayAllAction();
        }

        animator.SetInteger("AttackType", attack);
        animator.SetBool("SPAT", OnSpAt);
    }





    public void AttackHitAnimationEvent()
    {
        // 애니메이션 이벤트 중 1프레임 동안 충돌 체크
        // * Physics.OverlapSphere(충돌체크 중심점, 충돌 체크 범위, 대상 레이어)
        // - 레이캐스트 처럼 해당 메소드가 실행되는 순간 설정 영역안에 있는 충돌 대상들을 검출함
        Collider[] hits = Physics.OverlapSphere(atteckTransform.position, atteckRadius, targetLayer);

        // 피격된 대상들 중 지정된 각도 안에 있는 대상을 타격함
        foreach (Collider hit in hits)
        {
            if (!hit.tag.Equals("Enemy")) break;

            // 플레이어가 타격을 향한 방향벡터를 구함
            Vector3 directionToTarget = hit.transform.position - transform.position;

            // 타격 대상과의 시선 각도를 구함
            float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

            if (angleToTarget < hitAngle)
            {
                hit.GetComponent<ObjectHealth>().Hit(DMG);                
            }

        }

    }

    public void SpacialAttackAnimationEvent()
    {
        // 애니메이션 이벤트 중 1프레임 동안 충돌 체크
        // * Physics.OverlapSphere(충돌체크 중심점, 충돌 체크 범위, 대상 레이어)
        // - 레이캐스트 처럼 해당 메소드가 실행되는 순간 설정 영역안에 있는 충돌 대상들을 검출함
        Collider[] hits = Physics.OverlapSphere(atteckTransform.position, atteckRadius, targetLayer);

        // 피격된 대상들 중 지정된 각도 안에 있는 대상을 타격함
        foreach (Collider hit in hits)
        {
            if (!hit.tag.Equals("Enemy")) break;

            // 플레이어가 타격을 향한 방향벡터를 구함
            Vector3 directionToTarget = hit.transform.position - transform.position;

            // 타격 대상과의 시선 각도를 구함
            float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

            if (angleToTarget < hitAngle)
            {
                hit.GetComponent<ObjectHealth>().Hit(3);
            }

        }

    }

    public void HitEffectEvent()
    {
        Collider[] hits = Physics.OverlapSphere(atteckTransform.position, atteckRadius, targetLayer);

        foreach (Collider hit in hits)
        {
            if (!hit.tag.Equals("Enemy")) break;

            // 플레이어가 타격을 향한 방향벡터를 구함
            Vector3 directionToTarget = hit.transform.position - transform.position;

            // 타격 대상과의 시선 각도를 구함
            float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

            if (angleToTarget < hitAngle)
            {
                // 대상이 피격됐다면
                if (hit.GetComponent<ObjectHealth>().IsHit)
                {
                    // 이펙트 생성
                    Instantiate(hitEffect, sowrdEffectPos.position, Quaternion.identity);
                }


            }

        }

    }


    public void SwordTrailEventOn()
    {
        swordTrail.SetActive(true);
    }

    public void SwordTrailEventEnd()
    {
        swordTrail.SetActive(false);
    }

    public void SwordSound()
    {
        audioSource.clip = attackClip;
        audioSource.volume = 0.3f;
        audioSource.pitch = 1f;
        audioSource.Play();
    }

    public void AfterSwordSound()
    {
        audioSource.clip = affterAttackClip;
        audioSource.volume = 0.2f;
        audioSource.pitch = 1f;
        audioSource.Play();
    }

    public void SPSwordSound()
    {
        audioSource.clip = spAttackClip;
        audioSource.volume = 0.2f;
        audioSource.Play();
    }

    public void StopMovement()
    {
        // 움직임 정지
        GetComponent<TargetFocus_Movement>().OnAttack = true;
        GetComponent<FreeCam_Movement>().OnAttack = true;
    }


    // 처형 후 움직임 풀어주는 이벤트
    public void AfterExcutionEvenet()
    {
        animator.SetBool("Execution", false);
        // 다시 피격 가능
        canHitOnExecution = true;

        // 움직임 정지 해제
        GetComponent<TargetFocus_Movement>().OnAttack = false;
        GetComponent<FreeCam_Movement>().OnAttack = false;

        onExecution = false;
        canAttack = true;

        excutionCam.SetActive(false);
    }

    public void StopAllAction()
    {
        canAttack = false;
        gameObject.GetComponent<Character_Defense>().CanDefense = false;
        StopMovement();
    }

    public void PlayAllAction()
    {
        canAttack = true;
        gameObject.GetComponent<Character_Defense>().CanDefense = true;
        // 움직임 정지 해제
        GetComponent<TargetFocus_Movement>().OnAttack = false;
        GetComponent<FreeCam_Movement>().OnAttack = false;
        
    }

    public void AfterSpAttack() 
    {
        OnSpAt = false;

        PlayAllAction();
    }

    public void ExecutionTimeScaleCtrl(float time)
    {
        if(isBoss)
        {
            Time.timeScale = time;
        }        
    }

}
