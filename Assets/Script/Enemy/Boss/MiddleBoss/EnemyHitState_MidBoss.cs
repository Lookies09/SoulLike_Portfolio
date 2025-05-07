using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState_MidBoss : EnemyAttackAbleState
{
    // �ǰ� ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        // �̵� ����
        navMeshAgent.isStopped = true;

        // �ǰ� �ִϸ��̼� ���
        IsHit = true;
        animator.SetInteger("State", (int)state);
    }

    // �ǰ� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {

        // �Ѵ� ������ ����
        if (IsHit) return;

        // ���� ���� �Ÿ� �ȿ� �ְ�
        if (controller.GetPlayerDistance() < detectDistance)
        {
            // ���� ���� �Ÿ� �ȿ� �ִٸ�
            if (controller.GetPlayerDistance() < attackDistance)
            {
                // ��� ���·� ��ȯ
                controller.TransactionToState(7);
                return;
            }
            else // ���� ���� �Ÿ� �ۿ� �ִٸ�
            {
                // ������ ���·� ��ȯ
                controller.TransactionToState(1);
                return;
            }

        }
        else // ���� ���� �Ÿ� �ۿ� �����ٸ�
        {
            //����
            controller.TransactionToState(4);
            return;
        }


    }

    // �ǰ� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
        IsHit = false;
    }
}
