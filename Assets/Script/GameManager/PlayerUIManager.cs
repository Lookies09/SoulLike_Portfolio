using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    // �÷��̾� ���� ü��
    [SerializeField] private Image playerRedHealth;

    // �÷��̾� ���� ü�� ���󰡴� ȸ�� ü�¹�
    [SerializeField] private Image playerGrayHealth;

    // ü�� UI ��ü
    [SerializeField] private GameObject postureBar;

    // ü�� �� ���
    [SerializeField] private Image postureBackGround;

    // �÷��̾� ü�� ������ ������, ����
    [SerializeField] private Image postureGauge_R;
    [SerializeField] private Image postureGauge_L;

    // �÷��̾� ����
    [SerializeField] private GameObject Player;

    // ��� �˸� �ؽ�Ʈ
    [SerializeField] private GameObject deadText;

    // Ÿ�� ���� �̹���
    [SerializeField] private Image targetFocusImg;

    // ĵ���� ��Ʈ Ʈ������
    [SerializeField] private RectTransform canvasRectTransform;

    // �ΰ��� ��� ������ �̹���
    [SerializeField] private Image inGameUseItem;

    // �ΰ��� ��� ������ ���̹���
    [SerializeField] private Sprite nullItemIMG;

    // �ΰ��� ��� ������ ī��Ʈ
    [SerializeField] private Text useItemCount;

    private int useButtonNum = 0;

    //============================================

    // �κ��丮 UI �޴��� ����
    [SerializeField] private InventoryUIManager inventoryUIManager;

    // ===========================================
    // �÷��̾� ü���� 1�� ����� ����
    private float hpTo1;


    // �ð�
    private float time;

    // ��� �ð�
    private float deadTime;

    private void Awake()
    {
        float hp = Player.GetComponent<PlayerHealth>().Health;
        hpTo1 = 1 / hp;

        // Ŀ�� ���̴°� ��Ȱ��ȭ
        Cursor.visible = false;

    }


    private void Update()
    {

        if (Player.GetComponent<TargetFocus_Movement>().enabled == false)
        {
            targetFocusImg.enabled = false;            
        }
        else
        {
            targetFocusImg.enabled = true;

            GameObject target = Player.GetComponent<TargetFocus_Movement>().target;

            Vector3 targetPosition = Camera.main.WorldToViewportPoint(target.GetComponent<EnemyController>().TargetDotPos.position);

            // ȭ�� ��ǥ�� Canvas �������� ��ȯ
            Vector2 canvasPosition = new Vector2(
                ((targetPosition.x * canvasRectTransform.sizeDelta.x) - (canvasRectTransform.sizeDelta.x * 0.5f)),
                ((targetPosition.y * canvasRectTransform.sizeDelta.y) - (canvasRectTransform.sizeDelta.y * 0.5f)));


            targetFocusImg.rectTransform.anchoredPosition = canvasPosition;
        }


        float hp = Player.GetComponent<PlayerHealth>().Health;
        // �÷��̾� ü�� UI ����
        playerRedHealth.fillAmount = hp * hpTo1;

        // ���� ü���� �� ���� �� �� ����
        if (playerRedHealth.fillAmount < playerGrayHealth.fillAmount)
        {
            time += Time.deltaTime;

            // 2�� �Ŀ� ȸ�� ü�� ����
            if (time > 1f)
            {
                playerGrayHealth.fillAmount -= 0.8f * Time.deltaTime;
            }            
        }
        else
        {
            time = 0;
        }

        // ü�� UI ����
        if (Player.GetComponent<PlayerHealth>().Posture > 0)
        {
            postureBar.SetActive(true);
            postureGauge_R.fillAmount = Player.GetComponent<PlayerHealth>().Posture * 0.01f;
            postureGauge_L.fillAmount = postureGauge_R.fillAmount;

            if (Player.GetComponent<PlayerHealth>().Posture < 50)
            {
                postureBackGround.color = new Color32(0, 0, 0, 200);
            }
            else
            {
                postureBackGround.color = new Color32(178, 0, 0, 200);
            }

        }
        else
        {
            postureBar.SetActive(false);
        }


        if (Player.GetComponent<PlayerHealth>().Health <= 0)
        {
            deadTime += Time.deltaTime;
            if (deadTime > 1.5f)
            {
                deadText.SetActive(true);
            }            
        }
        else
        {
            deadTime = 0;
            deadText.SetActive(false);
        }

        ItemSwap();

        UseItem(inventoryUIManager.UseButtons[useButtonNum].GetComponent<UseItemButton>().Item);
    }


    // �ΰ��� ������ ����
    public void ItemSwap()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            useButtonNum++;
            if (useButtonNum > 4)
            {
                useButtonNum = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            useButtonNum--;
            if (useButtonNum < 0)
            {
                useButtonNum = 4;
            }
        }

        if (inventoryUIManager.UseButtons[useButtonNum].GetComponent<UseItemButton>().Item == null)
        {
            inGameUseItem.sprite = nullItemIMG;
            useItemCount.text = "";
            return;
        }
        inGameUseItem.sprite = inventoryUIManager.UseButtons[useButtonNum].GetComponent<UseItemButton>().Item.ItemIcon;
        useItemCount.text = inventoryUIManager.UseButtons[useButtonNum].GetComponent<UseItemButton>().Item.ItemCount.ToString();
    }

    public void UseItem(Item item)
    {
        // RŰ�� ������
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (item == null)
            {
                Debug.Log("�������� ����ֽ��ϴ�");
                return;
            }

            if (item.ItemCount > 0)
            {
                item.Use();
                Debug.Log("������ ���");
            }            
        }
    }
}
