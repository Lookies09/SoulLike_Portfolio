using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth_Bow : ObjectHealth
{
     private EnemyController controller;

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
        controller = GetComponent<EnemyController>();
    }
    private void Update()
    {
        Excution();
    }

    // �÷��̾�� ���� ����
    public override void Hit(int DMG)
    {
        // ���� ���°� �̹� ����� ���¸� �ǰ� ó������ ����
        if (health <= 0) return;

        // ü�� ����
        health -= DMG;
        posture += 25;

        // ��Ʈ �Ҹ� ���
        hitAudio.clip = hitSound;
        hitAudio.Play();

        // �ǰ� ���·� ��ȯ
        controller.TransactionToState(4);
    }

    public override void Death()
    {
        // ó�� �ִϸ��̼� ���·� ��ȯ
        health = 0;
        isDead = true;
        controller.TransactionToState(5);
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
        // �ú��� ������ �ʿ� ����
    }
}
