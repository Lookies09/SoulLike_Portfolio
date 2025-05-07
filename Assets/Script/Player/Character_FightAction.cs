using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_FightAction : MonoBehaviour
{
    // ĳ���� ��Ʈ�ѷ�
    private CharacterController cc;

    // ������ ������Ʈ
    private Movement movement;

    // �ִϸ�����
    private Animator animator;

    // ���� ��� �ð�
    private float Atime;

    // �޹� ���� ��ȣ
    private int attack;

    // ���� Ÿ�� �߽��� ��ġ
    [SerializeField] private Transform atteckTransform;

    // ���� ��� ���̾�
    [SerializeField] private LayerMask targetLayer;

    // ���� ����
    [SerializeField] private float atteckRadius;

    // ���� ���� ����
    [SerializeField] private float hitAngle;

    // Į ����Ʈ ���� ��ġ
    [SerializeField] private Transform sowrdEffectPos;

    // �� ��Ʈ ����Ʈ
    [SerializeField] private GameObject hitEffect;

    // ���ݷ�
    [SerializeField] private int DMG;

    // ó�� ī�޶�
    [SerializeField] private GameObject excutionCam;

    // Į Ʈ����
    [SerializeField] private GameObject swordTrail;

    // ����� �ҽ�
    [SerializeField] private AudioSource audioSource;

    // ���� �����Ŭ��
    [SerializeField] private AudioClip attackClip;

    // Ư������ �����Ŭ��
    [SerializeField] private AudioClip spAttackClip;

    // ���� �� �����Ŭ��
    [SerializeField] private AudioClip affterAttackClip;

    // ���� ���� �Ǻ�
    private bool canAttack = true;

    // ó�� �� �ǰ� Ȯ��
    private bool canHitOnExecution = true;

    // Ư�� ���� �Ǻ�
    private bool onSpAt;

    public bool CanAttack { get => canAttack; set => canAttack = value; }
    public bool OnSpAt { get => onSpAt; set => onSpAt = value; }
    public bool CanHitOnExecution { get => canHitOnExecution; set => canHitOnExecution = value; }

    private bool onExecution;

    // ���� ó�� �̺�Ʈ
    private bool isBoss;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        Atime += Time.deltaTime;

        Attack();


    }


    private void Attack()
    {
        if (Input.GetMouseButton(0) && Input.GetMouseButton(1) && canAttack && movement.Grounded)
        {
            OnSpAt = true;

            // ������ ����
            StopMovement();
            return;
        }

        if (Input.GetMouseButtonDown(0) && canAttack && movement.Grounded && !OnSpAt)
        {
            animator.SetTrigger("Attack");

            gameObject.GetComponent<Character_Defense>().CanDefense = false;

            Collider[] enemys = Physics.OverlapSphere(atteckTransform.position, atteckRadius, targetLayer);

            foreach (Collider enemy in enemys)
            {
                // ó�� �̺�Ʈ
                if (enemy.GetComponent<ObjectHealth>().Health <= 0 || enemy.GetComponent<ObjectHealth>().Posture >= 100)
                {
                    if (enemy.GetComponent<ObjectHealth>().IsDead) { return; }
                    if (enemy.name == "Samurai_LastBoss")
                    {
                        excutionCam.SetActive(true);
                        isBoss = true;
                    }
                    else
                    {
                        isBoss = false;
                    }
                    onExecution = true;
                    canAttack = false;                    
                    
                    // �ǰ� �Ұ���
                    CanHitOnExecution = false;

                    // ������ ����
                    StopMovement();

                    // �ִϸ��̼� ���
                    animator.SetBool("Execution", true);
                    enemy.GetComponent<ObjectHealth>().Death();
                                        
                    return;                    
                }

            }


            if (attack == 0 && Atime >= 0.3f && onExecution  == false)
            {
                attack++;
                Atime = 0;

                // ������ ����
                StopMovement();


            }
            else if (attack == 1 && Atime >= 0.5f)
            {
                Atime = 0;
                attack++;
            }
            else if (attack == 2 && Atime >= 0.5f)
            {
                Atime = 0;
                attack++;
            }
            else if (attack == 3 && Atime >= 0.5f)
            {
                Atime = 0;
                attack++;
            }

        }
        if (attack > 0 && Atime >= 0.9f)
        {
            attack = 0;
            gameObject.GetComponent<Character_Defense>().CanDefense = true;

            // ������ Ǯ��
            PlayAllAction();
        }

        animator.SetInteger("AttackType", attack);
        animator.SetBool("SPAT", OnSpAt);
    }





    public void AttackHitAnimationEvent()
    {
        // �ִϸ��̼� �̺�Ʈ �� 1������ ���� �浹 üũ
        // * Physics.OverlapSphere(�浹üũ �߽���, �浹 üũ ����, ��� ���̾�)
        // - ����ĳ��Ʈ ó�� �ش� �޼ҵ尡 ����Ǵ� ���� ���� �����ȿ� �ִ� �浹 ������ ������
        Collider[] hits = Physics.OverlapSphere(atteckTransform.position, atteckRadius, targetLayer);

        // �ǰݵ� ���� �� ������ ���� �ȿ� �ִ� ����� Ÿ����
        foreach (Collider hit in hits)
        {
            if (!hit.tag.Equals("Enemy")) break;

            // �÷��̾ Ÿ���� ���� ���⺤�͸� ����
            Vector3 directionToTarget = hit.transform.position - transform.position;

            // Ÿ�� ������ �ü� ������ ����
            float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

            if (angleToTarget < hitAngle)
            {
                hit.GetComponent<ObjectHealth>().Hit(DMG);                
            }

        }

    }

    public void SpacialAttackAnimationEvent()
    {
        // �ִϸ��̼� �̺�Ʈ �� 1������ ���� �浹 üũ
        // * Physics.OverlapSphere(�浹üũ �߽���, �浹 üũ ����, ��� ���̾�)
        // - ����ĳ��Ʈ ó�� �ش� �޼ҵ尡 ����Ǵ� ���� ���� �����ȿ� �ִ� �浹 ������ ������
        Collider[] hits = Physics.OverlapSphere(atteckTransform.position, atteckRadius, targetLayer);

        // �ǰݵ� ���� �� ������ ���� �ȿ� �ִ� ����� Ÿ����
        foreach (Collider hit in hits)
        {
            if (!hit.tag.Equals("Enemy")) break;

            // �÷��̾ Ÿ���� ���� ���⺤�͸� ����
            Vector3 directionToTarget = hit.transform.position - transform.position;

            // Ÿ�� ������ �ü� ������ ����
            float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

            if (angleToTarget < hitAngle)
            {
                hit.GetComponent<ObjectHealth>().Hit(3);
            }

        }

    }

    public void HitEffectEvent()
    {
        Collider[] hits = Physics.OverlapSphere(atteckTransform.position, atteckRadius, targetLayer);

        foreach (Collider hit in hits)
        {
            if (!hit.tag.Equals("Enemy")) break;

            // �÷��̾ Ÿ���� ���� ���⺤�͸� ����
            Vector3 directionToTarget = hit.transform.position - transform.position;

            // Ÿ�� ������ �ü� ������ ����
            float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

            if (angleToTarget < hitAngle)
            {
                // ����� �ǰݵƴٸ�
                if (hit.GetComponent<ObjectHealth>().IsHit)
                {
                    // ����Ʈ ����
                    Instantiate(hitEffect, sowrdEffectPos.position, Quaternion.identity);
                }


            }

        }

    }


    public void SwordTrailEventOn()
    {
        swordTrail.SetActive(true);
    }

    public void SwordTrailEventEnd()
    {
        swordTrail.SetActive(false);
    }

    public void SwordSound()
    {
        audioSource.clip = attackClip;
        audioSource.volume = 0.3f;
        audioSource.pitch = 1f;
        audioSource.Play();
    }

    public void AfterSwordSound()
    {
        audioSource.clip = affterAttackClip;
        audioSource.volume = 0.2f;
        audioSource.pitch = 1f;
        audioSource.Play();
    }

    public void SPSwordSound()
    {
        audioSource.clip = spAttackClip;
        audioSource.volume = 0.2f;
        audioSource.Play();
    }

    public void StopMovement()
    {
        // ������ ����
        GetComponent<TargetFocus_Movement>().OnAttack = true;
        GetComponent<FreeCam_Movement>().OnAttack = true;
    }


    // ó�� �� ������ Ǯ���ִ� �̺�Ʈ
    public void AfterExcutionEvenet()
    {
        animator.SetBool("Execution", false);
        // �ٽ� �ǰ� ����
        canHitOnExecution = true;

        // ������ ���� ����
        GetComponent<TargetFocus_Movement>().OnAttack = false;
        GetComponent<FreeCam_Movement>().OnAttack = false;

        onExecution = false;
        canAttack = true;

        excutionCam.SetActive(false);
    }

    public void StopAllAction()
    {
        canAttack = false;
        gameObject.GetComponent<Character_Defense>().CanDefense = false;
        StopMovement();
    }

    public void PlayAllAction()
    {
        canAttack = true;
        gameObject.GetComponent<Character_Defense>().CanDefense = true;
        // ������ ���� ����
        GetComponent<TargetFocus_Movement>().OnAttack = false;
        GetComponent<FreeCam_Movement>().OnAttack = false;
        
    }

    public void AfterSpAttack() 
    {
        OnSpAt = false;

        PlayAllAction();
    }

    public void ExecutionTimeScaleCtrl(float time)
    {
        if(isBoss)
        {
            Time.timeScale = time;
        }        
    }

}
