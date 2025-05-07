using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSMController_LastBoss : EnemyController
{
    // 중간보스 상태들
    public enum STATE { EQUIP, MOVE, DETECT, ATTACK, HIT, DEATH, DEFENSE, REBOUND }

    //==============================================

    // 공격 범위
    [SerializeField] private float attackRadius;

    // 공격 범위 각도
    [SerializeField] private float hitAngle;

    // 공격 타겟 중심점 위치
    [SerializeField] private Transform attackTransform;

    // 공격 대상 레이어
    [SerializeField] private LayerMask targetLayer;

    // 기본공격 데미지
    [SerializeField] private int nomalDMG;

    // 강공격 데미지
    [SerializeField] private int specialDMG;

    // 움직임 오디로 소스
    [SerializeField] private AudioSource movementAudio;

    // 걷는 소리
    [SerializeField] private AudioClip walkClip;

    // 달리는 소리
    [SerializeField] private AudioClip runClip;

    // 점프 소리
    [SerializeField] private AudioClip jumpClip;

    // 착지 소리
    [SerializeField] private AudioClip landClip;

    // 대쉬 소리
    [SerializeField] private AudioClip dodgeClip;

    // 구르는 소리
    [SerializeField] private AudioClip rollClip;


    //==============================================




    public void EnemyNomalAtteckAnimationEvent()
    {
        Collider[] hits = Physics.OverlapSphere(attackTransform.position, attackRadius, targetLayer);

        foreach (Collider hit in hits)
        {
            Vector3 Direction = hit.transform.position - transform.position;

            float angleToTarget = Vector3.Angle(transform.forward, Direction);

            if (angleToTarget < hitAngle)
            {

                if (hit.GetComponent<Character_Defense>().OnParry)
                {
                    // 적군 패링 영향
                    gameObject.GetComponent<EnemyAttackState_LastBoss>().OnRebound = true;
                    return;
                }
                else if (hit.GetComponent<Character_Defense>().IsDefense)
                {
                    Debug.Log("플레이어가 방어함");
                    hit.GetComponent<Character_Defense>().DefenseHit(nomalDMG);
                    return;
                }
                else
                {
                    if (hit.GetComponent<ObjectHealth>().Health > 0)
                    {
                        Debug.Log("플레이어가 맞음");
                        Instantiate(hitEffect, hitEffectPos.position, Quaternion.identity);
                        hit.GetComponent<ObjectHealth>().Hit(nomalDMG);
                        return;
                    }
                }
            }
        }
    }

    public void EnemySpecialAtteckAnimationEvent()
    {
        Collider[] hits = Physics.OverlapSphere(attackTransform.position, attackRadius, targetLayer);

        foreach (Collider hit in hits)
        {
            Vector3 Direction = hit.transform.position - transform.position;

            float angleToTarget = Vector3.Angle(transform.forward, Direction);

            if (angleToTarget < hitAngle)
            {

                if (hit.GetComponent<ObjectHealth>().Health > 0)
                {
                    Debug.Log("플레이어가 맞음");
                    Instantiate(hitEffect, hitEffectPos.position, Quaternion.identity);
                    hit.GetComponent<ObjectHealth>().Hit(specialDMG);
                    return;
                }

            }
        }
    }

    public void WalkSoundEvent()
    {
        movementAudio.clip = walkClip;
        movementAudio.Play();

    }

    public void RunSoundEvent()
    {
        movementAudio.clip = runClip;
        movementAudio.Play();

    }
    public void JumpSoundEvent()
    {
        movementAudio.clip = jumpClip;
        movementAudio.Play();

    }
    public void LandSoundEvent()
    {
        movementAudio.clip = landClip;
        movementAudio.Play();

    }

    public void DodgeSoundEvent()
    {
        movementAudio.clip = dodgeClip;
        movementAudio.Play();

    }

    public void RollSoundEvent()
    {
        movementAudio.clip = rollClip;
        movementAudio.Play();

    }

}
