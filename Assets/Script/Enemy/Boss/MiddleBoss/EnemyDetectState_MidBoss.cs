using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectState_MidBoss : EnemyAttackAbleState
{
    // ���� �̵� �ӵ�
    [SerializeField] protected float detectSpeed = 1;


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


        // ���� ����� ���� ���ɰŸ� �ȿ� �ְ�
        if (controller.GetPlayerDistance() < detectDistance)
        {
            // ���� ����� ���� ���� �Ÿ������� ���Դٸ�
            if (controller.GetPlayerDistance() < attackDistance)
            {

                // ���� ���� ��ȯ
                controller.TransactionToState(3);
                return;
            }
            else
            {
                // ���� ��� ���� ó��
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(controller.Player.transform.position);
            }
            

        }
        else // ���� ����� ���� ���ɰŸ� ������ �����ٸ�
        {
            // �ʱ� Idle ��ġ�� ������ - GiveUp
            controller.TransactionToState(4);
            return;
        }

    }

    // ���� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {

    }

    
}
