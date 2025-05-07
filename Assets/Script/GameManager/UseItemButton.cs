using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseItemButton : MonoBehaviour
{
    private Item item;

    // ��ư ��ȣ
    [SerializeField] private int num;

    [SerializeField] private Image iconImg;

    [SerializeField] private Text itemCount;

    [SerializeField] private InventoryUIManager inventoryUIManager;

    public Item Item { get => item; set => item = value; }

    private void Awake()
    {
        //inventoryUIManager = GameObject.Find("Canvas").GetComponent<InventoryUIManager>();
    }

    // ��ư ��ȣ �Ѱ��ֱ�
    public void OnUseItemButtonClick()
    {
        inventoryUIManager.OnUseItemButtonClick(num);
    }

    // ��ư�� ������ ������Ʈ
    public void UseItemInfoSet()
    {
        if (item == null) { return; }

        iconImg.sprite = item.ItemIcon;
        itemCount.text = item.ItemCount.ToString();
    }

}
