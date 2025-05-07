using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class InventorySystem : MonoBehaviour
{
    // 아이템 정보 스크립터블 오브젝트 참조
    [SerializeField] private ItemList itemList;

    // 인벤토리 크기 (슬롯 갯수)
    [SerializeField] private int inventorySize;

    // 획득한 아이템 리스트
    [SerializeField] private List<Item> hasItemList = new List<Item>();

    public List<Item> HasItemList { get => hasItemList; set => hasItemList = value; }
    public int InventorySize { get => inventorySize; set => inventorySize = value; }

    // 아이템 정보 셀 프리펩
    [SerializeField] private GameObject itemInfoCellPrefab;

    // 장착아이템 정보 셀 프리펩
    [SerializeField] private GameObject equipmentItemInfoCellPrefab;

    // 스크롤뷰의 콘텐츠뷰 참조
    [SerializeField] private Transform cellContentView;

    // 장착아이템 선택 스크롤뷰의 콘텐츠뷰 참조
    [SerializeField] private Transform equipmentCellContentView;


    public void OnAddItemInfoCallback(Item item)
    {
        GameObject itemInfoCell = Instantiate(itemInfoCellPrefab, cellContentView);
        GameObject itemInfoCell2 = Instantiate(equipmentItemInfoCellPrefab, equipmentCellContentView);


        // 생성한 아이템 셀에 콜백으로 넘겨 받은 데이터를 표시함 (셀 초기화)
        itemInfoCell.GetComponent<ItemInfoCell>().Init(item);
        itemInfoCell2.GetComponent<ItemInfoCell>().Init(item);
    }


    public bool AddItem(ItemInfo itemInfo)
    {
        Item hasItem = HasItemList.FirstOrDefault(item => item.ItemId == itemInfo.ItemId);

        // 이미 획득한 소모성 아이템이면
        if (hasItem != null)
        {
            hasItem.ItemCount++;
        }
        else
        {
            // 획득한 아이템 정보를 아이템 리스트에서 찾아 해당 아이템 생성
            Item item = itemList.List.FirstOrDefault(item => item.ItemId == itemInfo.ItemId).Clone();

            if (item != null)
            {                
                // 인벤토리에 획득한 아이템 추가
                hasItemList.Add(item);               
            }
            
        }

        return true;
    }

    public void UpdateInventoryUI()
    {
        // 콘텐츠 셀 전체를 다 찾은 다음에
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("ContentCell");

        for (int i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }


        // 획득한 아이템 정보를 아이템 표시 UI에 설정함
        for (int i = 0; i < HasItemList.Count; i++)
        {
            // 현재 슬롯 번째에 해당하는 인벤토리 보유 아이템에 정보를 표시함
            Item item = HasItemList[i];
            OnAddItemInfoCallback(item);
        }

    }
}
