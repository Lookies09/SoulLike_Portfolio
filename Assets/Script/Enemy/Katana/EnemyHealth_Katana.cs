using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth_Katana : ObjectHealth
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

    // ü�� UI
    [SerializeField] private GameObject healthUI;

    public override void Awake()
    {
        base.Awake();
        controller = GetComponent<EnemyFSMController>();
        startHp = health;
    }

    private void Update()
    {
        Excution();

        // �Ȱų� ��ȸ ���¶��
        if (controller.CurrentState == controller.EnemyStates1[0] || controller.CurrentState == controller.EnemyStates1[1]) 
        {
            if(health >= startHp /2)
            {
                posture -= 8 * Time.deltaTime;
            }
            else
            {
                posture -= 1 * Time.deltaTime;
            }
            
        }

    }

    // �÷��̾�� ���� ����
    public override void Hit(int DMG)
    {
        // ���� ���°� �̹� ����� ���¸� �ǰ� ó������ ����
        if (health <= 0) return;

        // ���� ��� ���̶��
        if (controller.CurrentState == controller.EnemyStates1[2] || controller.CurrentState == controller.EnemyStates1[7])
        {
            // ü�� ����
            posture += 5;

            // ���� ����Ʈ ���
            Instantiate(guardEffect, guardEffectPos.position, Quaternion.identity);
            animator.SetTrigger("DefenseHit");
            return;
        }
        else // ���� �� �ִ� ��Ȳ�̸�
        {
            // ü�� ����
            health -= DMG;

            // ü�� ����
            posture += 20;

            // ��Ʈ �Ҹ� ���
            hitAudio.clip = hitSound;
            hitAudio.Play();

            IsHit = true;
            // �ǰ� ���·� ��ȯ
            controller.TransactionToState(5);

        }

        
    }

    public override void Death()
    {
        // ó�� �ִϸ��̼� ���·� ��ȯ
        health = 0;
        isDead = true;
        controller.TransactionToState(6);
        healthUI.SetActive(false);
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

    public override void postureBreak()
    {
        if (posture >= 100)
        {
            // �ڼ� �������� �λ� ����

        }
    }

}
