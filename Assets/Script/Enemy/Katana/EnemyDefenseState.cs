using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefenseState : EnemyAttackAbleState
{

    private float time;


    public override void EnterState(int state)
    {

        // �� ���� �̵� ����
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0f;

        // ��� ���� �ִϸ��̼� ���
        animator.SetInteger("State", (int)state);
    }

    // ��� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        time += Time.deltaTime;

        LookAtTarget(true);

        // �߰� ���ɰŸ� �ȿ� �ְ�
        if (controller.GetPlayerDistance() < detectDistance)
        {
            // ���� ���� �Ÿ��ȿ� ������
            if (controller.GetPlayerDistance() < attackDistance)
            {
                // �ð��� 0.8�� �����ٸ�
                if (time > 0.8f)
                {
                    // ���� ���·� ��ȯ��
                    controller.TransactionToState(3);
                    return;
                }                
            }
            // ���� �Ÿ� �ۿ� �ִٸ�
            else
            {
                // �߰� ���·� ��ȯ��
                controller.TransactionToState(2);
                return;
            }

        }
        // �߰� ���ɰŸ� �ۿ� �ִٸ�
        else
        {
            // ����(��ȸ ��ġ)�� ������
            controller.TransactionToState(4);
            return;
        }




    }

    // ��� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
        time = 0;
    }


}
