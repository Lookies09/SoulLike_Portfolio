using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefenseState_LastBoss : EnemyAttackAbleState
{
    // �ð�
    private float time;


    // ��� ���� �ð�
    private float dftime;

    public override void EnterState(int state)
    {

        dftime = 1f;

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
            // ���� ����� �߰� ���ɰŸ� �ȿ��ִٸ�
            if (controller.GetPlayerDistance() < detectDistance)
            {                
                controller.TransactionToState(2);
                return;
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
