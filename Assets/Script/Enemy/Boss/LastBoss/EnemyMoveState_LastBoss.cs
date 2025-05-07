using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState_LastBoss : EnemyAttackAbleState
{
    // ���⼭ ������ �޸��� �͵� �Ű� ����� ��


    // �̵� �ӵ�
    [SerializeField] protected float moveSpeed;

    // �ð�
    private float time;

    // ���� �ȱ� �ð�
    private float wTime;


    // ������ ���� (0 = L, 1 = R)
    private int direction;

    // �̵� ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        // �ð� ���� ��÷
        wTime = Random.Range(1f, 2f);
        // ���� ���� ��÷
        direction = Random.Range(0, 2);

        // �̵� �ӵ��� �׺���̼� ������Ʈ�� ����
        navMeshAgent.speed = moveSpeed;

        // �̵� �ִϸ��̼� ���
        animator.SetInteger("State", (int)state);
        animator.SetInteger("MoveDirec", direction);

    }

    // �̵� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        // �ð� �帣�� ����
        time += Time.deltaTime;
                
        animator.SetFloat("Time", time);

        // ��� �ٶ�
        LookAtTarget(true);

        // �÷��̾ �������� �Ÿ��� ������
        if (controller.GetPlayerDistance() < detectDistance)
        {
            // ���ݰ��� �Ÿ��� ������
            if (controller.GetPlayerDistance() < attackDistance)
            {                
                // �÷��̾ ���� �Ѵٸ�
                if (Input.GetMouseButtonDown(0))
                {
                    controller.TransactionToState(6);
                    return;
                }
                

            }

            if (time > wTime) // �����ʰ� �����ٸ�
            {
                // ���� ���·� ��ȯ
                controller.TransactionToState(2);

                return;

            }

        }        


    }

    // ��ȸ ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
        time = 0;        
    }
}
