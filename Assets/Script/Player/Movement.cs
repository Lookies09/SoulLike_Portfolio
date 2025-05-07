using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    protected enum DIRECTION_TYPE { FORWARD, BACKWARD, LEFT, RIGHT };

    // ĳ���� �ȱ� �ӵ�
    [SerializeField] protected float walkSpeed;

    // ĳ���� ��� �ȱ� �ӵ�
    [SerializeField] protected float defenseWalkSpeed;

    // ĳ���� �޸��� �ӵ�
    [SerializeField] protected float runSpeed;

    // ĳ���� ���� �ӵ�
    [SerializeField] protected float jumpSpeed;

    // ������ ����� �ҽ�
    [SerializeField] protected AudioSource audioSource;

    // �ȱ� ����� Ŭ��
    [SerializeField] private AudioClip walkClip;

    // �޸��� ����� Ŭ��
    [SerializeField] private AudioClip runClip;

    // ���� ���� ����� Ŭ��
    [SerializeField] private AudioClip jumpStartClip;

    // ���� ���� ���� ����� Ŭ��
    [SerializeField] private AudioClip landingClip;


    // ĳ���� �̵� ����
    protected Vector3 direction;

    // ĳ���� ��Ʈ�ѷ�
    protected CharacterController cc;

    // �ִϸ�����
    protected Animator animator;

    // �߷°�
    [SerializeField] protected float gravity;

    // ĳ���� ��� ������Ʈ
    protected Character_Defense character_Defense;

    // ĳ���� ü�� ������Ʈ
    protected PlayerHealth playerHealth;

    // ������ �ð�
    protected float time;

    // ���� �ð�
    protected float jTime;

    // ���� ���� ����
    protected bool grounded = false;
    protected float groundedTimer; // ���� üũ Ÿ�̸�
    protected float vSpeed = 0.0f; // ���� �̵� �ӵ�

    protected Vector3 movemetSpeed;

    // ������ �̵� ���� ����
    protected bool onAttack;

    public bool OnAttack { get => onAttack; set => onAttack = value; }
    public bool Grounded { get => grounded; set => grounded = value; }

    protected void Awake()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        character_Defense = GetComponent<Character_Defense>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    public virtual void Update()
    {
        PostureDown();
    }

    // �ȱ� �����϶� ü�� ���� �޼ҵ�
    public void PostureDown()
    {       
        // �̵� �ӵ��� �ȴ¼ӵ� ���϶��
        if (movemetSpeed.magnitude <= 4 && playerHealth.Posture > 0 && !onAttack && Grounded)
        {
            // ���� ü���� ���� �̻��̶��
            if (playerHealth.Health >= playerHealth.StartHp / 2)
            {
                // 10�� ����
                playerHealth.Posture -= 10 * Time.deltaTime;                
            }
            // ���� �̸��̶��
            else
            {
                // 2�� ����
                playerHealth.Posture -= 2 * Time.deltaTime;
            }
        }
    }


    protected void Jump()
    {
        // Character Controller �� ���� �ν�������
        if (!cc.isGrounded)
        {
            // ���� ���°��� �ƴ� ���
            if (Grounded)
            {
                // ���� ���� �ð��� �����
                groundedTimer += Time.deltaTime;                

                // ���� �ð��� 0.5�� �̻� �����ٸ�
                if (groundedTimer >= 0.5f)
                {
                    Grounded = false;
                }
            }
        }
        // Character Controller�� ���� ���¸�
        else
        {            
            // ���� ���� �ð��� �ʱ�ȭ ��
            groundedTimer = 0.0f;
            // ���� ���·� ���°��� ������
            Grounded = true;
        }

        // ���� ���¿��� ���� Ű�� �����ٸ�
        if (Grounded && Input.GetButtonDown("Jump") )
        {            
            animator.SetTrigger("Jump");

            // ���� �ӵ��� ���� �̵� �ӵ��� ������
            vSpeed = jumpSpeed;

            // ���� ���°� �ƴ����� ������ (������)
            Grounded = false;
        }
    }

    protected void GravityDown()
    {
        // ���� �߷��� ������ (-10)
        vSpeed = vSpeed - gravity * Time.deltaTime;

        // �ϰ��ӵ��� -10 ���� �۾�����
        if (vSpeed < -gravity)
            vSpeed = -gravity; // �ִ� �ϰ��ӵ��� -10���� ������

        // �߷� ���� �ϰ� �ӵ��� ����� ���� �̵� ���͸� ������
        var verticalMove = new Vector3(0, vSpeed * Time.deltaTime, 0);

        // �߷°��� ����� ���� �ϰ� �̵��� ó����
        var flag = cc.Move(verticalMove);

        // ĳ���� ��Ʈ�ѷ��� ���鿡 ��Ҵٸ�
        if ((flag & CollisionFlags.Below) != 0)
        {
            // ���� �ϰ� �ӵ��� 0���� ������
            vSpeed = 0;
        }
    }

    protected void RunDash()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Grounded)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                // �ѹ� Ŭ�� �� �뽬 ����
                animator.SetTrigger("Slide");
            }
            

            // �� ������ �޸���
            animator.SetBool("IsRun", true);

            // ���� * �޸��� �ӵ�
            movemetSpeed = direction * runSpeed;

        }
        else
        {
            animator.SetBool("IsRun", false);
        }
    }


    public void MovementSpeedCtrl(bool onAttack)
    {
        this.onAttack = onAttack;

        if (onAttack)
        {
            direction = Vector3.zero;
        }
    }

    public void WalkSound()
    {
        audioSource.clip = walkClip;
        audioSource.Play();
    }

    public void RunSound()
    {
        audioSource.clip = runClip;
        audioSource.Play();
    }

    public void JumpSound()
    {
        audioSource.clip = jumpStartClip;
        audioSource.Play();
    }

    public void LandSound()
    {
        audioSource.clip = landingClip;
        audioSource.Play();
    }

    public void JumpCheckOn()
    {
        animator.SetBool("JumpCheck", true);
    }

    public void JumpCheckOff()
    {
        animator.SetBool("JumpCheck", false);
    }
}
