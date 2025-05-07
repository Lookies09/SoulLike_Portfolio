using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState_Bow : EnemyAttackAbleState
{
    // �ǰ� ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        Debug.Log("��Ʈ ���� On");
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
            Debug.Log("���ݻ��� ����");
            // ��� ���·� ��ȯ
            controller.TransactionToState(0);
            return;
        }
        // �°� �ִ� ���°� �ƴϰ� ���ݰŸ����� ����ٸ�
        else
        {
            Debug.Log("��ȸ���� ����");
            // ����(��ȸ) ���·� ��ȯ
            controller.TransactionToState(3);
            return;
        }
    }

    // �ǰ� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
        IsHit = false;
    }
}
