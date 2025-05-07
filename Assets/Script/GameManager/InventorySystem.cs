using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class InventorySystem : MonoBehaviour
{
    // ������ ���� ��ũ���ͺ� ������Ʈ ����
    [SerializeField] private ItemList itemList;

    // �κ��丮 ũ�� (���� ����)
    [SerializeField] private int inventorySize;

    // ȹ���� ������ ����Ʈ
    [SerializeField] private List<Item> hasItemList = new List<Item>();

    public List<Item> HasItemList { get => hasItemList; set => hasItemList = value; }
    public int InventorySize { get => inventorySize; set => inventorySize = value; }

    // ������ ���� �� ������
    [SerializeField] private GameObject itemInfoCellPrefab;

    // ���������� ���� �� ������
    [SerializeField] private GameObject equipmentItemInfoCellPrefab;

    // ��ũ�Ѻ��� �������� ����
    [SerializeField] private Transform cellContentView;

    // ���������� ���� ��ũ�Ѻ��� �������� ����
    [SerializeField] private Transform equipmentCellContentView;


    public void OnAddItemInfoCallback(Item item)
    {
        GameObject itemInfoCell = Instantiate(itemInfoCellPrefab, cellContentView);
        GameObject itemInfoCell2 = Instantiate(equipmentItemInfoCellPrefab, equipmentCellContentView);


        // ������ ������ ���� �ݹ����� �Ѱ� ���� �����͸� ǥ���� (�� �ʱ�ȭ)
        itemInfoCell.GetComponent<ItemInfoCell>().Init(item);
        itemInfoCell2.GetComponent<ItemInfoCell>().Init(item);
    }


    public bool AddItem(ItemInfo itemInfo)
    {
        Item hasItem = HasItemList.FirstOrDefault(item => item.ItemId == itemInfo.ItemId);

        // �̹� ȹ���� �Ҹ� �������̸�
        if (hasItem != null)
        {
            hasItem.ItemCount++;
        }
        else
        {
            // ȹ���� ������ ������ ������ ����Ʈ���� ã�� �ش� ������ ����
            Item item = itemList.List.FirstOrDefault(item => item.ItemId == itemInfo.ItemId).Clone();

            if (item != null)
            {                
                // �κ��丮�� ȹ���� ������ �߰�
                hasItemList.Add(item);               
            }
            
        }

        return true;
    }

    public void UpdateInventoryUI()
    {
        // ������ �� ��ü�� �� ã�� ������
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("ContentCell");

        for (int i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }


        // ȹ���� ������ ������ ������ ǥ�� UI�� ������
        for (int i = 0; i < HasItemList.Count; i++)
        {
            // ���� ���� ��°�� �ش��ϴ� �κ��丮 ���� �����ۿ� ������ ǥ����
            Item item = HasItemList[i];
            OnAddItemInfoCallback(item);
        }

    }
}
