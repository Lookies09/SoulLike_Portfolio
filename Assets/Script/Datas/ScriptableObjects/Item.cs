using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    // ������ Ÿ��
    [SerializeField] protected EnumTypes.ITEM_TYPE itemTYpe;
    // ������ ���̵�
    [SerializeField] protected int itemId;
    // ������ �̸�
    [SerializeField] protected string itemName;
    //������ ����
    [SerializeField] protected string description;
    // ������ ������
    [SerializeField] protected Sprite itemIcon;
    // ������ ����
    [SerializeField] protected int itemPrice;
    // ������ ����
    [SerializeField] protected int itemCount;

    [SerializeField] protected GameObject itemPrefab;

    public EnumTypes.ITEM_TYPE ItemTYpe { get => itemTYpe; set => itemTYpe = value; }
    public int ItemId { get => itemId; set => itemId = value; }
    public string ItemName { get => itemName; set => itemName = value; }
    public string Description { get => description; set => description = value; }
    public Sprite ItemIcon { get => itemIcon; set => itemIcon = value; }
    public int ItemPrice { get => itemPrice; set => itemPrice = value; }
    public int ItemCount { get => itemCount; set => itemCount = value; }


    // ������ ����
    public Item Clone()
    {
        Item newItem = Instantiate(this);
        return newItem;
    }

    public abstract void Use();
}
