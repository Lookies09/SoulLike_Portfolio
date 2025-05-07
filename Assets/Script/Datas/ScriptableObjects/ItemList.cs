using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ������ ����Ʈ
[CreateAssetMenu(fileName = "ItemList" , menuName = "Item/ItemList")]
public class ItemList : ScriptableObject
{
    // ������ ����Ʈ
    [SerializeField] private List<Item> list;

    public List<Item> List { get => list; set => list = value; }
}
