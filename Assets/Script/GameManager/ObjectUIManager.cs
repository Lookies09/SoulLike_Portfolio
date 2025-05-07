using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ObjectUIManager : MonoBehaviour
{
    // �÷��̾� ����
    private GameObject player;

    // �÷��̾� �ʱ� ü��
    private int preHealth;

    // ���� �ǳ� UI
    [SerializeField] private GameObject panel;

    // ���� �ǳʱ� �ǳ� UI
    [SerializeField] private GameObject teleportPanel;

    // �� ���� �÷��̾� ü��
    [SerializeField] private Image inGameHealth;

    // �ǳ� �÷��̾� ü��
    [SerializeField] private Image panelHealth;

    // ��� �̹���
    [SerializeField] private Image teleportPosImg;

    // ��� �̹��� �迭
    [SerializeField] private Sprite[] teleportPosImgs;

    // ���� �ǳʱ� �߿��� ��� ��ư
    [SerializeField] private Button importantPosButton;

    // ���� �ǳʱ� �ڼ��� ��� ��ư �迭
    [SerializeField] private Button[] detailPosButton;

    // ��� �ó׸��� ī�޶�
    [SerializeField] private GameObject cinemaCamera;

    

    private void Awake()
    {
        player = GameObject.Find("Player");

        preHealth = player.GetComponent<PlayerHealth>().Health;
    }

    // Update is called once per frame
    void Update()
    {
        // �ǳ� ü�°� �ΰ��� ü�� ���� ���߱�
        panelHealth.fillAmount = inGameHealth.fillAmount;

        

        // ���� �ǳ��� Ȱ��ȭ �Ǿ���
        if (panel.activeSelf)
        {
            // ��� �ǳ� �ѹ��� �ݱ�
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // ����ǳʱ� �ǳ� ��Ȱ��ȭ
                teleportPanel.SetActive(false);
                // ī�޶� ȸ�� Ǯ��
                cinemaCamera.SetActive(true);
                // �ǳ� ��Ȱ��ȭ
                panel.SetActive(false);
                // Ŀ�� ��Ȱ��ȭ
                Cursor.visible = false;
                // �÷��̾� ��� �׼� Ȱ��ȭ
                player.GetComponent<Character_FightAction>().PlayAllAction();
            }


            // ����ǳʱ� �ǳ��� Ȱ��ȭ�Ǿ���
            if (teleportPanel.activeSelf)
            {
                // �߿��� ��� ��ư�� Ȱ��ȭ �Ǿ��ٸ�
                if (importantPosButton.interactable == true)
                {
                    // ���콺 ��Ŭ����
                    if (Input.GetMouseButtonDown(1))
                    {
                        // ����ǳʱ� �ǳ� ��Ȱ��ȭ
                        teleportPanel.SetActive(false);
                    }
                }
                // �߿��� ��� ��ư�� ��Ȱ��ȭ �Ǿ��ٸ�
                else
                {
                    // ���콺 ��Ŭ����
                    if (Input.GetMouseButtonDown(1))
                    {
                        // �߿��� ��� ��ư Ȱ��ȭ �ϰ� ������ġ ��Ȱ��ȭ
                        TeleportPanelButtonctrl(true);
                    }
                }
            }
            // ����ǳʱ� �ǳ� ��Ȱ��ȭ�Ǿ��ٸ�(���� �ǳڸ� �ִٸ�)
            else
            {
                // ���콺 ��Ŭ����
                if (Input.GetMouseButtonDown(1))
                {
                    // �ǳ� �ݱ�
                    panel.SetActive(false);
                    // ī�޶� ȸ�� Ǯ��
                    cinemaCamera.SetActive(true);
                    // Ŀ�� ��Ȱ��ȭ
                    Cursor.visible = false;
                    // �÷��̾� ��� �׼� Ȱ��ȭ
                    player.GetComponent<Character_FightAction>().PlayAllAction();
                }
            }
        }       

    }

    // �޽��ϱ� ��ư Ŭ��
    public void OnRestButtonClick()
    {
        player.GetComponent<PlayerHealth>().Health = preHealth;
    }

    // ���� �ǳʱ� ��ư Ŭ��
    public void OnTeleportButtonClick()
    {
        Debug.Log("��ư ����");
        teleportPanel.SetActive(true);

        // �߿��� ��� ��ư Ȱ��ȭ �ϰ� ������ġ ��Ȱ��ȭ
        TeleportPanelButtonctrl(true);

    }

    // ���� ��ġ ��ư�� Ŀ�� �÷�����
    public void OnDetailPosButtonTriggerEnter(int num)
    {
        teleportPosImg.sprite = teleportPosImgs[num];
    }

    // ���� ��ġ ��ư Ŭ��
    public void OnDetailPosButtonClick(int num)
    {
        SceneManager.LoadScene(num);
    }

    // �߿��� ��� ��ư Ŭ��
    public void OnImportantPosButtonClick()
    {
        // �߿��� ��� ��ư ��Ȱ��ȭ �ϰ� ������ġ Ȱ��ȭ
        TeleportPanelButtonctrl(false);
    }

    // ����ǳʱ� �ǳ� ��ư ��Ʈ��
    public void TeleportPanelButtonctrl(bool active)
    {
        // �߿��� ��� ��ư active
        importantPosButton.interactable = active;

        // ���� ��ġ ��ư !active
        for (int i = 0; i < detailPosButton.Length; i++)
        {
            detailPosButton[i].interactable = !active;            
        }
    }

}
