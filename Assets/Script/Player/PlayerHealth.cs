using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : ObjectHealth
{
    private FreeCam_Movement freeCam_Movement;
    private TargetFocus_Movement targetFocus_Movement;
    private Character_Defense characterDefense;
    private Character_FightAction characterFightAction;

    [SerializeField] private AudioSource hitAudio;
    [SerializeField] private AudioClip hitSound;

    

    private bool onGroggy = false;

    [SerializeField] private float groggyTime;
    private float time;

    [SerializeField] private ParticleSystem healEffect;
    [SerializeField] private GameObject healText;

    [SerializeField] private ParticleSystem dmgUpEffect;
    [SerializeField] private GameObject dmgUpText;

    // 초기 체력
    private int firstHealth;

    public override void Awake()
    {
        firstHealth = health;

        base.Awake();
        freeCam_Movement = GetComponent<FreeCam_Movement>();
        targetFocus_Movement = GetComponent<TargetFocus_Movement>();
        characterDefense = GetComponent<Character_Defense>();
        characterFightAction = GetComponent<Character_FightAction>();
        StartHp = health;
    }

    private void Update()
    {
        Death();
        animator.SetBool("OnHit", isHit);
        postureBreak();

        if (healEffect.isPlaying == true)
        {
            healText.SetActive(true);
        }
        else
        { healText.SetActive(false); }

        if (dmgUpEffect.isPlaying == true)
        {
            dmgUpText.SetActive(true);
        }
        else
        { dmgUpText.SetActive(false); }

    }

    public override void Hit(int DMG)
    {
        // 만약 피격 불가능 상태라면 아무 처리 X
        if (characterFightAction.CanHitOnExecution == false) { return; }

        // 그로기 상태라면
        if (onGroggy)
        {
            // 체력 감소 * 1.5배
            Health -= (DMG*15/10);
        }
        // 아니라면
        else
        {
            // 체력 감소
            Health -= DMG;
        }

        // 히트 소리 재생
        hitAudio.clip = hitSound;
        hitAudio.Play();

        // 체간 증가
        Posture += DMG;

        // 피격 확인
        IsHit = true;
        GetComponent<Character_Defense>().ArrowRayHit = false;
        

        animator.SetTrigger("Hit");
    }

    public override void Death()
    {
        if (Health <= 0)
        {
            isDeath = true;
            animator.SetBool("Die", isDeath);

            // 이동 정지 처리하기 위해
            freeCam_Movement.OnAttack = true;
            targetFocus_Movement.OnAttack = true;
        }
    }


    public override void postureBreak()
    {
        // 체간이 0이하로 떨어지지 않게 조정
        if (posture <= 0) { posture = 0; }

        // 체간이 100이 되었다면
        if (posture >= 99)
        {
            // 그로기 상태 온
            onGroggy = true;

            // 이동 정지 처리하기 위해
            freeCam_Movement.OnAttack = true;
            targetFocus_Movement.OnAttack = true;
            // 공격 방어 정지 처리
            characterDefense.CanDefense = false;
            characterFightAction.CanAttack = false;

            time += Time.deltaTime;            

            //  시간이 지나면
            if (time > groggyTime) 
            {
                // 그로기 해제, 체간 0, 시간 0
                onGroggy = false;
                posture = 0;
                time = 0;

                // 이동 정지 해제
                freeCam_Movement.OnAttack = false;
                targetFocus_Movement.OnAttack = false;

                // 공격 방어 정지 해제
                characterDefense.CanDefense = true;
                characterFightAction.CanAttack = true;
            }                        
        }
        animator.SetFloat("Time", time);
        animator.SetBool("OnGroggy", onGroggy);
    }

    // 체력 회복
    public void RecoveryHealth(int recoverPoint)
    {
        healEffect.Play();

        health += recoverPoint;

        // 초기 체력보다 현재 체력이 커지면
        if (health > firstHealth)
        {
            health = firstHealth;
        }        
    }

    // 공격력 올리기
    public void DMGUpItem()
    {
        dmgUpEffect.Play();
    }
}
