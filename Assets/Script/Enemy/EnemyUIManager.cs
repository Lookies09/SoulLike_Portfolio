using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIManager : MonoBehaviour
{
    // 플레이어 붉은 체력
    [SerializeField] private Image enemyRedHealth;

    // 플레이어 붉은 체력 따라가는 회색 체력바
    [SerializeField] private Image enemyGrayHealth;

    // 체간 UI 전체
    [SerializeField] private GameObject postureBar;

    // 플레이어 체간 게이지 오른쪽, 왼쪽
    [SerializeField] private Image postureGauge_R;
    [SerializeField] private Image postureGauge_L;

    // 플레이어 체력을 1로 만드는 숫자
    private float hpTo1;

    // 시간
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
        // 플레이어 체력 UI 연결
        enemyRedHealth.fillAmount = hp * hpTo1;

        // 붉은 체력이 더 작을 때 만 동작
        if (enemyRedHealth.fillAmount < enemyGrayHealth.fillAmount)
        {
            time += Time.deltaTime;

            // 2초 후에 회색 체력 감소
            if (time > 1f)
            {
                enemyGrayHealth.fillAmount -= 0.8f * Time.deltaTime;
            }
        }
        else
        {
            time = 0;
        }


        
        // 체간 UI 조절
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
