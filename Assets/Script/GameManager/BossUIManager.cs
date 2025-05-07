using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUIManager : MonoBehaviour
{
    // �߰����� ����
    [SerializeField] private GameObject midBoss;

    // �������� ����
    [SerializeField] private GameObject lastBoss;

    // ���� ü�� Ui ��ü
    [SerializeField] private GameObject bossHPUI;

    // ���� ���� ü��
    [SerializeField] private Image bossRedHealth;

    // ���� ���� ü�� ���󰡴� ȸ�� ü�¹�
    [SerializeField] private Image bossGrayHealth;

    // ���� ü�� Ui ��ü
    [SerializeField] private GameObject bossPostureUI;

    // ���� ü�� ������ ������, ����
    [SerializeField] private Image boss_postureGauge_R;
    [SerializeField] private Image boss_postureGauge_L;

    // ���� �̸� �ؽ�Ʈ
    [SerializeField] private Text bossNameText;

    // ���� �̸� �׸���
    [SerializeField] private Text bossNameShadow;

    // �λ� �ؽ�Ʈ
    [SerializeField] private GameObject executionText;

    // �߰����� ü���� 1�� ����� ����
    private float hpTo1_M;

    // �������� ü���� 1�� ����� ����
    private float hpTo1_L;

    // �ð�
    private float time;

    // ������ ���� ����
    private bool isBossRoomEnter;

    private void Awake()
    {
        float hp = midBoss.GetComponent<ObjectHealth>().Health;
        hpTo1_M = 1 / hp;

        float hp_L = lastBoss.GetComponent<ObjectHealth>().Health;
        hpTo1_L = 1 / hp_L;
    }

    private void Update()
    {      
        if (GameObject.Find("Samurai_LastBoss") == false) 
        {
            gameObject.GetComponent<Collider>().enabled = false;
            MidBossUI();
        }
        else
        {
            gameObject.GetComponent<Collider>().enabled = true;            
        }

        if (isBossRoomEnter)
        {
            LastBossUI();
        }

        // ���� ü���� �� ���� �� �� ����
        if (bossRedHealth.fillAmount < bossGrayHealth.fillAmount)
        {
            time += Time.deltaTime;

            // 1�� �Ŀ� ȸ�� ü�� ����
            if (time > 1f)
            {
                bossGrayHealth.fillAmount -= 1f * Time.deltaTime;
            }
        }
        else
        {
            time = 0;
        }
    }

    // �߰� ���� UI ó��
    public void MidBossUI()
    {
        // �߰� ������ ������
        if (midBoss == null)
        {
            // ü��, ü�� UI ��Ȱ��ȭ
            bossHPUI.SetActive(false);
            bossPostureUI.SetActive(false);

            // �λ� ǥ�� Ȱ��ȭ
            executionText.GetComponent<ExcutionText>().ExecutionTextEvent();

            return;
        }

        // �÷��̾ ���� �ȴ´ٸ�
        if (midBoss.GetComponent<EnemyController>().CurrentState == midBoss.GetComponent<EnemyController>().EnemyStates1[1])
        {
            // ü�� UI Ȱ��ȭ
            bossHPUI.SetActive(true);

            // �̸� �Է�
            bossNameText.text = "�߰� ����";
            bossNameShadow.text = bossNameText.text;

        }

        // �߰����� ü�� UI ����
        float hp = midBoss.GetComponent<ObjectHealth>().Health;
        bossRedHealth.fillAmount = hp * hpTo1_M;


        // ü�� UI ����
        if (midBoss.GetComponent<ObjectHealth>().Posture > 0)
        {
            bossPostureUI.SetActive(true);
            boss_postureGauge_R.fillAmount = midBoss.GetComponent<ObjectHealth>().Posture * 0.01f;
            boss_postureGauge_L.fillAmount = boss_postureGauge_R.fillAmount;

        }
        else
        {
            bossPostureUI.SetActive(false);
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        isBossRoomEnter = true;
    }

    // ���� ���� UI ó��
    public void LastBossUI()
    {
        // ���� ������ ������
        if (lastBoss == null)
        {
            // ü��, ü�� UI ��Ȱ��ȭ
            bossHPUI.SetActive(false);
            bossPostureUI.SetActive(false);

            // �λ� ǥ�� Ȱ��ȭ
            executionText.GetComponent<ExcutionText>().ExecutionTextEvent();
            return;
        }

        // �÷��̾ ���� Į�� �̴´ٸ�
        if (lastBoss?.GetComponent<EnemyController>()?.CurrentState == lastBoss?.GetComponent<EnemyController>()?.EnemyStates1[0])
        {
            // ü�� UI Ȱ��ȭ
            bossHPUI.SetActive(true);

            // �̸� �Է�
            bossNameText.text = "���� ����";
            bossNameShadow.text = bossNameText.text;
        }

        // �������� ü�� UI ����
        float hp = lastBoss.GetComponent<ObjectHealth>().Health;
        bossRedHealth.fillAmount = hp * hpTo1_L;

        

        // ü�� UI ����
        if (lastBoss.GetComponent<ObjectHealth>().Posture > 0)
        {
            bossPostureUI.SetActive(true);
            boss_postureGauge_R.fillAmount = lastBoss.GetComponent<ObjectHealth>().Posture * 0.01f;
            boss_postureGauge_L.fillAmount = boss_postureGauge_R.fillAmount;

        }
        else
        {
            bossPostureUI.SetActive(false);
        }
    }
}
