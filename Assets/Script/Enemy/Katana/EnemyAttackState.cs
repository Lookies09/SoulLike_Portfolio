using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : EnemyAttackAbleState
{
    [SerializeField] protected float smoothValue; // ȸ�� ���� ��ġ

    // ���� �� ���� Ȯ��
    private bool isOnAttack;

    // �ð�
    private float time = 0;

    // ���� ���� �ð�
    private float ADelaytime;

    // ���� ���� Ÿ��
    private int ranInt = 0;

    // Į Ʈ����
    [SerializeField] private GameObject swordTrail;
        

    // ��Ҹ� ����� �ҽ�
    [SerializeField] private AudioSource voiceAudioSource;

    // ���� Ÿ�� 1
    [SerializeField] private AudioClip voiceType1;

    // ���� Ÿ�� 2
    [SerializeField] private AudioClip voiceType2;

    // ���� Ÿ�� 3
    [SerializeField] private AudioClip voiceType3;

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
        // ���� ���� ���� (3��
        animator.SetInteger("State", state);

        // ������ ���� �̵� ����
        navMeshAgent.isStopped = true;
        
        // ���� ���� �ִϸ��̼� �̱�
        ranInt = Random.Range(0, 10);

        if (ranInt != 0)
        {
            if (ranInt > 3)
            {
                // �Ϲ� ����
                attackController.TransactionToState(0);
                ADelaytime = 1.5f;
                return;
            }
            else
            {
                // ��� ����
                attackController.TransactionToState(1);
                ADelaytime = 2f;
                return;
            }
        }
        

        OnRebound = false;

    }

    // ���� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        time += Time.deltaTime;

        LookAtTarget(true);

        // ���� ���ӽð��� �����ٸ�
        if (time > ADelaytime)
        {
            isOnAttack = true;

        }
        else // �ƴϸ�
        {

            isOnAttack = false;
        }

        // ���ٿ�� üũ
        if (onRebound)
        {
            // ���⼭ ü�� �������� ���� ���� ������Ʈ���� �����ؾ���            
            objectHealth.Posture += 15;

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
                // ��� ��� ���
                controller.TransactionToState(7);
            }
            else // ���� ���� �Ÿ����� ���� ���ٸ�
            {
                // ���� ��� ���
                controller.TransactionToState(2);
            }
        }
        else if (controller.GetPlayerDistance() >= detectDistance && isOnAttack) // ���� ���� �Ÿ����� ����ٸ�
        {
            // ��ȸ
            controller.TransactionToState(4);
            
        }

        
    }

    // ���� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
        // �ٸ� �ൿ ����
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
