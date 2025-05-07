using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

// ��(Ȱ) ��Ʈ�ѷ�
public class EnemyFSMController_Bow : EnemyController
{
    // ��(Ȱ) ���µ�
    public enum STATE { IDLE, WANDER, ATTACK, GIVEUP, HIT, DEATH }

    // ���� �״��ϴ� ȭ��
    [SerializeField] private GameObject arrowOnHand;

    // ���� �߻�Ǵ� ȭ�� ������
    [SerializeField] private GameObject arrowprefab;

    private Vector3 direction;

    // �߻� ��ġ
    [SerializeField]  private Transform shootPos;

    // ȭ�� Ÿ��
    [SerializeField] private Transform Target;

    // Ȱ ���� ���� ����� Ŭ��
    [SerializeField] private AudioClip bowString;

    // Ȱ ��� ����� Ŭ��
    [SerializeField] private AudioClip bowShoot;

    [SerializeField] private AudioSource audioSource;

    // ������ ����� �ҽ�
    [SerializeField] private AudioSource movementAudio;

    // �ȴ� �Ҹ�
    [SerializeField] private AudioClip walkClip;



    // ȭ�� �̴� �ִϸ��̼� �̺�Ʈ
    public void ArrowPickup()
    {
        Debug.Log("ȭ�����");
        arrowOnHand.SetActive(true);
    }

    // Ȱ ���� ���� �ִϸ��̼� �̺�Ʈ
    public void BowStringEvent()
    {
        audioSource.clip = bowString;
        audioSource.Play();
    }


    // ȭ�� �߻��ϴ� �ִϸ��̼� �̺�Ʈ
    public void ShootArrow()
    {
        direction = (Target.position - shootPos.position).normalized;
        //direction.y = 0;

        Debug.Log("ȭ��߻�");
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
