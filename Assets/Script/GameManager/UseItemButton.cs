using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseItemButton : MonoBehaviour
{
    private Item item;

    // 버튼 번호
    [SerializeField] private int num;

    [SerializeField] private Image iconImg;

    [SerializeField] private Text itemCount;

    [SerializeField] private InventoryUIManager inventoryUIManager;

    public Item Item { get => item; set => item = value; }

    private void Awake()
    {
        //inventoryUIManager = GameObject.Find("Canvas").GetComponent<InventoryUIManager>();
    }

    // 버튼 번호 넘겨주기
    public void OnUseItemButtonClick()
    {
        inventoryUIManager.OnUseItemButtonClick(num);
    }

    // 버튼에 아이콘 업데이트
    public void UseItemInfoSet()
    {
        if (item == null) { return; }

        iconImg.sprite = item.ItemIcon;
        itemCount.text = item.ItemCount.ToString();
    }

}
