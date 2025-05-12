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

    // �ʱ� ü��
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
        // ���� �ǰ� �Ұ��� ���¶�� �ƹ� ó�� X
        if (characterFightAction.CanHitOnExecution == false) { return; }

        // �׷α� ���¶��
        if (onGroggy)
        {
            // ü�� ���� * 1.5��
            Health -= (DMG*15/10);
        }
        // �ƴ϶��
        else
        {
            // ü�� ����
            Health -= DMG;
        }

        // ��Ʈ �Ҹ� ���
        hitAudio.clip = hitSound;
        hitAudio.Play();

        // ü�� ����
        Posture += DMG;

        // �ǰ� Ȯ��
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

            // �̵� ���� ó���ϱ� ����
            freeCam_Movement.OnAttack = true;
            targetFocus_Movement.OnAttack = true;
        }
    }


    public override void postureBreak()
    {
        // ü���� 0���Ϸ� �������� �ʰ� ����
        if (posture <= 0) { posture = 0; }

        // ü���� 100�� �Ǿ��ٸ�
        if (posture >= 99)
        {
            // �׷α� ���� ��
            onGroggy = true;

            // �̵� ���� ó���ϱ� ����
            freeCam_Movement.OnAttack = true;
            targetFocus_Movement.OnAttack = true;
            // ���� ��� ���� ó��
            characterDefense.CanDefense = false;
            characterFightAction.CanAttack = false;

            time += Time.deltaTime;            

            //  �ð��� ������
            if (time > groggyTime) 
            {
                // �׷α� ����, ü�� 0, �ð� 0
                onGroggy = false;
                posture = 0;
                time = 0;

                // �̵� ���� ����
                freeCam_Movement.OnAttack = false;
                targetFocus_Movement.OnAttack = false;

                // ���� ��� ���� ����
                characterDefense.CanDefense = true;
                characterFightAction.CanAttack = true;
            }                        
        }
        animator.SetFloat("Time", time);
        animator.SetBool("OnGroggy", onGroggy);
    }

    // ü�� ȸ��
    public void RecoveryHealth(int recoverPoint)
    {
        healEffect.Play();

        health += recoverPoint;

        // �ʱ� ü�º��� ���� ü���� Ŀ����
        if (health > firstHealth)
        {
            health = firstHealth;
        }        
    }

    // ���ݷ� �ø���
    public void DMGUpItem()
    {
        dmgUpEffect.Play();
    }
}
