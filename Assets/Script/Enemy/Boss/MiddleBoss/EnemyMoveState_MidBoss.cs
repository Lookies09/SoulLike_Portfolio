using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState_MidBoss : EnemyAttackAbleState
{
    // �̵� �ӵ�
    [SerializeField] protected float moveSpeed;

    // �ð�
    private float time;

    // ���� �޸��� �ð�
    private float wTime;


    // ������ ���� (0 = L, 1 = R)
    private int direction;

    // �̵� ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        wTime = Random.Range(1.5f, 3.3f);
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
        objectHealth.Posture -= 10 * Time.deltaTime;

        // �ð� �帣�� ����
        time += Time.deltaTime;

        // ��� �ٶ�
        LookAtTarget(true);

        // �÷��̾ �������� �Ÿ��� ������
        if (controller.GetPlayerDistance() < detectDistance)
        {
            if (time > wTime) // �����ʰ� �����ٸ�
            {
                // ���� ���·� ��ȯ
                controller.TransactionToState(2);
                return;
            }

            // ���� ���� �ȿ� ���԰� �����Ѵٸ�
            if (controller.GetPlayerDistance() < attackDistance && Input.GetMouseButtonDown(0))
            {
                // ���
                controller.TransactionToState(7);
                return;
            }

            

        }
        // �߰ݰŸ����� ����ٸ�
        else 
        {
            // ��ȯ
            controller.TransactionToState(4);
            
        }
                       

    }

    // ��ȸ ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
        time = 0;
        
    }


}
