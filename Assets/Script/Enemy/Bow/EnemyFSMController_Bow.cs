using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

// 적(활) 컨트롤러
public class EnemyFSMController_Bow : EnemyController
{
    // 적(활) 상태들
    public enum STATE { IDLE, WANDER, ATTACK, GIVEUP, HIT, DEATH }

    // 껏다 켰다하는 화살
    [SerializeField] private GameObject arrowOnHand;

    // 실제 발사되는 화살 프리펩
    [SerializeField] private GameObject arrowprefab;

    private Vector3 direction;

    // 발사 위치
    [SerializeField]  private Transform shootPos;

    // 화살 타겟
    [SerializeField] private Transform Target;

    // 활 시위 당기는 오디오 클립
    [SerializeField] private AudioClip bowString;

    // 활 쏘는 오디오 클립
    [SerializeField] private AudioClip bowShoot;

    [SerializeField] private AudioSource audioSource;

    // 움직임 오디오 소스
    [SerializeField] private AudioSource movementAudio;

    // 걷는 소리
    [SerializeField] private AudioClip walkClip;



    // 화살 뽑는 애니메이션 이벤트
    public void ArrowPickup()
    {
        Debug.Log("화살뽑음");
        arrowOnHand.SetActive(true);
    }

    // 활 시위 당기는 애니메이션 이벤트
    public void BowStringEvent()
    {
        audioSource.clip = bowString;
        audioSource.Play();
    }


    // 화살 발사하는 애니메이션 이벤트
    public void ShootArrow()
    {
        direction = (Target.position - shootPos.position).normalized;
        //direction.y = 0;

        Debug.Log("화살발사");
        arrowOnHand.SetActive(false);

        audioSource.clip = bowShoot;
        audioSource.Play();

        GameObject arrow = Instantiate(arrowprefab, shootPos.position, Quaternion.LookRotation(direction));
        arrow.GetComponent<ArrowMovement>().direction = direction;



    }


    public void RigBuilderOnEvent()
    {
        gameObject.GetComponent<RigBuilder>().layers[0].active = true;
    }

    public void RigBuilderOffEvent()
    {
        gameObject.GetComponent<RigBuilder>().layers[0].active = false;
    }

    public void WalkSoundEvent()
    {
        movementAudio.clip = walkClip;
        movementAudio.Play();

    }
}
