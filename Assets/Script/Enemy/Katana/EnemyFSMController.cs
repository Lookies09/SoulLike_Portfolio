using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSMController : EnemyController
{
    // 적(칼) 상태들
    public enum STATE { IDLE, WANDER, DETECT, ATTACK, GIVEUP,  HIT, DEATH , DEFENSE, REBOUND }

    //==============================================
    
    // 공격 범위
    [SerializeField] private float atteckRadius;

    // 공격 범위 각도
    [SerializeField] private float hitAngle;

    // 공격 타겟 중심점 위치
    [SerializeField] private Transform atteckTransform;

    // 공격 대상 레이어
    [SerializeField] private LayerMask targetLayer;

    // 기본공격 데미지
    [SerializeField] private int nomalDMG;

    // 강공격 데미지
    [SerializeField] private int specialDMG;

    [SerializeField] private AudioSource katanaAudio;

    // 칼 스윙 오디오 클립
    [SerializeField] private AudioClip swingSound;

    // 움직임 오디로 소스
    [SerializeField] private AudioSource movementAudio;
       

    // 걷는 소리
    [SerializeField] private AudioClip walkClip;

    


    // ==================================================


    public void EnemyNomalAtteckAnimationEvent()
    {
        Collider[] hits = Physics.OverlapSphere(atteckTransform.position, atteckRadius, targetLayer);

        foreach (Collider hit in hits)
        {
            Vector3 Direction = hit.transform.position - transform.position;

            float angleToTarget = Vector3.Angle(transform.forward, Direction);

            if (angleToTarget < hitAngle)
            {
                if (hit.GetComponent<Character_Defense>().OnParry)
                {
                    // 적군 패링 영향
                    gameObject.GetComponent<EnemyAttackState>().OnRebound = true;
                    return;
                }
                else if (hit.GetComponent<Character_Defense>().IsDefense)
                {
                    // Debug.Log("플레이어가 방어함");
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


    public void SwordSwingSoundEvent()
    {
        katanaAudio.clip = swingSound;
        katanaAudio.Play();
    }

    public void WalkSoundEvent()
    {
        movementAudio.clip = walkClip;
        movementAudio.Play();

    }

    
}
