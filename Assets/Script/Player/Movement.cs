using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    protected enum DIRECTION_TYPE { FORWARD, BACKWARD, LEFT, RIGHT };

    // 캐릭터 걷기 속도
    [SerializeField] protected float walkSpeed;

    // 캐릭터 방어 걷기 속도
    [SerializeField] protected float defenseWalkSpeed;

    // 캐릭터 달리기 속도
    [SerializeField] protected float runSpeed;

    // 캐릭터 점프 속도
    [SerializeField] protected float jumpSpeed;

    // 움직임 오디오 소스
    [SerializeField] protected AudioSource audioSource;

    // 걷기 오디오 클립
    [SerializeField] private AudioClip walkClip;

    // 달리기 오디오 클립
    [SerializeField] private AudioClip runClip;

    // 점프 시작 오디오 클립
    [SerializeField] private AudioClip jumpStartClip;

    // 점프 지면 착지 오디오 클립
    [SerializeField] private AudioClip landingClip;


    // 캐릭터 이동 방향
    protected Vector3 direction;

    // 캐릭터 컨트롤러
    protected CharacterController cc;

    // 애니메이터
    protected Animator animator;

    // 중력값
    [SerializeField] protected float gravity;

    // 캐릭터 방어 컴포넌트
    protected Character_Defense character_Defense;

    // 캐릭터 체력 컴포넌트
    protected PlayerHealth playerHealth;

    // 움직임 시간
    protected float time;

    // 점프 시간
    protected float jTime;

    // 지면 착지 여부
    protected bool grounded = false;
    protected float groundedTimer; // 지면 체크 타이머
    protected float vSpeed = 0.0f; // 수직 이동 속도

    protected Vector3 movemetSpeed;

    // 공격중 이동 금지 여부
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

    // 걷기 이하일때 체간 감소 메소드
    public void PostureDown()
    {       
        // 이동 속도가 걷는속도 이하라면
        if (movemetSpeed.magnitude <= 4 && playerHealth.Posture > 0 && !onAttack && Grounded)
        {
            // 현재 체력이 절반 이상이라면
            if (playerHealth.Health >= playerHealth.StartHp / 2)
            {
                // 10씩 감소
                playerHealth.Posture -= 10 * Time.deltaTime;                
            }
            // 절반 미만이라면
            else
            {
                // 2씩 감소
                playerHealth.Posture -= 2 * Time.deltaTime;
            }
        }
    }


    protected void Jump()
    {
        // Character Controller 는 착지 인식하지만
        if (!cc.isGrounded)
        {
            // 착지 상태값이 아닐 경우
            if (Grounded)
            {
                // 착지 상태 시간을 계산함
                groundedTimer += Time.deltaTime;                

                // 착지 시간이 0.5초 이상 지났다면
                if (groundedTimer >= 0.5f)
                {
                    Grounded = false;
                }
            }
        }
        // Character Controller가 착지 상태면
        else
        {            
            // 착지 상태 시간을 초기화 함
            groundedTimer = 0.0f;
            // 착지 상태로 상태값을 변경함
            Grounded = true;
        }

        // 착지 상태에서 점프 키를 눌렀다면
        if (Grounded && Input.GetButtonDown("Jump") )
        {            
            animator.SetTrigger("Jump");

            // 점프 속도를 수직 이동 속도에 적용함
            vSpeed = jumpSpeed;

            // 착지 상태가 아님으로 설정함 (떠있음)
            Grounded = false;
        }
    }

    protected void GravityDown()
    {
        // 수직 중력을 적용함 (-10)
        vSpeed = vSpeed - gravity * Time.deltaTime;

        // 하강속도가 -10 보다 작아지면
        if (vSpeed < -gravity)
            vSpeed = -gravity; // 최대 하강속도를 -10으로 설정함

        // 중력 수직 하강 속도가 적용된 수직 이동 벡터를 설정함
        var verticalMove = new Vector3(0, vSpeed * Time.deltaTime, 0);

        // 중력값이 적용된 수직 하강 이동을 처리함
        var flag = cc.Move(verticalMove);

        // 캐릭터 컨트롤러가 지면에 닿았다면
        if ((flag & CollisionFlags.Below) != 0)
        {
            // 수직 하강 속도를 0으로 설정함
            vSpeed = 0;
        }
    }

    protected void RunDash()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Grounded)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                // 한번 클릭 시 대쉬 구현
                animator.SetTrigger("Slide");
            }
            

            // 꾹 누르면 달리기
            animator.SetBool("IsRun", true);

            // 방향 * 달리기 속도
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
