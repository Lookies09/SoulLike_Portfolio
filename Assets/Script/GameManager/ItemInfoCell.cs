using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoCell : MonoBehaviour
{
    // �� �׸� ��� �ؽ�Ʈ
    [SerializeField] private Image itemIMG;
    [SerializeField] private Text nameText;
    [SerializeField] private Text itemCount;

    private InventoryUIManager inventoryUIManager;

    // ������ ���� ����
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

    // ����ǰ ��ư �������� ���� �Ѱ��ֱ�
    public void OnItemInfoCellClick()
    {
        inventoryUIManager.ItemSelected(item);
    }

    // ���� ������ ���������� ������ ���� �Ѱ��ֱ�
    public void OnUseItemSelected()
    {
        inventoryUIManager.UseItemSelect(item);
    }

}
