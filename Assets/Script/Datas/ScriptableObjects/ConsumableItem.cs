using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static EnumTypes;

//�Ҹ� ������
[CreateAssetMenu(fileName = "ConsumableItem", menuName = "Item/Consumable")]
public class ConsumableItem : Item
{
    // �Ҹ� ������ Ÿ��
    [SerializeField] private EnumTypes.CB_TYPE cb_Type;
    // ������ ���� ��ġ
    [SerializeField] private int upValue;

    // �÷��̾� ����
    private GameObject player;

    public EnumTypes.CB_TYPE CbType { get => cb_Type; set => cb_Type = value; }
    public int UpValue { get => upValue; set => upValue = value; }

    private void Awake()
    {
        player = GameObject.Find("Player");
    }
        

    public override void Use()
    {
        // [���� �ڵ�]

        // �Ҹ� ������ ��� -> ������ ���ӿ�����Ʈ�� Start �̺�Ʈ���� ��� ���� ó��
        // instantiate(itemPrefab);

        // ������ ��� ���� ����
        itemCount--;

        // ü�¿ø��� �Ͻ�
        if (cb_Type == CB_TYPE.HP_UP)
        {
            Debug.Log("ü��ȸ��");
            // 10 ȸ��
            player.GetComponent<PlayerHealth>().RecoveryHealth(upValue);
        }
        // ���ݷ� �ø��� �Ͻ�
        else if (cb_Type == CB_TYPE.DMG_UP)
        {
            Debug.Log("���ݷ� ����");
            player.GetComponent <PlayerHealth>().DMGUpItem();
        }
        // ��ȭ �߰� �Ͻ�
        else
        {
            Debug.Log("��� �߰�");
        }

    }

}
