using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static EnumTypes;

//소모성 아이템
[CreateAssetMenu(fileName = "ConsumableItem", menuName = "Item/Consumable")]
public class ConsumableItem : Item
{
    // 소모성 아이템 타입
    [SerializeField] private EnumTypes.CB_TYPE cb_Type;
    // 아이템 증가 수치
    [SerializeField] private int upValue;

    // 플레이어 참조
    private GameObject player;

    public EnumTypes.CB_TYPE CbType { get => cb_Type; set => cb_Type = value; }
    public int UpValue { get => upValue; set => upValue = value; }

    private void Awake()
    {
        player = GameObject.Find("Player");
    }
        

    public override void Use()
    {
        // [예시 코드]

        // 소모성 아이템 사용 -> 아이템 게임오브젝트의 Start 이벤트에서 기능 수행 처리
        // instantiate(itemPrefab);

        // 아이템 사용 갯수 감소
        itemCount--;

        // 체력올리기 일시
        if (cb_Type == CB_TYPE.HP_UP)
        {
            Debug.Log("체력회복");
            // 10 회복
            player.GetComponent<PlayerHealth>().RecoveryHealth(upValue);
        }
        // 공격력 올리기 일시
        else if (cb_Type == CB_TYPE.DMG_UP)
        {
            Debug.Log("공격력 증가");
            player.GetComponent <PlayerHealth>().DMGUpItem();
        }
        // 재화 추가 일시
        else
        {
            Debug.Log("골드 추가");
        }

    }

}
