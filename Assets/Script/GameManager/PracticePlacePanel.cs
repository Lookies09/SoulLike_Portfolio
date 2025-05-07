using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PracticePlacePanel : MonoBehaviour
{
    // �÷��̾� ����
    [SerializeField] private GameObject player;

    // ���� ����
    [SerializeField] private GameObject[] enemys;

    // ������ �ǳ� ����
    [SerializeField] private GameObject pracRommPanel;

    // �ó׸��� ī�޶��
    [SerializeField] private GameObject Caneras;

    // Start is called before the first frame update
    void Start()
    {
        // �ǳ� �ѱ�
        pracRommPanel.SetActive(true);
        // ī�޶� ȸ�� ����
        Caneras.SetActive(false);
        // �÷��̾� ��� �׼� ����
        player.GetComponent<Character_FightAction>().StopAllAction();
        // Ŀ�� Ȱ��ȭ
        Cursor.visible = true;

    }

    // Update is called once per frame
    void Update()
    {
    }

    // ���� ���� ��ư Ŭ��
    public void OnSelectEnemyButtonClick(int num)
    {
        // ���� ��ȯ
        enemys[num].SetActive(true);

        //�ǳ� ����
        pracRommPanel.SetActive(false);

        // ī�޶� ȸ�� Ǯ��
        Caneras.SetActive(true);
        // �÷��̾� ��� �׼� Ǯ��
        player.GetComponent<Character_FightAction>().PlayAllAction();
        // Ŀ�� ��Ȱ��ȭ
        Cursor.visible = false;
    }

    // ���� ���� ���� �ʱ� ��ư Ŭ��
    public void OnNonSelectEnemyButtonClick()
    {
        //�ǳ� ����
        pracRommPanel.SetActive(false);

        // ī�޶� ȸ�� Ǯ��
        Caneras.SetActive(true);
        // �÷��̾� ��� �׼� Ǯ��
        player.GetComponent<Character_FightAction>().PlayAllAction();
        // Ŀ�� ��Ȱ��ȭ
        Cursor.visible = false;
    }
}
