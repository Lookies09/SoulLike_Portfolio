using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSMController : EnemyController
{
    // ��(Į) ���µ�
    public enum STATE { IDLE, WANDER, DETECT, ATTACK, GIVEUP,  HIT, DEATH , DEFENSE, REBOUND }

    //==============================================
    
    // ���� ����
    [SerializeField] private float atteckRadius;

    // ���� ���� ����
    [SerializeField] private float hitAngle;

    // ���� Ÿ�� �߽��� ��ġ
    [SerializeField] private Transform atteckTransform;

    // ���� ��� ���̾�
    [SerializeField] private LayerMask targetLayer;

    // �⺻���� ������
    [SerializeField] private int nomalDMG;

    // ������ ������
    [SerializeField] private int specialDMG;

    [SerializeField] private AudioSource katanaAudio;

    // Į ���� ����� Ŭ��
    [SerializeField] private AudioClip swingSound;

    // ������ ����� �ҽ�
    [SerializeField] private AudioSource movementAudio;
       

    // �ȴ� �Ҹ�
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
                    // ���� �и� ����
                    gameObject.GetComponent<EnemyAttackState>().OnRebound = true;
                    return;
                }
                else if (hit.GetComponent<Character_Defense>().IsDefense)
                {
                    // Debug.Log("�÷��̾ �����");
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
