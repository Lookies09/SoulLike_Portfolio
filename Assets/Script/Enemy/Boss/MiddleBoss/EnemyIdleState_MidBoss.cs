using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState_MidBoss : EnemyAttackAbleState
{
    // ��� ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {        
        // �׺���̼� ������Ʈ �̵� ����
        navMeshAgent.isStopped = true;

        // ��� �ִϸ��̼� ���
        animator.SetInteger("State", (int)state);
    }

    // ��� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        objectHealth.Posture -= 10 * Time.deltaTime;

        // �÷��̰� ���� ���� �Ÿ��ȿ� ���Դٸ�
        if (controller.GetPlayerDistance() <= attackDistance)
        {
            // ���� ���·� ��ȯ
            controller.TransactionToState(3);
            return;
        }

        // �÷��̾ ���� ���� �Ÿ��ȿ� ���Դٸ�
        if (controller.GetPlayerDistance() <= detectDistance)
        {
            // ������ ���·� ��ȯ
            controller.TransactionToState(1);
            return;
        }

        
    }

    // ��� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
        
    }
}
