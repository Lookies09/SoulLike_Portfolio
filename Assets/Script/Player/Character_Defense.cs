using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Defense : MonoBehaviour
{
    // 캐릭터 컨트롤러
    private CharacterController cc;

    // 캐릭터 공격 컴포넌트
    private Character_FightAction character_FightAction;

    // 애니메이터
    private Animator animator;

    // 방어 판별
    private bool isDefense;

    // 방어 애니메이션 조절
    private bool onDefense;

    // 방어 시간
    private float dTime;

    // 방어 가능 판별
    private bool canDefense = true;


    // 방어 이펙트
    [SerializeField] private GameObject defenseEffect;

    // 방어 이펙트 위치
    [SerializeField] private Transform defenseEffectPos;

    // 패링 타겟 중심점 위치
    [SerializeField] private Transform parryTransform;

    // 패링 대상 레이어
    [SerializeField] private LayerMask targetLayer;

    // 패링 범위
    [SerializeField] private float parryRadius;

    // 패링 이펙트
    [SerializeField] private GameObject parryEffect;

    // 방어 포즈 오디오 소스
    [SerializeField] private AudioSource poseAudioSource;

    // 방어 오디오클립
    [SerializeField] private AudioClip defenseClip;

    // 플레이어 체력 참조
     protected PlayerHealth playerHealth;

    public bool IsDefense { get => isDefense; set => isDefense = value; }
    public bool OnParry { get => onParry; set => onParry = value; }
    public bool ArrowRayHit { get => arrowRayHit; set => arrowRayHit = value; }
    public bool AfterParry { get => afterParry; set => afterParry = value; }
    public bool CanDefense { get => canDefense; set => canDefense = value; }

    // 패링 확인
    private bool onParry = false;

    // 화살 레이 충돌 확인
    private bool arrowRayHit;

    // 화살 제거 확인
    private bool afterParry;



    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
        character_FightAction = GetComponent<Character_FightAction>();
    }

    private void Update()
    {        
        Defense();
        
    }
    
    private void Defense()
    {
        if (Input.GetMouseButton(1) && canDefense)
        {
            animator.SetTrigger("Defense");
            if (Input.GetMouseButtonDown(1))
            {
                
                character_FightAction.AfterSpAttack();
            }

            // 방어 애니메이션 시작
            dTime += Time.deltaTime;
            onDefense = true;
        }
        else
        {
            // 방어 해제
            isDefense = false;
            // 방어 애니메이션 해제
            onDefense = false;

            dTime = 0;

        }

        animator.SetBool("IsDefense", onDefense);
        animator.SetFloat("DTime", dTime);
    }

    public void DefenseHit(int DMG)
    {

        Instantiate(defenseEffect, defenseEffectPos.position, Quaternion.identity);
        animator.SetTrigger("DefenseHit");
        arrowRayHit = false;
        // 체간 증가
        playerHealth.Posture += DMG*75/100;

    }

    public void Parry()
    {        

        if (arrowRayHit)
        {
            Debug.Log("화살 패링");
            animator.SetTrigger("Parry");            
            Instantiate(parryEffect, defenseEffectPos.position, defenseEffectPos.rotation);
            afterParry = true;
        }

        Collider[] hits = Physics.OverlapSphere(parryTransform.position, parryRadius, targetLayer);
        
        foreach (Collider hit in hits)
        {
            // 검출된게 적군이면
            if ((hit.tag == "Enemy"))
            {
                // 패링처리
                if (hit.GetComponent<EnemyAttackAbleState>().IsParry)
                {
                    animator.SetTrigger("Parry");                    
                    Instantiate(parryEffect, defenseEffectPos.position, defenseEffectPos.rotation);
                    OnParry = true;
                }

            }
            // 아니면 
            else
            {
                break;
            }
        }
    }
    public void DefenseOn()
    {
        isDefense = true;
    }

    public void DefenseOff()
    {
        isDefense = false;
    }


    public void ParryEnd()
    {
        OnParry = false;
        afterParry = false;
        arrowRayHit = false;
    }



    // 방어포즈할때 찰칵 소리
    public void DefensePoseSound()
    {
        poseAudioSource.clip = defenseClip;        
        poseAudioSource.pitch = 0.8f;
        poseAudioSource.Play();
    }
}
