using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    // 플레이어 붉은 체력
    [SerializeField] private Image playerRedHealth;

    // 플레이어 붉은 체력 따라가는 회색 체력바
    [SerializeField] private Image playerGrayHealth;

    // 체간 UI 전체
    [SerializeField] private GameObject postureBar;

    // 체간 뒷 배경
    [SerializeField] private Image postureBackGround;

    // 플레이어 체간 게이지 오른쪽, 왼쪽
    [SerializeField] private Image postureGauge_R;
    [SerializeField] private Image postureGauge_L;

    // 플레이어 참조
    [SerializeField] private GameObject Player;

    // 사망 알림 텍스트
    [SerializeField] private GameObject deadText;

    // 타겟 락온 이미지
    [SerializeField] private Image targetFocusImg;

    // 캔버스 렉트 트렌스폼
    [SerializeField] private RectTransform canvasRectTransform;

    // 인게임 사용 아이템 이미지
    [SerializeField] private Image inGameUseItem;

    // 인게임 사용 아이템 빈이미지
    [SerializeField] private Sprite nullItemIMG;

    // 인게임 사용 아이템 카운트
    [SerializeField] private Text useItemCount;

    private int useButtonNum = 0;

    //============================================

    // 인벤토리 UI 메니저 참조
    [SerializeField] private InventoryUIManager inventoryUIManager;

    // ===========================================
    // 플레이어 체력을 1로 만드는 숫자
    private float hpTo1;


    // 시간
    private float time;

    // 사망 시간
    private float deadTime;

    private void Awake()
    {
        float hp = Player.GetComponent<PlayerHealth>().Health;
        hpTo1 = 1 / hp;

        // 커서 보이는거 비활성화
        Cursor.visible = false;

    }


    private void Update()
    {

        if (Player.GetComponent<TargetFocus_Movement>().enabled == false)
        {
            targetFocusImg.enabled = false;            
        }
        else
        {
            targetFocusImg.enabled = true;

            GameObject target = Player.GetComponent<TargetFocus_Movement>().target;

            Vector3 targetPosition = Camera.main.WorldToViewportPoint(target.GetComponent<EnemyController>().TargetDotPos.position);

            // 화면 좌표를 Canvas 영역으로 변환
            Vector2 canvasPosition = new Vector2(
                ((targetPosition.x * canvasRectTransform.sizeDelta.x) - (canvasRectTransform.sizeDelta.x * 0.5f)),
                ((targetPosition.y * canvasRectTransform.sizeDelta.y) - (canvasRectTransform.sizeDelta.y * 0.5f)));


            targetFocusImg.rectTransform.anchoredPosition = canvasPosition;
        }


        float hp = Player.GetComponent<PlayerHealth>().Health;
        // 플레이어 체력 UI 연결
        playerRedHealth.fillAmount = hp * hpTo1;

        // 붉은 체력이 더 작을 때 만 동작
        if (playerRedHealth.fillAmount < playerGrayHealth.fillAmount)
        {
            time += Time.deltaTime;

            // 2초 후에 회색 체력 감소
            if (time > 1f)
            {
                playerGrayHealth.fillAmount -= 0.8f * Time.deltaTime;
            }            
        }
        else
        {
            time = 0;
        }

        // 체간 UI 조절
        if (Player.GetComponent<PlayerHealth>().Posture > 0)
        {
            postureBar.SetActive(true);
            postureGauge_R.fillAmount = Player.GetComponent<PlayerHealth>().Posture * 0.01f;
            postureGauge_L.fillAmount = postureGauge_R.fillAmount;

            if (Player.GetComponent<PlayerHealth>().Posture < 50)
            {
                postureBackGround.color = new Color32(0, 0, 0, 200);
            }
            else
            {
                postureBackGround.color = new Color32(178, 0, 0, 200);
            }

        }
        else
        {
            postureBar.SetActive(false);
        }


        if (Player.GetComponent<PlayerHealth>().Health <= 0)
        {
            deadTime += Time.deltaTime;
            if (deadTime > 1.5f)
            {
                deadText.SetActive(true);
            }            
        }
        else
        {
            deadTime = 0;
            deadText.SetActive(false);
        }

        ItemSwap();

        UseItem(inventoryUIManager.UseButtons[useButtonNum].GetComponent<UseItemButton>().Item);
    }


    // 인게임 아이템 변경
    public void ItemSwap()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            useButtonNum++;
            if (useButtonNum > 4)
            {
                useButtonNum = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            useButtonNum--;
            if (useButtonNum < 0)
            {
                useButtonNum = 4;
            }
        }

        if (inventoryUIManager.UseButtons[useButtonNum].GetComponent<UseItemButton>().Item == null)
        {
            inGameUseItem.sprite = nullItemIMG;
            useItemCount.text = "";
            return;
        }
        inGameUseItem.sprite = inventoryUIManager.UseButtons[useButtonNum].GetComponent<UseItemButton>().Item.ItemIcon;
        useItemCount.text = inventoryUIManager.UseButtons[useButtonNum].GetComponent<UseItemButton>().Item.ItemCount.ToString();
    }

    public void UseItem(Item item)
    {
        // R키를 누르면
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (item == null)
            {
                Debug.Log("아이템이 비어있습니다");
                return;
            }

            if (item.ItemCount > 0)
            {
                item.Use();
                Debug.Log("아이템 사용");
            }            
        }
    }
}
