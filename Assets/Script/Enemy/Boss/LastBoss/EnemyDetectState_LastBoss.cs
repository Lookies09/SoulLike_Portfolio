using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectState_LastBoss : EnemyAttackAbleState
{
    // ���� �̵� �ӵ�
    [SerializeField] protected float detectSpeed = 1;

    // ������ ���� (0 = F, 1 = FL, 2 = FR)
    private int direction;

    // �ð�
    private float time = 0;

    // ���� ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        // ���� �ӵ� ����
        //navMeshAgent.speed = detectSpeed;

        // ���� ���� ��÷
        direction = Random.Range(0, 3);

        // ���� �ִϸ��̼� ���
        animator.SetInteger("State", (int)state);
        animator.SetInteger("DetectDirec", 0);


    }

    // ���� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        time += Time.deltaTime;
        animator.SetFloat("Time", time);

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
            // ���� ����� ���� ������ �ȵ����� �״�� �߰�
            else
            {
                // ���� ��� ���� ó��
                //navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(controller.Player.transform.position);
            }


        }

    }

    // ���� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
        time = 0;
    }
}
