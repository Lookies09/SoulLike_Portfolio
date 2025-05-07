using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth_LastBoss : ObjectHealth
{
    private EnemyController controller;

    // 가드 이펙트
    [SerializeField] private GameObject guardEffect;
    // 가드 이펙트 위치
    [SerializeField] private Transform guardEffectPos;

    // 처형 가능 표시
    [SerializeField] private GameObject deathDot;

    // 히트 오디오소스
    [SerializeField] private AudioSource hitAudio;

    // 히트 사운드
    [SerializeField] private AudioClip hitSound;


    // 공격 중 히트 가능
    private bool canHit;

    public override void Awake()
    {
        base.Awake();
        controller = GetComponent<EnemyController>();
        startHp = health;
    }

    private void Update()
    {
        Excution();

        // 걷거나 배회 상태라면
        if (controller.CurrentState == controller.EnemyStates1[7] || controller.CurrentState == controller.EnemyStates1[1])
        {
            if (health >= startHp / 2)
            {
                posture -= 10 * Time.deltaTime;
            }
            else
            {
                posture -= 2 * Time.deltaTime;
            }

        }
    }

    // 플레이어에게 공격 받음
    public override void Hit(int DMG)
    {
        // 현재 상태가 이미 사망한 상태면 피격 처리하지 않음
        if (health <= 0) return;

        // 현재 방어 중이라면
        if (controller.CurrentState == controller.EnemyStates1[6])
        {
            // 가드 이펙트 재생
            Instantiate(guardEffect, guardEffectPos.position, Quaternion.identity);
            animator.SetTrigger("DefenseHit");

            // 체간 증가
            posture += 2;
            return;
        }
        else // 맞을 수 있는 상황에서
        {
            isHit = true;

            // 체간 증가
            posture += 6;

            // 히트 소리 재생
            hitAudio.clip = hitSound;
            hitAudio.Play();

            // 공격 상태이고 
            if (controller.CurrentState == controller.EnemyStates1[3])
            {

                // 히트가 가능한 상태면
                if (canHit)
                {
                    // 체력감소
                    health -= DMG;

                    // 피격 상태로 전환
                    controller.TransactionToState(4);
                    return;
                }
                else
                {
                    // 체력만 감소
                    health -= DMG;
                    return;
                }

            }
            else
            {
                // 체력 감소 하고
                health -= DMG;

                // 피격 상태로 전환
                controller.TransactionToState(4);
            }



        }


    }

    public override void Death()
    {
        // 처형 애니메이션 상태로 전환
        health = 0;
        isDead = true;
        controller.TransactionToState(5);
        return;
    }

    public void Excution()
    {
        if (health > 0)
        {
            if (posture >= 100)
            {
                posture = 100;
                deathDot.SetActive(true);
                return;
            }

            deathDot.SetActive(false);
        }
        else
        {
            if (isDead)
            {
                deathDot.SetActive(false);
            }
            else
            {
                deathDot.SetActive(true);
            }

        }
    }


    public void CanHitEvent()
    {
        canHit = true;
    }

    public void CannotHitEvent()
    {
        canHit = false;
    }

    public override void postureBreak()
    {
        // 체간이 100 이상이면
        if (posture >= 100)
        {
            // 자세 무너지고 인살 가능

        }
    }
}
