using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    // 플레이어 참조
    private GameObject player;

    // 인 게임 플레이어 체력
    [SerializeField] private Image inGameHealth;

    // 판넬 플레이어 체력
    [SerializeField] private Image panelHealth;

    // 모든 시네마신 카메라
    [SerializeField] private GameObject cinemaCamera;

    // 랜턴 판넬 UI
    [SerializeField] private GameObject panel;

    // 인벤토리 전체 판넬 UI
    [SerializeField] private GameObject inventory_Panel;

    // 인벤토리 내 판넬들 Ui 배열
    [SerializeField] private GameObject[] inven_Panels;

    // root 1 버튼들
    [SerializeField] private Button[] root1_buttons;

    [SerializeField] private InventorySystem inventorySystem;

    private float time;

    //==============================
    // 소지품 창 UI

    // 아이템 이름
    [SerializeField] private Text itemName;

    // 아이템 아이콘
    [SerializeField] private Image itemIcon;

    // 아이템 소지수
    [SerializeField] private Text itemCount;

    // 아이템 설명
    [SerializeField] private Text itemDescript;

    // 장착 아이템 창 UI

    // 전체 장착아이템 창 판넬
    [SerializeField] private GameObject EquipmentItemPanel;

    // 아이템 이름
    [SerializeField] private Text equipItemName;

    // 아이템 아이콘
    [SerializeField] private Image equipItemIcon;

    // 아이템 소지수
    [SerializeField] private Text equipItemCount;

    // 아이템 설명
    [SerializeField] private Text equipItemDescript;


    // 사용 아이템 버튼 번호
    private int useButtonNum;

    // 사용 아이템 버튼들 참조
    [SerializeField] private GameObject[] useButtons;

    public GameObject[] UseButtons { get => useButtons; set => useButtons = value; }
    public int UseButtonNum { get => useButtonNum; set => useButtonNum = value; }


    //==============================

    private void Awake()
    {
        player = GameObject.Find("Player");        
    }

    void Update()
    {
        // 석등 판넬이 비활성화 되어있을때
        if (!panel.activeSelf) 
        {           

            // esc 버튼을 누르면
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // 판넬 체력과 인게임 체력 비율 맞추기
                panelHealth.fillAmount = inGameHealth.fillAmount;

                // 사용 아이템 버튼 업데이트
                useItemButtonUpdate();                

                // 인벤토리 전체 판넬 열고
                inventory_Panel.SetActive(true);

                // 작은 판넬들 다 꺼버리고, 버튼도 초기화 (어차피 버튼과 판넬은 1:1)
                for (int i = 0; i < inven_Panels.Length; i++) 
                {
                    inven_Panels[i].SetActive(false);
                    root1_buttons[i].enabled = true;
                }

                // 첫 판넬만 다시 켜기
                inven_Panels[0].SetActive(true);
                // 첫버튼만 클릭상태로
                root1_buttons[0].Select();
            }
        }

        // 인벤토리 전체 판넬이 켜졌고
        if (inventory_Panel.activeSelf)
        {
            time += Time.deltaTime;

            // 카메라 회전 비활성화
            cinemaCamera.SetActive(false);
            // 커서 활성화
            Cursor.visible = true;
            // 플레이어 모든 액션 비활성화
            player.GetComponent<Character_FightAction>().StopAllAction();

            // UI가 켜지고 0.2초 후에 esc를 누르면
            if (time > 0.2f && Input.GetKeyDown(KeyCode.Escape))
            {
                if (EquipmentItemPanel.activeSelf)
                {
                    // 장착아이템 판넬 꺼질때
                    EquipmentItemPanel.SetActive(false);
                    return;
                }

                // 인벤토리 판넬 끄기
                inventory_Panel.SetActive(false);      
                // 카메라 회전 풀기
                cinemaCamera.SetActive(true);
                // 커서 비활성화
                Cursor.visible = false;
                // 플레이어 모든 액션 활성화
                player.GetComponent<Character_FightAction>().PlayAllAction();

                time = 0f;
            }
        }
    }    


    public void OnEquipmentButtonClick()
    {
        // 작은 판넬들 다 꺼버리고
        for (int i = 0; i < inven_Panels.Length; i++)
        {
            inven_Panels[i].SetActive(false);
        }

        inven_Panels[0].SetActive(true);
    }

    public void OnItemPanelButtonClick()
    {
        // 작은 판넬들 다 꺼버리고
        for (int i = 0; i < inven_Panels.Length; i++)
        {
            inven_Panels[i].SetActive(false);
        }

        inven_Panels[1].SetActive(true);


        // 아이템UI 업데이트
        inventorySystem.UpdateInventoryUI();
    }
    public void OnSettingPanelButtonClick()
    {
        // 작은 판넬들 다 꺼버리고
        for (int i = 0; i < inven_Panels.Length; i++)
        {
            inven_Panels[i].SetActive(false);
        }

        inven_Panels[2].SetActive(true);
    }

    // 아이템 정보 입력
    public void ItemSelected(Item item)
    {
        itemName.text = item.ItemName;
        itemIcon.sprite = item.ItemIcon;
        itemCount.text = item.ItemCount.ToString();
        itemDescript.text = item.Description;

        equipItemName.text = item.ItemName;
        equipItemIcon.sprite = item.ItemIcon;
        equipItemCount.text = item.ItemCount.ToString();
        equipItemDescript.text = item.Description;
    }

    // 사용 아이템 버튼 눌렀을때
    public void OnUseItemButtonClick(int num)
    {
        // 사용 아이템 버튼 번호를 참조 받기
        UseButtonNum = num;

        // 장착아이템 판넬 활성화
        EquipmentItemPanel.SetActive(true);

        // 아이템UI 업데이트
        inventorySystem.UpdateInventoryUI();
    }

    // 장착아이템 판넬 내 아이템 선택했을시
    public void UseItemSelect(Item item)
    {
        // 눌렀던 버튼의 컴포넌트를 참조하여 아이템 정보 넘겨주기
        UseButtons[UseButtonNum].GetComponent<UseItemButton>().Item = item;

        // 판넬 끄기
        EquipmentItemPanel.SetActive(false);

        // 해당 아이콘 업데이트
        UseButtons[UseButtonNum].GetComponent<UseItemButton>().UseItemInfoSet();
    }

    public void useItemButtonUpdate()
    {
        for (int i = 0; i < UseButtons.Length; i++)
        {
            UseButtons[i].GetComponent<UseItemButton>().UseItemInfoSet();
        }
    }

}
