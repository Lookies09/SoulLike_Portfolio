using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyDetectState : EnemyAttackAbleState
{
    // ���� �̵� �ӵ�
    [SerializeField] protected float detectSpeed;

    // ���� ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        // ���� �ִϸ��̼� ���
        animator.SetInteger("State", (int)state);

        // ���� ��� ���� ó��
        navMeshAgent.isStopped = false;
        navMeshAgent.updateRotation = false;

        // ���� �ӵ� ����
        navMeshAgent.speed = detectSpeed;

    }

    // ���� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {   
        
        navMeshAgent.SetDestination(controller.Player.transform.position);
        LookAtTarget(true);

        // �߰� ���ɰŸ� �ȿ� �ְ�
        if (controller.GetPlayerDistance() < detectDistance)
        {
            // ���� ���� �Ÿ��ȿ� ������
            if (controller.GetPlayerDistance() < attackDistance)
            {
                // ��� ���·� ��ȯ��
                controller.TransactionToState(7);
                return;
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

    // ���� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
        
    }

    
}
