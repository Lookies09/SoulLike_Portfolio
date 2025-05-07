using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState_MidBoss : EnemyAttackAbleState
{
    // ============================================
    // ���� �� ���� Ȯ��
    private bool isOnAttack = false;

    // �ð�
    private float time;

    // ���� ���� �ð�
    private float ADelaytime = 0;

    // ���� ���� Ÿ��
    private int ranAttackInt;

    private int ranPattern;

    private bool lookOn = true;

    private bool firstAttack = true;

    [SerializeField] private GameObject dangerInfo;

    // Į Ʈ����
    [SerializeField] private GameObject swordTrail;

    // ��Ҹ� ����� �ҽ�
    [SerializeField] private AudioSource voiceAudioSource;

    // ���ӸŴ��� ���� ����� �ҽ�
    [SerializeField] private AudioSource dangerAudioSource;

    // Į ����� �ҽ�
    [SerializeField] private AudioSource katanaAudio;

    // Į �ֵθ��� �Ҹ�
    [SerializeField] private AudioClip katanaSwingClip;

    // ���� Ÿ�� 1
    [SerializeField] private AudioClip voiceType1;

    // ���� Ÿ�� 2
    [SerializeField] private AudioClip voiceType2;

    // ���� Ÿ�� 3
    [SerializeField] private AudioClip voiceType3;

    // ���� Ÿ�� 4
    [SerializeField] private AudioClip voiceType4;

    // ���� Ÿ�� 5
    [SerializeField] private AudioClip voiceType5;
    // ============================================

    // ���ٿ�� Ȯ��
    private bool onRebound = false;
    public bool OnRebound { get => onRebound; set => onRebound = value; }

    // ���� ��Ʈ�ѷ�
    private AttackController attackController;



    public override void Awake()
    {
        base.Awake();

        attackController = GetComponent<AttackController>();
    }

    // ���� ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        time = 0; ADelaytime = 0;

        // �׺���̼� ������Ʈ �̵� ����
        navMeshAgent.isStopped = true;

        // ���ٿ�� ����
        OnRebound = false;
        isReboundAttack = false;

        // ���� ���� ���� (3��
        animator.SetInteger("State", state);

        // ������ ����ϰ� ù ������ ��
        if (firstAttack)
        {
            // �ð� ����
            ADelaytime = 2.5f;

            // �� ���� ��ŸƮ
            attackController.TransactionToState(3);

            // ù���� ��
            firstAttack = false;

            return;
        }
        else // ù���� ���Ŀ���
        {
            // ���� ���� ���� ��÷
            ranAttackInt = Random.Range(0, attackController.attackPatternState1.Length);

            // ���� Ÿ�Ժ� �ð� �Է�
            if (ranAttackInt == 0) { ADelaytime = 1.4f; } // �⺻ ����
            else if (ranAttackInt == 1) { ADelaytime = 3f; } // ���� ����
            else if (ranAttackInt == 2) { ADelaytime = 2f; } // ���� ����
            else if (ranAttackInt == 3) { ADelaytime = 2.5f; } // ������
            else if (ranAttackInt == 4) { ADelaytime = 0.6f; } // ��뽬

            // ���� ��Ʈ�ѷ����� ���� ���� ����
            attackController.TransactionToState(ranAttackInt);
        }
    }

    // ���� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        LookAtTarget(lookOn);
        time += Time.deltaTime;

        animator.SetFloat("Time", time);


        // ���� ���ӽð��� �����ٸ�
        if (time > ADelaytime)
        {
            isOnAttack = true;            
        }
        else // �ƴϸ�
        {
            isOnAttack = false;
        }

        // ================= ���� ���� ========================
       
        // �� �뽬�ߴٸ�
        if (ranAttackInt == 4 && time >= 0.6f)
        {           
             // �ȱ�
             controller.TransactionToState(1);       
            
        }

        // ================================================

        // ���ٿ�� üũ
        if (onRebound)
        {
            // ���⼭ ü�� �������� ���� ���� ������Ʈ���� �����ؾ���            
            objectHealth.Posture += 5;

            if (isReboundAttack)
            {
                // ���ٿ�� ���� ��ȯ
                controller.TransactionToState(8);
            }


            onRebound = false;
            return;
        }

        // ����� ���� ���� �Ÿ� �ȿ� �����鼭 ���� �ð��� ������ ��
        if (controller.GetPlayerDistance() < detectDistance && isOnAttack)
        {
            // ���� ���� �Ÿ� �ȿ� ������
            if (controller.GetPlayerDistance() < attackDistance)
            {
                // ���� ��� ���
                controller.TransactionToState(3);
                return;
            }
            else // ���� ���� �Ÿ����� ���� ���ٸ�
            {
                // �ȱ� ��� ���
                controller.TransactionToState(1);
                return;
            }
        }
        else if(controller.GetPlayerDistance() >= detectDistance && isOnAttack) // ���� ���� �Ÿ����� ����ٸ�
        {
            // ó�� ��ġ�� ����
            controller.TransactionToState(4);
            
        }
    }

    // ���� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
        // �ٸ� �ൿ ����
        isOnAttack = true;
        time = 0;

        // ���ٿ�� ����
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

    public void DangerInfoOn()
    {
        // �Ҹ� ���
        dangerAudioSource.Play();
        dangerInfo.SetActive(true);
    }

    public void DangerInfoOff()
    {
        dangerInfo.SetActive(false);
    }

    public void SwordTrailEventOn()
    {
        swordTrail.SetActive(true);
    }

    public void SwordTrailEventEnd()
    {
        swordTrail.SetActive(false);
    }

    // �⺻ ����
    public void AVoiceType1()
    {
        voiceAudioSource.clip = voiceType1;
        voiceAudioSource.Play();
    }

    // ���� ���� 1
    public void AVoiceType2()
    {
        voiceAudioSource.clip = voiceType2;
        voiceAudioSource.Play();
    }

    // ���� ���� 2
    public void AVoiceType3()
    {
        voiceAudioSource.clip = voiceType3;
        voiceAudioSource.Play();
    }

    // ���� ����
    public void AVoiceType4()
    {
        voiceAudioSource.clip = voiceType4;
        voiceAudioSource.Play();
    }

    // Ư�� ����
    public void AVoiceType5()
    {
        voiceAudioSource.clip = voiceType5;
        voiceAudioSource.Play();
    }

    public void KatanaSwingSoundEvent()
    {
        katanaAudio.clip = katanaSwingClip;
        katanaAudio.Play();

    }
}
