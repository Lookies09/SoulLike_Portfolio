using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReboundState_MidBoss : EnemyAttackAbleState
{
    // �ð�
    private float time = 0;

    // ���ٿ�� ���� �ð�
    [SerializeField] private float reboundTime;

    public override void EnterState(int state)
    {
        animator.SetInteger("State", state);
        Debug.Log("�и�");
    }


    public override void UpdateState()
    {
        time += Time.deltaTime;

        // ����� ���� ���� �Ÿ� �ȿ� �����鼭 ���� �ð��� ������ ��
        if (controller.GetPlayerDistance() < detectDistance && time > reboundTime)
        {
            // ���� ���� �Ÿ� �ȿ� ������
            if (controller.GetPlayerDistance() < attackDistance)
            {
                // ���� ��� ���
                controller.TransactionToState(3);
            }
            else // ���� ���� �Ÿ����� ���� ���ٸ�
            {
                // ���� ��� ���
                controller.TransactionToState(2);
            }
        }
        else if (controller.GetPlayerDistance() >= detectDistance) // ���� ���� �Ÿ����� ����ٸ�
        {
            // ��ȯ
            controller.TransactionToState(4);            
        }
    }


    public override void ExitState()
    {

        time = 0;
    }
}
