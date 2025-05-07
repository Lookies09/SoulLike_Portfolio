using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth_LastBoss : ObjectHealth
{
    private EnemyController controller;

    // ���� ����Ʈ
    [SerializeField] private GameObject guardEffect;
    // ���� ����Ʈ ��ġ
    [SerializeField] private Transform guardEffectPos;

    // ó�� ���� ǥ��
    [SerializeField] private GameObject deathDot;

    // ��Ʈ ������ҽ�
    [SerializeField] private AudioSource hitAudio;

    // ��Ʈ ����
    [SerializeField] private AudioClip hitSound;


    // ���� �� ��Ʈ ����
    private bool canHit;

    public override void Awake()
    {
        base.Awake();
        controller = GetComponent<EnemyController>();
        startHp = health;
    }

    private void Update()
    {
        Excution();

        // �Ȱų� ��ȸ ���¶��
        if (controller.CurrentState == controller.EnemyStates1[7] || controller.CurrentState == controller.EnemyStates1[1])
        {
            if (health >= startHp / 2)
            {
                posture -= 10 * Time.deltaTime;
            }
            else
            {
                posture -= 2 * Time.deltaTime;
            }

        }
    }

    // �÷��̾�� ���� ����
    public override void Hit(int DMG)
    {
        // ���� ���°� �̹� ����� ���¸� �ǰ� ó������ ����
        if (health <= 0) return;

        // ���� ��� ���̶��
        if (controller.CurrentState == controller.EnemyStates1[6])
        {
            // ���� ����Ʈ ���
            Instantiate(guardEffect, guardEffectPos.position, Quaternion.identity);
            animator.SetTrigger("DefenseHit");

            // ü�� ����
            posture += 2;
            return;
        }
        else // ���� �� �ִ� ��Ȳ����
        {
            isHit = true;

            // ü�� ����
            posture += 6;

            // ��Ʈ �Ҹ� ���
            hitAudio.clip = hitSound;
            hitAudio.Play();

            // ���� �����̰� 
            if (controller.CurrentState == controller.EnemyStates1[3])
            {

                // ��Ʈ�� ������ ���¸�
                if (canHit)
                {
                    // ü�°���
                    health -= DMG;

                    // �ǰ� ���·� ��ȯ
                    controller.TransactionToState(4);
                    return;
                }
                else
                {
                    // ü�¸� ����
                    health -= DMG;
                    return;
                }

            }
            else
            {
                // ü�� ���� �ϰ�
                health -= DMG;

                // �ǰ� ���·� ��ȯ
                controller.TransactionToState(4);
            }



        }


    }

    public override void Death()
    {
        // ó�� �ִϸ��̼� ���·� ��ȯ
        health = 0;
        isDead = true;
        controller.TransactionToState(5);
        return;
    }

    public void Excution()
    {
        if (health > 0)
        {
            if (posture >= 100)
            {
                posture = 100;
                deathDot.SetActive(true);
                return;
            }

            deathDot.SetActive(false);
        }
        else
        {
            if (isDead)
            {
                deathDot.SetActive(false);
            }
            else
            {
                deathDot.SetActive(true);
            }

        }
    }


    public void CanHitEvent()
    {
        canHit = true;
    }

    public void CannotHitEvent()
    {
        canHit = false;
    }

    public override void postureBreak()
    {
        // ü���� 100 �̻��̸�
        if (posture >= 100)
        {
            // �ڼ� �������� �λ� ����

        }
    }
}
