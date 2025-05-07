using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ObjectUIManager : MonoBehaviour
{
    // 플레이어 참조
    private GameObject player;

    // 플레이어 초기 체력
    private int preHealth;

    // 랜턴 판넬 UI
    [SerializeField] private GameObject panel;

    // 석등 건너기 판넬 UI
    [SerializeField] private GameObject teleportPanel;

    // 인 게임 플레이어 체력
    [SerializeField] private Image inGameHealth;

    // 판넬 플레이어 체력
    [SerializeField] private Image panelHealth;

    // 장소 이미지
    [SerializeField] private Image teleportPosImg;

    // 장소 이미지 배열
    [SerializeField] private Sprite[] teleportPosImgs;

    // 석등 건너기 중요한 장소 버튼
    [SerializeField] private Button importantPosButton;

    // 석등 건너기 자세한 장소 버튼 배열
    [SerializeField] private Button[] detailPosButton;

    // 모든 시네마신 카메라
    [SerializeField] private GameObject cinemaCamera;

    

    private void Awake()
    {
        player = GameObject.Find("Player");

        preHealth = player.GetComponent<PlayerHealth>().Health;
    }

    // Update is called once per frame
    void Update()
    {
        // 판넬 체력과 인게임 체력 비율 맞추기
        panelHealth.fillAmount = inGameHealth.fillAmount;

        

        // 석등 판넬이 활성화 되었고
        if (panel.activeSelf)
        {
            // 모든 판넬 한번에 닫기
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // 석등건너기 판넬 비활성화
                teleportPanel.SetActive(false);
                // 카메라 회전 풀기
                cinemaCamera.SetActive(true);
                // 판넬 비활성화
                panel.SetActive(false);
                // 커서 비활성화
                Cursor.visible = false;
                // 플레이어 모든 액션 활성화
                player.GetComponent<Character_FightAction>().PlayAllAction();
            }


            // 석등건너기 판넬이 활성화되었고
            if (teleportPanel.activeSelf)
            {
                // 중요한 장소 버튼이 활성화 되었다면
                if (importantPosButton.interactable == true)
                {
                    // 마우스 우클릭시
                    if (Input.GetMouseButtonDown(1))
                    {
                        // 석등건너기 판넬 비활성화
                        teleportPanel.SetActive(false);
                    }
                }
                // 중요한 장소 버튼이 비활성화 되었다면
                else
                {
                    // 마우스 우클릭시
                    if (Input.GetMouseButtonDown(1))
                    {
                        // 중요한 장소 버튼 활성화 하고 세부위치 비활성화
                        TeleportPanelButtonctrl(true);
                    }
                }
            }
            // 석등건너기 판넬 비활성화되었다면(석등 판넬만 있다면)
            else
            {
                // 마우스 우클릭시
                if (Input.GetMouseButtonDown(1))
                {
                    // 판넬 닫기
                    panel.SetActive(false);
                    // 카메라 회전 풀기
                    cinemaCamera.SetActive(true);
                    // 커서 비활성화
                    Cursor.visible = false;
                    // 플레이어 모든 액션 활성화
                    player.GetComponent<Character_FightAction>().PlayAllAction();
                }
            }
        }       

    }

    // 휴식하기 버튼 클릭
    public void OnRestButtonClick()
    {
        player.GetComponent<PlayerHealth>().Health = preHealth;
    }

    // 석등 건너기 버튼 클릭
    public void OnTeleportButtonClick()
    {
        Debug.Log("버튼 누름");
        teleportPanel.SetActive(true);

        // 중요한 장소 버튼 활성화 하고 세부위치 비활성화
        TeleportPanelButtonctrl(true);

    }

    // 세부 위치 버튼에 커서 올렸을시
    public void OnDetailPosButtonTriggerEnter(int num)
    {
        teleportPosImg.sprite = teleportPosImgs[num];
    }

    // 세부 위치 버튼 클릭
    public void OnDetailPosButtonClick(int num)
    {
        SceneManager.LoadScene(num);
    }

    // 중요한 장소 버튼 클릭
    public void OnImportantPosButtonClick()
    {
        // 중요한 장소 버튼 비활성화 하고 세부위치 활성화
        TeleportPanelButtonctrl(false);
    }

    // 석등건너기 판넬 버튼 컨트롤
    public void TeleportPanelButtonctrl(bool active)
    {
        // 중요한 장소 버튼 active
        importantPosButton.interactable = active;

        // 세부 위치 버튼 !active
        for (int i = 0; i < detailPosButton.Length; i++)
        {
            detailPosButton[i].interactable = !active;            
        }
    }

}
