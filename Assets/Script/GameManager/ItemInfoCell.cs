using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoCell : MonoBehaviour
{
    // 셀 항목 출력 텍스트
    [SerializeField] private Image itemIMG;
    [SerializeField] private Text nameText;
    [SerializeField] private Text itemCount;

    private InventoryUIManager inventoryUIManager;

    // 아이템 정보 저장
    private Item item;

    private void Awake()
    {
        inventoryUIManager = GameObject.Find("Canvas").GetComponent<InventoryUIManager>();
    }

    // Start is called before the first frame update
    public void Init(Item item)
    {
        this.item = item;

        nameText.text = item.ItemName;
        itemIMG.sprite = item.ItemIcon;
        itemCount.text = item.ItemCount.ToString();

    }

    // 소지품 버튼 눌렀을때 정보 넘겨주기
    public void OnItemInfoCellClick()
    {
        inventoryUIManager.ItemSelected(item);
    }

    // 장착 아이템 선택했을때 아이템 정보 넘겨주기
    public void OnUseItemSelected()
    {
        inventoryUIManager.UseItemSelect(item);
    }

}
