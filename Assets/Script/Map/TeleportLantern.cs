using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportLantern : MonoBehaviour
{
    [SerializeField] private GameObject lightEffect;

    // UI 위치
    [SerializeField] private Transform uiPos;

    // 석등 상호작용 Ui 이미지
    [SerializeField] private Image lantern_UI;

    // 석등 상호작용 Ui 텍스트
    [SerializeField] private Text lantern_Text;

    // 상호작용 UI 전체
    [SerializeField] private GameObject info_UI_all;

    // 캔버스 렉트 트렌스폼
    [SerializeField] private RectTransform canvasRectTransform;

    // 판넬 UI
    [SerializeField] private GameObject panel;

    // 플레이어 참조
    private GameObject player;

    // 플레이어 접촉 여부
    private bool isContact;

    // 석등 오디오 소스들
    [SerializeField] private AudioSource[] audioSources;

    // 모든 시네마신 카메라
    [SerializeField] private GameObject cinemaCamera;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (isContact)
        {
            lightEffect.SetActive(true);
            lantern_Text.text = "석등에 접촉한다";


            info_UI_all.SetActive(true);

            Vector3 targetPosition = Camera.main.WorldToViewportPoint(uiPos.position);

            // 화면 좌표를 Canvas 영역으로 변환
            Vector2 canvasPosition = new Vector2(
                ((targetPosition.x * canvasRectTransform.sizeDelta.x) - (canvasRectTransform.sizeDelta.x * 0.5f)),
                ((targetPosition.y * canvasRectTransform.sizeDelta.y) - (canvasRectTransform.sizeDelta.y * 0.5f)));

            lantern_UI.rectTransform.anchoredPosition = canvasPosition;

            // 석등과 접촉하고 E를 눌렀다면.
            if (Input.GetKeyDown(KeyCode.E))
            {
                info_UI_all.SetActive(false);

                for (int i = 0; i < audioSources.Length; i++)
                {
                    audioSources[i].Play();
                }               

                // 판넬 활성화
                panel.SetActive(true);                

                isContact = false;
            }
        }

        if (panel.activeSelf)
        {
            // 카메라 회전 고정
            cinemaCamera.SetActive(false);

            // 플레이어 모든 액션 정지
            player.GetComponent<Character_FightAction>().StopAllAction();

            // 커서 활성화
            Cursor.visible = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {            
            isContact = true;            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {            
            isContact = false;

            info_UI_all.SetActive(false);
            lightEffect.SetActive(false);
        }
    }
}
