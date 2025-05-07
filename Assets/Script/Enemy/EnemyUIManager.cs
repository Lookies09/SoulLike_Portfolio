using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIManager : MonoBehaviour
{
    // �÷��̾� ���� ü��
    [SerializeField] private Image enemyRedHealth;

    // �÷��̾� ���� ü�� ���󰡴� ȸ�� ü�¹�
    [SerializeField] private Image enemyGrayHealth;

    // ü�� UI ��ü
    [SerializeField] private GameObject postureBar;

    // �÷��̾� ü�� ������ ������, ����
    [SerializeField] private Image postureGauge_R;
    [SerializeField] private Image postureGauge_L;

    // �÷��̾� ü���� 1�� ����� ����
    private float hpTo1;

    // �ð�
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        float hp = gameObject.GetComponent<ObjectHealth>().Health;
        hpTo1 = 1 / hp;
    }

    // Update is called once per frame
    void Update()
    {
        float hp = gameObject.GetComponent<ObjectHealth>().Health;
        // �÷��̾� ü�� UI ����
        enemyRedHealth.fillAmount = hp * hpTo1;

        // ���� ü���� �� ���� �� �� ����
        if (enemyRedHealth.fillAmount < enemyGrayHealth.fillAmount)
        {
            time += Time.deltaTime;

            // 2�� �Ŀ� ȸ�� ü�� ����
            if (time > 1f)
            {
                enemyGrayHealth.fillAmount -= 0.8f * Time.deltaTime;
            }
        }
        else
        {
            time = 0;
        }


        
        // ü�� UI ����
        if (gameObject.GetComponent<ObjectHealth>().Posture > 0)
        {
            postureBar.SetActive(true);
            postureGauge_R.fillAmount = gameObject.GetComponent<ObjectHealth>().Posture * 0.01f;
            postureGauge_L.fillAmount = postureGauge_R.fillAmount;

        }
        else
        {
            postureBar.SetActive(false);
        }
        

    }
}
