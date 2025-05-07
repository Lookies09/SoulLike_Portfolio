using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState_LastBoss : EnemyAttackAbleState
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

    // Į Ʈ����
    [SerializeField] private GameObject swordTrail;

    // �տ� ����ִ� īŸ��
    [SerializeField] private GameObject handKatana;

    // Į���� ����ִ� īŸ��
    [SerializeField] private GameObject unEquipKatana;

    // ���� �� Į��
    [SerializeField] private GameObject scabBard;

    // �տ� ����ִ� Ȱ
    [SerializeField] private GameObject handBow;

    // � �ִ� Ȱ
    [SerializeField] private GameObject backBow;

    // ���� �״��ϴ� ȭ��
    [SerializeField] private GameObject arrowOnHand;

    // ���� �߻�Ǵ� ȭ�� ������
    [SerializeField] private GameObject arrowprefab;

    private Vector3 direction;

    // �߻� ��ġ
    [SerializeField] private Transform shootPos;

    // ȭ�� Ÿ��
    [SerializeField] private Transform Target;

    // ============================================
    // Ȱ ����� �ҽ�
    [SerializeField] private AudioSource arrowAudio;

    // Į ����� �ҽ�
    [SerializeField] private AudioSource katanaAudio;

    // ���ӸŴ��� ���� ����� �ҽ�
    [SerializeField] private AudioSource dangerAudioSource;

    // ��Ҹ� ����� �ҽ�
    [SerializeField] private AudioSource voiceAudioSource;

    // ȭ�� ��� �Ҹ�
    [SerializeField] private AudioClip arrowShootClip;

    // Ȱ ���� �Ҹ�
    [SerializeField] private AudioClip bowStringClip;

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

    // ���� Ÿ�� 6
    [SerializeField] private AudioClip voiceType6;

    // ============================================

    // ���ٿ�� Ȯ��
    private bool onRebound = false;
    public bool OnRebound { get => onRebound; set => onRebound = value; }

    // ���� ��Ʈ�ѷ�
    private AttackController attackController;

    [SerializeField] private GameObject dangerInfo;

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
            ADelaytime = 6.5f;

            // �� ���� ��ŸƮ
            attackController.TransactionToState(4);

            // ù���� ��
            firstAttack = false;

            return;
        }
        else // ù���� ���Ŀ���
        {
            // ���� ���� ���� ��÷
            ranAttackInt = Random.Range(0, attackController.attackPatternState1.Length);
            
            // ���� Ÿ�Ժ� �ð� �Է�
            if (ranAttackInt == 0) { ADelaytime = 1.2f; } // �⺻ ���� (��)
            else if (ranAttackInt == 1) { ADelaytime = 3f; } // �⺻ ��� ����
            else if (ranAttackInt == 2) { ADelaytime = 5f; } // �޺� ����
            else if (ranAttackInt == 3) { ADelaytime = 5.5f; } // ���� ���� (��)
            else if (ranAttackInt == 4) { ADelaytime = 6.5f; } // Ȱ ����Ʈ ���� (��)
            else if (ranAttackInt == 5) { ADelaytime = 4.8f; } // Ȱ �� ���� (��)
            

            // ���� ��Ʈ�ѷ����� ���� ���� ����
            attackController.TransactionToState(ranAttackInt);
        }
    }

    // ���� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        LookAtTarget(lookOn);
        animator.SetFloat("Time", time);
        time += Time.deltaTime;

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
            objectHealth.Posture += 5;

            if (isReboundAttack)
            {
                // ���ٿ�� ���� ��ȯ
                controller.TransactionToState(7);
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
                // �ȱ� ��� ���
                controller.TransactionToState(1);
                return;
            }
            else // ���� ���� �Ÿ����� ���� ���ٸ�
            {
                // �ȱ� ��� ���
                controller.TransactionToState(1);
                return;
            }
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

    // Į ���� �̺�Ʈ
    public void KatanaEquipEvent()
    {
        handKatana.SetActive(true);
        unEquipKatana.SetActive(false);
        scabBard.SetActive(true);
    }

    // Į ���� �̺�Ʈ
    public void KatanaUnEquipEvent()
    {
        handKatana.SetActive(false);
        unEquipKatana.SetActive(true);
        scabBard.SetActive(false);
    }

    // Ȱ ���� �̺�Ʈ
    public void BowEquipEvent()
    {
        handBow.SetActive(true);
        backBow.SetActive(false);        
    }

    // Ȱ ���� �̺�Ʈ
    public void BowUnEquipEvent()
    {
        handBow.SetActive(false);
        backBow.SetActive(true);
    }

    // ȭ�� �̴� �ִϸ��̼� �̺�Ʈ
    public void ArrowPickup()
    {
        Debug.Log("ȭ�����");
        arrowOnHand.SetActive(true);
        
    }

    // Ȱ ���� ���� �ִϸ��̼� �̺�Ʈ
    public void BowStringEvent()
    {
        arrowAudio.clip = bowStringClip;
        arrowAudio.Play();
    }

    // ȭ�� �߻��ϴ� �ִϸ��̼� �̺�Ʈ
    public void ShootArrow()
    {
        direction = (Target.position - shootPos.position).normalized;
        //direction.y = 0;

        Debug.Log("ȭ��߻�");
        arrowOnHand.SetActive(false);

        arrowAudio.clip = arrowShootClip;
        arrowAudio.Play();

        GameObject arrow = Instantiate(arrowprefab, shootPos.position, Quaternion.LookRotation(direction));
        arrow.GetComponent<ArrowMovement>().direction = direction;
        
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

    public void KatanaSwingSoundEvent()
    {
        katanaAudio.clip = katanaSwingClip;
        katanaAudio.Play();

    }

    // �⺻ ����
    public void AVoiceType1()
    {
        voiceAudioSource.clip = voiceType1;
        voiceAudioSource.Play();
    }

    // �� Ȱ��� ����
    public void AVoiceType2()
    {
        voiceAudioSource.clip = voiceType2;
        voiceAudioSource.Play();
    }

    // ��� ����
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

    // Ư�� ���� 1 (����)
    public void AVoiceType5()
    {
        voiceAudioSource.clip = voiceType5;
        voiceAudioSource.Play();
    }

    // ������ ����
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
