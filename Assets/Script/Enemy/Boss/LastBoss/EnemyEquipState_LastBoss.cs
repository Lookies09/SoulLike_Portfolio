using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEquipState_LastBoss : EnemyAttackAbleState
{
    // 목소리 오디오소스
    [SerializeField] private AudioSource voiceAudio;

    // 칼소리 오디오소스
    [SerializeField] private AudioSource katanaAudio;

    // 웃는 목소리 사운드
    [SerializeField] private AudioClip laughSound;

    // 칼뽑는 사운드
    [SerializeField] private AudioClip katanaDrawSound;

    private float time = 0;

    // 왼손 칼집 잡는 부분 칼집
    [SerializeField] private GameObject L_Hand_KatanaZip;

    // 왼손 칼집 잡는 부분 칼
    [SerializeField] private GameObject L_Hand_Katana;

    // 오른손 뽑는 칼
    [SerializeField] private GameObject R_Hand_Katana;

    // 오른손 진짜 칼
    [SerializeField] private GameObject R_Hand_Real_Katana;

    // 허리춤에 뽑는 칼
    [SerializeField] private GameObject Spine_Equip_Katana;

    // 허리춤에 보이는 칼
    [SerializeField] private GameObject Spine_Look_Katana;


    // 발도 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        // 네비게이션 에이전트 이동 정지
        navMeshAgent.isStopped = true;

        // 발도 애니메이션 재생
        animator.SetInteger("State", (int)state);
    }

    // 대기 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {
        time += Time.deltaTime;

        // 플레이거 공격 가능 거리안에 들어왔다면
        if (controller.GetPlayerDistance() <= attackDistance)
        {
            // 공격 상태로 전환
            controller.TransactionToState(3);
            return;
        }

        // 플레이어가 추적 가능 거리안에 들어왔다면
        if (controller.GetPlayerDistance() <= detectDistance && time >= 3f)
        {
            // 움직임 상태로 전환
            controller.TransactionToState(1);
            return;
        }


    }

    // 대기 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
        time = 0;
    }

    // 왼손이 칼집에 손올리는 이벤트
    public void HandOnKatanaEvent()
    {
        // 처음 허리에 있는 칼 해제
        Spine_Equip_Katana.SetActive(false);

        // 칼집 잡는 칼, 칼집 활성화
        L_Hand_Katana.SetActive(true);
        L_Hand_KatanaZip.SetActive(true);

    }

    // 오른손이 칼을 잡는 이벤트
    public void GrabKatanaEvent()
    {
        // 왼손 칼집에 있는 칼 해제
        L_Hand_Katana.SetActive(false);

        // 오른손 칼 활성화
        R_Hand_Katana.SetActive(true);

    }

    // 칼집에 손 놓는 이벤트
    public void EquipKatanaEvent()
    {
        // 왼손 칼집 해제
        L_Hand_KatanaZip.SetActive(false);

        // 허리춤에 보이는 칼 활성화
        Spine_Look_Katana.SetActive(true);

        // 오른손 뽑는칼 비활성화
        R_Hand_Katana.SetActive(false);

        // 오른손 진짜 칼 활성화
        R_Hand_Real_Katana.SetActive(true);
    }

    public void LaughSoundEvent()
    {
        voiceAudio.clip = laughSound;
        voiceAudio.Play();

    }

    public void KatanaDrawSoundEvent()
    {
        katanaAudio.clip = katanaDrawSound;
        katanaAudio.Play();

    }
}
