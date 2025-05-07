using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefenseState_MidBoss : EnemyAttackAbleState
{
    // �ð�
    private float time;    


    // ��� ���� �ð�
    private float dftime;

    public override void EnterState(int state)
    {

        dftime = Random.Range(1, 1.5f);

        // �� ���� �̵� ����
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0f;

        // ��� ���� �ִϸ��̼� ���
        animator.SetInteger("State", (int)state);


    }

    // ��� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        // ����� �ֽ�
        LookAtTarget(true);

        time += Time.deltaTime;
        animator.SetFloat("Time", time);

        // ���� ����� ���� ���� �Ÿ����� �־����ٸ�
        if (controller.GetPlayerDistance() > attackDistance)
        {
            // ���� ����� �߰� ���ɰŸ����� �־����ٸ�
            if (controller.GetPlayerDistance() > detectDistance)
            {
                // ��ȸ ��ġ�� ����
                controller.TransactionToState(4);
                return;
            }
            // ���� ����� �߰� ���ɰŸ� �ȿ� �ִٸ�
            else
            {                
                // ������ ����
                controller.TransactionToState(1);
            }


        }
        // ���� ����� ���� ���� �Ÿ��� �ִٸ�
        else
        {
            // ���� �ð� ���� ���
            if (time > dftime)
            {                
                // ���� ���·� ��ȯ
                controller.TransactionToState(3);
            }


        }


    }

    // ��� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
        time = 0;
    }
}
