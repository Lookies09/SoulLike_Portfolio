using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PracticePlacePanel : MonoBehaviour
{
    // 플레이어 참조
    [SerializeField] private GameObject player;

    // 적군 참조
    [SerializeField] private GameObject[] enemys;

    // 연습실 판넬 참조
    [SerializeField] private GameObject pracRommPanel;

    // 시네마신 카메라들
    [SerializeField] private GameObject Caneras;

    // Start is called before the first frame update
    void Start()
    {
        // 판넬 켜기
        pracRommPanel.SetActive(true);
        // 카메라 회전 고정
        Caneras.SetActive(false);
        // 플레이어 모든 액션 정지
        player.GetComponent<Character_FightAction>().StopAllAction();
        // 커서 활성화
        Cursor.visible = true;

    }

    // Update is called once per frame
    void Update()
    {
    }

    // 적군 선택 버튼 클릭
    public void OnSelectEnemyButtonClick(int num)
    {
        // 적군 소환
        enemys[num].SetActive(true);

        //판넬 끄기
        pracRommPanel.SetActive(false);

        // 카메라 회전 풀기
        Caneras.SetActive(true);
        // 플레이어 모든 액션 풀기
        player.GetComponent<Character_FightAction>().PlayAllAction();
        // 커서 비활성화
        Cursor.visible = false;
    }

    // 적군 선택 하지 않기 버튼 클릭
    public void OnNonSelectEnemyButtonClick()
    {
        //판넬 끄기
        pracRommPanel.SetActive(false);

        // 카메라 회전 풀기
        Caneras.SetActive(true);
        // 플레이어 모든 액션 풀기
        player.GetComponent<Character_FightAction>().PlayAllAction();
        // 커서 비활성화
        Cursor.visible = false;
    }
}
