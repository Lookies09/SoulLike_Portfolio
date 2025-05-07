using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUIManager : MonoBehaviour
{
    // 중간보스 참조
    [SerializeField] private GameObject midBoss;

    // 최종보스 참조
    [SerializeField] private GameObject lastBoss;

    // 보스 체력 Ui 전체
    [SerializeField] private GameObject bossHPUI;

    // 보스 붉은 체력
    [SerializeField] private Image bossRedHealth;

    // 보스 붉은 체력 따라가는 회색 체력바
    [SerializeField] private Image bossGrayHealth;

    // 보스 체간 Ui 전체
    [SerializeField] private GameObject bossPostureUI;

    // 보스 체간 게이지 오른쪽, 왼쪽
    [SerializeField] private Image boss_postureGauge_R;
    [SerializeField] private Image boss_postureGauge_L;

    // 보스 이름 텍스트
    [SerializeField] private Text bossNameText;

    // 보스 이름 그림자
    [SerializeField] private Text bossNameShadow;

    // 인살 텍스트
    [SerializeField] private GameObject executionText;

    // 중간보스 체력을 1로 만드는 숫자
    private float hpTo1_M;

    // 최종보스 체력을 1로 만드는 숫자
    private float hpTo1_L;

    // 시간
    private float time;

    // 보스맵 진입 여부
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

        // 붉은 체력이 더 작을 때 만 동작
        if (bossRedHealth.fillAmount < bossGrayHealth.fillAmount)
        {
            time += Time.deltaTime;

            // 1초 후에 회색 체력 감소
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

    // 중간 보스 UI 처리
    public void MidBossUI()
    {
        // 중간 보스가 죽으면
        if (midBoss == null)
        {
            // 체력, 체간 UI 비활성화
            bossHPUI.SetActive(false);
            bossPostureUI.SetActive(false);

            // 인살 표시 활성화
            executionText.GetComponent<ExcutionText>().ExecutionTextEvent();

            return;
        }

        // 플레이어를 보고 걷는다면
        if (midBoss.GetComponent<EnemyController>().CurrentState == midBoss.GetComponent<EnemyController>().EnemyStates1[1])
        {
            // 체력 UI 활성화
            bossHPUI.SetActive(true);

            // 이름 입력
            bossNameText.text = "중간 보스";
            bossNameShadow.text = bossNameText.text;

        }

        // 중간보스 체력 UI 연결
        float hp = midBoss.GetComponent<ObjectHealth>().Health;
        bossRedHealth.fillAmount = hp * hpTo1_M;


        // 체간 UI 조절
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

    // 최종 보스 UI 처리
    public void LastBossUI()
    {
        // 최종 보스가 죽으면
        if (lastBoss == null)
        {
            // 체력, 체간 UI 비활성화
            bossHPUI.SetActive(false);
            bossPostureUI.SetActive(false);

            // 인살 표시 활성화
            executionText.GetComponent<ExcutionText>().ExecutionTextEvent();
            return;
        }

        // 플레이어를 보고 칼을 뽑는다면
        if (lastBoss?.GetComponent<EnemyController>()?.CurrentState == lastBoss?.GetComponent<EnemyController>()?.EnemyStates1[0])
        {
            // 체력 UI 활성화
            bossHPUI.SetActive(true);

            // 이름 입력
            bossNameText.text = "최종 보스";
            bossNameShadow.text = bossNameText.text;
        }

        // 최종보스 체력 UI 연결
        float hp = lastBoss.GetComponent<ObjectHealth>().Health;
        bossRedHealth.fillAmount = hp * hpTo1_L;

        

        // 체간 UI 조절
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
