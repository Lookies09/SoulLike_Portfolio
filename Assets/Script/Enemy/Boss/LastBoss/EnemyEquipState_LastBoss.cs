using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEquipState_LastBoss : EnemyAttackAbleState
{
    // ��Ҹ� ������ҽ�
    [SerializeField] private AudioSource voiceAudio;

    // Į�Ҹ� ������ҽ�
    [SerializeField] private AudioSource katanaAudio;

    // ���� ��Ҹ� ����
    [SerializeField] private AudioClip laughSound;

    // Į�̴� ����
    [SerializeField] private AudioClip katanaDrawSound;

    private float time = 0;

    // �޼� Į�� ��� �κ� Į��
    [SerializeField] private GameObject L_Hand_KatanaZip;

    // �޼� Į�� ��� �κ� Į
    [SerializeField] private GameObject L_Hand_Katana;

    // ������ �̴� Į
    [SerializeField] private GameObject R_Hand_Katana;

    // ������ ��¥ Į
    [SerializeField] private GameObject R_Hand_Real_Katana;

    // �㸮�㿡 �̴� Į
    [SerializeField] private GameObject Spine_Equip_Katana;

    // �㸮�㿡 ���̴� Į
    [SerializeField] private GameObject Spine_Look_Katana;


    // �ߵ� ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        // �׺���̼� ������Ʈ �̵� ����
        navMeshAgent.isStopped = true;

        // �ߵ� �ִϸ��̼� ���
        animator.SetInteger("State", (int)state);
    }

    // ��� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        time += Time.deltaTime;

        // �÷��̰� ���� ���� �Ÿ��ȿ� ���Դٸ�
        if (controller.GetPlayerDistance() <= attackDistance)
        {
            // ���� ���·� ��ȯ
            controller.TransactionToState(3);
            return;
        }

        // �÷��̾ ���� ���� �Ÿ��ȿ� ���Դٸ�
        if (controller.GetPlayerDistance() <= detectDistance && time >= 3f)
        {
            // ������ ���·� ��ȯ
            controller.TransactionToState(1);
            return;
        }


    }

    // ��� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
        time = 0;
    }

    // �޼��� Į���� �տø��� �̺�Ʈ
    public void HandOnKatanaEvent()
    {
        // ó�� �㸮�� �ִ� Į ����
        Spine_Equip_Katana.SetActive(false);

        // Į�� ��� Į, Į�� Ȱ��ȭ
        L_Hand_Katana.SetActive(true);
        L_Hand_KatanaZip.SetActive(true);

    }

    // �������� Į�� ��� �̺�Ʈ
    public void GrabKatanaEvent()
    {
        // �޼� Į���� �ִ� Į ����
        L_Hand_Katana.SetActive(false);

        // ������ Į Ȱ��ȭ
        R_Hand_Katana.SetActive(true);

    }

    // Į���� �� ���� �̺�Ʈ
    public void EquipKatanaEvent()
    {
        // �޼� Į�� ����
        L_Hand_KatanaZip.SetActive(false);

        // �㸮�㿡 ���̴� Į Ȱ��ȭ
        Spine_Look_Katana.SetActive(true);

        // ������ �̴�Į ��Ȱ��ȭ
        R_Hand_Katana.SetActive(false);

        // ������ ��¥ Į Ȱ��ȭ
        R_Hand_Real_Katana.SetActive(true);
    }

    public void LaughSoundEvent()
    {
        voiceAudio.clip = laughSound;
        voiceAudio.Play();

    }

    public void KatanaDrawSoundEvent()
    {
        katanaAudio.clip = katanaDrawSound;
        katanaAudio.Play();

    }
}
