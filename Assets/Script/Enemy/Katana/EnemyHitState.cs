using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState : EnemyAttackAbleState
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
       
        // �°� �ִ� ���°� �ƴϰ� ���ݰ����� ���¸�
        if (controller.GetPlayerDistance() <= attackDistance)
        {
            // ��� ���·� ��ȯ
            controller.TransactionToState(7);
            return;
        }
        //  ���ݰ��� �Ÿ����� ������� ���������� �Ÿ���
        else if (controller.GetPlayerDistance() <= detectDistance)
        {
            // �������·� ��ȯ
            controller.TransactionToState(2);
            return;
        }
        else // �� �ƴ϶�� 
        {
            //����(��ȸ)
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
