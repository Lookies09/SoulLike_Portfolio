using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Defense : MonoBehaviour
{
    // ĳ���� ��Ʈ�ѷ�
    private CharacterController cc;

    // ĳ���� ���� ������Ʈ
    private Character_FightAction character_FightAction;

    // �ִϸ�����
    private Animator animator;

    // ��� �Ǻ�
    private bool isDefense;

    // ��� �ִϸ��̼� ����
    private bool onDefense;

    // ��� �ð�
    private float dTime;

    // ��� ���� �Ǻ�
    private bool canDefense = true;


    // ��� ����Ʈ
    [SerializeField] private GameObject defenseEffect;

    // ��� ����Ʈ ��ġ
    [SerializeField] private Transform defenseEffectPos;

    // �и� Ÿ�� �߽��� ��ġ
    [SerializeField] private Transform parryTransform;

    // �и� ��� ���̾�
    [SerializeField] private LayerMask targetLayer;

    // �и� ����
    [SerializeField] private float parryRadius;

    // �и� ����Ʈ
    [SerializeField] private GameObject parryEffect;

    // ��� ���� ����� �ҽ�
    [SerializeField] private AudioSource poseAudioSource;

    // ��� �����Ŭ��
    [SerializeField] private AudioClip defenseClip;

    // �÷��̾� ü�� ����
     protected PlayerHealth playerHealth;

    public bool IsDefense { get => isDefense; set => isDefense = value; }
    public bool OnParry { get => onParry; set => onParry = value; }
    public bool ArrowRayHit { get => arrowRayHit; set => arrowRayHit = value; }
    public bool AfterParry { get => afterParry; set => afterParry = value; }
    public bool CanDefense { get => canDefense; set => canDefense = value; }

    // �и� Ȯ��
    private bool onParry = false;

    // ȭ�� ���� �浹 Ȯ��
    private bool arrowRayHit;

    // ȭ�� ���� Ȯ��
    private bool afterParry;



    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
        character_FightAction = GetComponent<Character_FightAction>();
    }

    private void Update()
    {        
        Defense();
        
    }
    
    private void Defense()
    {
        if (Input.GetMouseButton(1) && canDefense)
        {
            animator.SetTrigger("Defense");
            if (Input.GetMouseButtonDown(1))
            {
                
                character_FightAction.AfterSpAttack();
            }

            // ��� �ִϸ��̼� ����
            dTime += Time.deltaTime;
            onDefense = true;
        }
        else
        {
            // ��� ����
            isDefense = false;
            // ��� �ִϸ��̼� ����
            onDefense = false;

            dTime = 0;

        }

        animator.SetBool("IsDefense", onDefense);
        animator.SetFloat("DTime", dTime);
    }

    public void DefenseHit(int DMG)
    {

        Instantiate(defenseEffect, defenseEffectPos.position, Quaternion.identity);
        animator.SetTrigger("DefenseHit");
        arrowRayHit = false;
        // ü�� ����
        playerHealth.Posture += DMG*75/100;

    }

    public void Parry()
    {        

        if (arrowRayHit)
        {
            Debug.Log("ȭ�� �и�");
            animator.SetTrigger("Parry");            
            Instantiate(parryEffect, defenseEffectPos.position, defenseEffectPos.rotation);
            afterParry = true;
        }

        Collider[] hits = Physics.OverlapSphere(parryTransform.position, parryRadius, targetLayer);
        
        foreach (Collider hit in hits)
        {
            // ����Ȱ� �����̸�
            if ((hit.tag == "Enemy"))
            {
                // �и�ó��
                if (hit.GetComponent<EnemyAttackAbleState>().IsParry)
                {
                    animator.SetTrigger("Parry");                    
                    Instantiate(parryEffect, defenseEffectPos.position, defenseEffectPos.rotation);
                    OnParry = true;
                }

            }
            // �ƴϸ� 
            else
            {
                break;
            }
        }
    }
    public void DefenseOn()
    {
        isDefense = true;
    }

    public void DefenseOff()
    {
        isDefense = false;
    }


    public void ParryEnd()
    {
        OnParry = false;
        afterParry = false;
        arrowRayHit = false;
    }



    // ��������Ҷ� ��Ĭ �Ҹ�
    public void DefensePoseSound()
    {
        poseAudioSource.clip = defenseClip;        
        poseAudioSource.pitch = 0.8f;
        poseAudioSource.Play();
    }
}
