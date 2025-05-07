using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    // �÷��̾� ����
    private GameObject player;

    // �� ���� �÷��̾� ü��
    [SerializeField] private Image inGameHealth;

    // �ǳ� �÷��̾� ü��
    [SerializeField] private Image panelHealth;

    // ��� �ó׸��� ī�޶�
    [SerializeField] private GameObject cinemaCamera;

    // ���� �ǳ� UI
    [SerializeField] private GameObject panel;

    // �κ��丮 ��ü �ǳ� UI
    [SerializeField] private GameObject inventory_Panel;

    // �κ��丮 �� �ǳڵ� Ui �迭
    [SerializeField] private GameObject[] inven_Panels;

    // root 1 ��ư��
    [SerializeField] private Button[] root1_buttons;

    [SerializeField] private InventorySystem inventorySystem;

    private float time;

    //==============================
    // ����ǰ â UI

    // ������ �̸�
    [SerializeField] private Text itemName;

    // ������ ������
    [SerializeField] private Image itemIcon;

    // ������ ������
    [SerializeField] private Text itemCount;

    // ������ ����
    [SerializeField] private Text itemDescript;

    // ���� ������ â UI

    // ��ü ���������� â �ǳ�
    [SerializeField] private GameObject EquipmentItemPanel;

    // ������ �̸�
    [SerializeField] private Text equipItemName;

    // ������ ������
    [SerializeField] private Image equipItemIcon;

    // ������ ������
    [SerializeField] private Text equipItemCount;

    // ������ ����
    [SerializeField] private Text equipItemDescript;


    // ��� ������ ��ư ��ȣ
    private int useButtonNum;

    // ��� ������ ��ư�� ����
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
        // ���� �ǳ��� ��Ȱ��ȭ �Ǿ�������
        if (!panel.activeSelf) 
        {           

            // esc ��ư�� ������
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // �ǳ� ü�°� �ΰ��� ü�� ���� ���߱�
                panelHealth.fillAmount = inGameHealth.fillAmount;

                // ��� ������ ��ư ������Ʈ
                useItemButtonUpdate();                

                // �κ��丮 ��ü �ǳ� ����
                inventory_Panel.SetActive(true);

                // ���� �ǳڵ� �� ��������, ��ư�� �ʱ�ȭ (������ ��ư�� �ǳ��� 1:1)
                for (int i = 0; i < inven_Panels.Length; i++) 
                {
                    inven_Panels[i].SetActive(false);
                    root1_buttons[i].enabled = true;
                }

                // ù �ǳڸ� �ٽ� �ѱ�
                inven_Panels[0].SetActive(true);
                // ù��ư�� Ŭ�����·�
                root1_buttons[0].Select();
            }
        }

        // �κ��丮 ��ü �ǳ��� ������
        if (inventory_Panel.activeSelf)
        {
            time += Time.deltaTime;

            // ī�޶� ȸ�� ��Ȱ��ȭ
            cinemaCamera.SetActive(false);
            // Ŀ�� Ȱ��ȭ
            Cursor.visible = true;
            // �÷��̾� ��� �׼� ��Ȱ��ȭ
            player.GetComponent<Character_FightAction>().StopAllAction();

            // UI�� ������ 0.2�� �Ŀ� esc�� ������
            if (time > 0.2f && Input.GetKeyDown(KeyCode.Escape))
            {
                if (EquipmentItemPanel.activeSelf)
                {
                    // ���������� �ǳ� ������
                    EquipmentItemPanel.SetActive(false);
                    return;
                }

                // �κ��丮 �ǳ� ����
                inventory_Panel.SetActive(false);      
                // ī�޶� ȸ�� Ǯ��
                cinemaCamera.SetActive(true);
                // Ŀ�� ��Ȱ��ȭ
                Cursor.visible = false;
                // �÷��̾� ��� �׼� Ȱ��ȭ
                player.GetComponent<Character_FightAction>().PlayAllAction();

                time = 0f;
            }
        }
    }    


    public void OnEquipmentButtonClick()
    {
        // ���� �ǳڵ� �� ��������
        for (int i = 0; i < inven_Panels.Length; i++)
        {
            inven_Panels[i].SetActive(false);
        }

        inven_Panels[0].SetActive(true);
    }

    public void OnItemPanelButtonClick()
    {
        // ���� �ǳڵ� �� ��������
        for (int i = 0; i < inven_Panels.Length; i++)
        {
            inven_Panels[i].SetActive(false);
        }

        inven_Panels[1].SetActive(true);


        // ������UI ������Ʈ
        inventorySystem.UpdateInventoryUI();
    }
    public void OnSettingPanelButtonClick()
    {
        // ���� �ǳڵ� �� ��������
        for (int i = 0; i < inven_Panels.Length; i++)
        {
            inven_Panels[i].SetActive(false);
        }

        inven_Panels[2].SetActive(true);
    }

    // ������ ���� �Է�
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

    // ��� ������ ��ư ��������
    public void OnUseItemButtonClick(int num)
    {
        // ��� ������ ��ư ��ȣ�� ���� �ޱ�
        UseButtonNum = num;

        // ���������� �ǳ� Ȱ��ȭ
        EquipmentItemPanel.SetActive(true);

        // ������UI ������Ʈ
        inventorySystem.UpdateInventoryUI();
    }

    // ���������� �ǳ� �� ������ ����������
    public void UseItemSelect(Item item)
    {
        // ������ ��ư�� ������Ʈ�� �����Ͽ� ������ ���� �Ѱ��ֱ�
        UseButtons[UseButtonNum].GetComponent<UseItemButton>().Item = item;

        // �ǳ� ����
        EquipmentItemPanel.SetActive(false);

        // �ش� ������ ������Ʈ
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
