using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct ItemInfo
{
    // ������ ���̵�
    public int ItemId;

    // ������ Ÿ��
    public EnumTypes.ITEM_TYPE itemType;
}

public class ItemBag : MonoBehaviour
{
    // UI ��ġ
    [SerializeField] private Transform uiPos;

    // ������ ��ȣ�ۿ� Ui �̹���
    [SerializeField] private Image objectUI_img;

    // ������ ��ȣ�ۿ� Ui �ؽ�Ʈ
    [SerializeField] private Text objectUI_Text;

    // ��ȣ�ۿ� UI ��ü
    [SerializeField] private GameObject info_UI_all;

    // ĵ���� ��Ʈ Ʈ������
    [SerializeField] private RectTransform canvasRectTransform;

    // �÷��̾� ���� ����
    private bool isContact;

    // �κ��丮 �ý���
    private InventorySystem inventorySystem;


    // ������ ����
    [SerializeField] private ItemInfo itemInfo;

    // ���� ������ ����
    [SerializeField] private int idNum;

    public ItemInfo ItemInfo { get => itemInfo; set => itemInfo = value; }

    void Start()
    {
        itemInfo.ItemId = idNum;
        inventorySystem = FindObjectOfType<InventorySystem>();
    }


    private void Update()
    {
        if (isContact)
        {
            objectUI_Text.text = "�������� �ݴ´�";


            info_UI_all.SetActive(true);

            Vector3 targetPosition = Camera.main.WorldToViewportPoint(uiPos.position);

            // ȭ�� ��ǥ�� Canvas �������� ��ȯ
            Vector2 canvasPosition = new Vector2(
                ((targetPosition.x * canvasRectTransform.sizeDelta.x) - (canvasRectTransform.sizeDelta.x * 0.5f)),
                ((targetPosition.y * canvasRectTransform.sizeDelta.y) - (canvasRectTransform.sizeDelta.y * 0.5f)));

            objectUI_img.rectTransform.anchoredPosition = canvasPosition;

            // EŰ�� ������
            if (Input.GetKeyDown(KeyCode.E))
            {
                // ȹ���� ������ ������ �κ��丮 �ý��ۿ� �߰�
                bool invenAdded = inventorySystem.AddItem(ItemInfo);

                // �κ��丮 ������ �߰��� �����ߴٸ�
                if (invenAdded)
                {
                    isContact = false;

                    info_UI_all.SetActive(false);

                    // ȹ���� ������ ����
                    Destroy(gameObject);
                }
            }
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
        }
    }
}
