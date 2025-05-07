using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState_Bow : EnemyAttackAbleState
{

    // ���� �� ���� Ȯ��
    private bool isOnAttack;

    // �ð�
    private float time;

    // ���� ���� �ð�
    [SerializeField] private float ADelaytime;

    // ��Ҹ� ����� �ҽ�
    [SerializeField] private AudioSource voiceAudio;

    // ���� �Ҹ�
    [SerializeField] private AudioClip attackClip;


    // ���� ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        // ���� �ִϸ��̼� ���
        animator.SetInteger("State", 2);

        // ������ ���� �̵� ����
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0f;
        

    }

    // ���� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        time += Time.deltaTime;
        animator.SetFloat("Time", time);
        LookAtTarget(true);

        // ���� ��ǿ� ���� ADelaytime�ʰ� ������ ������
        if (time < ADelaytime)
        {
            // �ٸ� �ൿ �Ұ�
            isOnAttack = false;
        }
        else
        {
            // �ٸ� �ൿ �Ұ�
            isOnAttack = true;
        }




        // ���� ����� ���� ���� �Ÿ����� �־����ٸ�
        if (controller.GetPlayerDistance() > attackDistance && isOnAttack)
        {
            
            // ��ȸ ��ġ�� ����
            controller.TransactionToState(3);
            return;
          
        }
        // ���� ����� ���� ���� �Ÿ��� �ִٸ�
        else if(controller.GetPlayerDistance() <= attackDistance && isOnAttack)
        {
            // �� ����
            controller.TransactionToState(2);            
        }


    }

    // ���� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
        // �ٸ� �ൿ ����
        isOnAttack = true;
        time = 0;
    }


    public void AttackSoundEvent()
    {
        voiceAudio.clip = attackClip;
        voiceAudio.Play();

    }

}
