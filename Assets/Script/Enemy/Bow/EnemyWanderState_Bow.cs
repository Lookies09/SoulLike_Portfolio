using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWanderState_Bow : EnemyAttackAbleState
{
    // ��ȸ ���� ����
    [SerializeField] protected float radius;

    // ��ȸ ��ġ ���ӿ�����Ʈ ����
    protected Transform targetTransform = null;

    // ��ȸ ��ġ (�⺻ : ���� ��ġ��)
    public Vector3 targetPosition = Vector3.positiveInfinity;

    // ��ȸ�� �̵� �ӵ�
    [SerializeField] protected float moveSpeed;

    // ��ȸ ��ġ���� �Ÿ�
    public float targetDistance = Mathf.Infinity;

    // ��ȸ �̵� �׺���̼� üũ ���� �Ÿ�
    [SerializeField] protected float wanderNavCheckRadius;

    private float time;

    // ��ȸ ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        // ��ȸ �̵� �ӵ��� �׺���̼� ������Ʈ�� ����
        navMeshAgent.speed = moveSpeed;

        // ��ȸ �ִϸ��̼� ���
        animator.SetInteger("State", (int)state);

        // ���ο� ��ȸ ��ġ�� Ž��
        NewRandomDestination();
    }

    // ��ȸ ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {

        // �÷��̾ ���� ���� �Ÿ��ȿ� ������
        if (controller.GetPlayerDistance() <= attackDistance)
        {
            // ���� ���·� ��ȯ
            controller.TransactionToState(2);
            return;
        }

        // ��ȸ�� �̵� ��ġ�� �����Ѵٸ�
        if (targetTransform != null)
        {
            // ��ȸ�� ��ġ ��ó�� �����ߴٸ�
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

    // ���ο� ��ȸ ��ġ�� Ž����
    protected void NewRandomDestination()
    {
        // ��ȸ ��ġ �ε��� ��÷
        int index = Random.Range(0, controller.WanderPoints.Length);

        // ���� ��ȸ ��ġ�� Ž�� �ߴٸ� �ٽ� Ž��
        float distance = Vector3.Distance(controller.WanderPoints[index].position, transform.position);
        if (distance < radius)
        {
            // ��ȸ�� ��ġ�� �ٽ� ��÷��
            NewRandomDestination();
            return;
        }

        // ��ȸ ��ġ�� ����
        targetTransform = controller.WanderPoints[index];

        // ��ȸ ��ġ�� ���������� ���� �������� ������ ��ġ�� �缱��
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += controller.WanderPoints[index].position;
        //randomDirection.y = 0f;

        // ���� ��÷�� ��ȸ ��ġ�� �׺���̼� ������Ʈ �̵� �ӵ��� ����
        targetPosition = randomDirection;

        Debug.Log($"��ȸ �̵��� ��ġ : {targetPosition}");

        // �׺���̼� �̵��� ��ȿ�ϴٸ�
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, wanderNavCheckRadius, 1))
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = moveSpeed;
            navMeshAgent.SetDestination(targetPosition);
        }
    }
}
