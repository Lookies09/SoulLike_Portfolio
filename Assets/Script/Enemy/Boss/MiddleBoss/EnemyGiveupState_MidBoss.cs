using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGiveupState_MidBoss : EnemyAttackAbleState
{
    // ���� ��ġ ���ӿ�����Ʈ ����
    protected Transform targetTransform = null;

    // ��ȸ ��ġ (�⺻ : ���� ��ġ��)
    public Vector3 targetPosition;

    // ���ͽ� �̵� �ӵ�
    [SerializeField] protected float moveSpeed;

    // ���� ��ġ���� �Ÿ�
    public float targetDistance = Mathf.Infinity;


    // ��ȸ ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        targetTransform = controller.WanderPoints[0];
        targetPosition = targetTransform.position;
        // ��ȸ �̵� �ӵ��� �׺���̼� ������Ʈ�� ����
        navMeshAgent.speed = moveSpeed;

        // ��ȸ �ִϸ��̼� ���
        animator.SetInteger("State", (int)state);

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = moveSpeed;
        navMeshAgent.SetDestination(targetPosition);
    }

    // ��ȸ ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        // �÷��̾ ���� ���� �Ÿ��ȿ� ������
        if (controller.GetPlayerDistance() <= attackDistance)
        {
            // ���� ���·� ��ȯ
            controller.TransactionToState(3);
            return;
        }

        // �÷��̾ ���� ���� �Ÿ��ȿ� ������
        if (controller.GetPlayerDistance() <= detectDistance)
        {
            // ������ ���·� ��ȯ
            controller.TransactionToState(1);
            return;
        }

        // ��ȸ�� �̵� ��ġ�� �����Ѵٸ�
        if (targetTransform != null)
        {
            // ���� ��ġ ��ó�� �����ߴٸ�
            targetDistance = Vector3.Distance(transform.position, targetPosition);
            if (targetDistance < 1f)
            {
                // ��� ���·� ��ȯ
                controller.TransactionToState(0);
            }
        }
    }

    // ��ȸ ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
        // ��ȸ ���� ��ġ ������ �ʱ�ȭ
        targetTransform = null;
        targetPosition = Vector3.positiveInfinity;
        targetDistance = Mathf.Infinity;

        // �׺���̼� �̵� ����
        navMeshAgent.isStopped = true;
    }
}
