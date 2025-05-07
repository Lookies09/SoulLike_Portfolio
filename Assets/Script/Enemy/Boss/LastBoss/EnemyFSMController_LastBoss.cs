using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSMController_LastBoss : EnemyController
{
    // �߰����� ���µ�
    public enum STATE { EQUIP, MOVE, DETECT, ATTACK, HIT, DEATH, DEFENSE, REBOUND }

    //==============================================

    // ���� ����
    [SerializeField] private float attackRadius;

    // ���� ���� ����
    [SerializeField] private float hitAngle;

    // ���� Ÿ�� �߽��� ��ġ
    [SerializeField] private Transform attackTransform;

    // ���� ��� ���̾�
    [SerializeField] private LayerMask targetLayer;

    // �⺻���� ������
    [SerializeField] private int nomalDMG;

    // ������ ������
    [SerializeField] private int specialDMG;

    // ������ ����� �ҽ�
    [SerializeField] private AudioSource movementAudio;

    // �ȴ� �Ҹ�
    [SerializeField] private AudioClip walkClip;

    // �޸��� �Ҹ�
    [SerializeField] private AudioClip runClip;

    // ���� �Ҹ�
    [SerializeField] private AudioClip jumpClip;

    // ���� �Ҹ�
    [SerializeField] private AudioClip landClip;

    // �뽬 �Ҹ�
    [SerializeField] private AudioClip dodgeClip;

    // ������ �Ҹ�
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
                    // ���� �и� ����
                    gameObject.GetComponent<EnemyAttackState_LastBoss>().OnRebound = true;
                    return;
                }
                else if (hit.GetComponent<Character_Defense>().IsDefense)
                {
                    Debug.Log("�÷��̾ �����");
                    hit.GetComponent<Character_Defense>().DefenseHit(nomalDMG);
                    return;
                }
                else
                {
                    if (hit.GetComponent<ObjectHealth>().Health > 0)
                    {
                        Debug.Log("�÷��̾ ����");
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
                    Debug.Log("�÷��̾ ����");
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
