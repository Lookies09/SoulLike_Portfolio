using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportLantern : MonoBehaviour
{
    [SerializeField] private GameObject lightEffect;

    // UI ��ġ
    [SerializeField] private Transform uiPos;

    // ���� ��ȣ�ۿ� Ui �̹���
    [SerializeField] private Image lantern_UI;

    // ���� ��ȣ�ۿ� Ui �ؽ�Ʈ
    [SerializeField] private Text lantern_Text;

    // ��ȣ�ۿ� UI ��ü
    [SerializeField] private GameObject info_UI_all;

    // ĵ���� ��Ʈ Ʈ������
    [SerializeField] private RectTransform canvasRectTransform;

    // �ǳ� UI
    [SerializeField] private GameObject panel;

    // �÷��̾� ����
    private GameObject player;

    // �÷��̾� ���� ����
    private bool isContact;

    // ���� ����� �ҽ���
    [SerializeField] private AudioSource[] audioSources;

    // ��� �ó׸��� ī�޶�
    [SerializeField] private GameObject cinemaCamera;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (isContact)
        {
            lightEffect.SetActive(true);
            lantern_Text.text = "��� �����Ѵ�";


            info_UI_all.SetActive(true);

            Vector3 targetPosition = Camera.main.WorldToViewportPoint(uiPos.position);

            // ȭ�� ��ǥ�� Canvas �������� ��ȯ
            Vector2 canvasPosition = new Vector2(
                ((targetPosition.x * canvasRectTransform.sizeDelta.x) - (canvasRectTransform.sizeDelta.x * 0.5f)),
                ((targetPosition.y * canvasRectTransform.sizeDelta.y) - (canvasRectTransform.sizeDelta.y * 0.5f)));

            lantern_UI.rectTransform.anchoredPosition = canvasPosition;

            // ����� �����ϰ� E�� �����ٸ�.
            if (Input.GetKeyDown(KeyCode.E))
            {
                info_UI_all.SetActive(false);

                for (int i = 0; i < audioSources.Length; i++)
                {
                    audioSources[i].Play();
                }               

                // �ǳ� Ȱ��ȭ
                panel.SetActive(true);                

                isContact = false;
            }
        }

        if (panel.activeSelf)
        {
            // ī�޶� ȸ�� ����
            cinemaCamera.SetActive(false);

            // �÷��̾� ��� �׼� ����
            player.GetComponent<Character_FightAction>().StopAllAction();

            // Ŀ�� Ȱ��ȭ
            Cursor.visible = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {            
            isContact = true;            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {            
            isContact = false;

            info_UI_all.SetActive(false);
            lightEffect.SetActive(false);
        }
    }
}
