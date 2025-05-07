    using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 월드 forward로 방향이 맞춰져 있는 움직임
public class TargetFocus_Movement : Movement
{
    // 프리룩 카메라
    [SerializeField] private GameObject FreeLook1;

    // 타겟 고정 카메라
    [SerializeField] private GameObject targetCam;

    // 타겟 위치
    [SerializeField] public GameObject target;



    // Update is called once per frame
    public override void Update()
    {

        if (Input.GetMouseButtonDown(2) || target == null)
        {
            FreeLook1.SetActive(true);

            targetCam.SetActive(false);

            gameObject.GetComponent<FreeCam_Movement>().enabled = true;
            
            gameObject.GetComponent<TargetFocus_Movement>().enabled = false;

            return;
        }
        

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (!onAttack)
        {
            Jump();
        }
        


        direction = new Vector3(h, 0f ,v);
        direction = direction.normalized;

        

        // --------------------------

        Quaternion rotate = Quaternion.LookRotation((target.transform.position - transform.position), Vector3.up);
        rotate.x = 0;
        rotate.z = 0;
        transform.rotation = rotate;
        // --------------------------


        // 카메라 y축의 y회전각도를 기준으로 캐릭터의 시선 방향벡터를 설정
        direction = Quaternion.LookRotation((target.transform.position - transform.position).normalized, transform.up) * direction;        
        direction.Normalize(); // 방향벡터 정규화함




        // 공격중 움직임 조절
        MovementSpeedCtrl(onAttack);



        if (character_Defense.IsDefense)
        {
            // 방향 * 방어 걷기 속도
            movemetSpeed = direction * defenseWalkSpeed;
        }
        else
        {
            // 방향 * 걷기 속도
            movemetSpeed = direction * walkSpeed;
        }

        // 현재 캐릭터가 이동 중이면
        if (direction.magnitude >= 0.1f)
        {
            time += Time.deltaTime;
            animator.SetFloat("Time", time);

            // --------------------------
            // 월드축 방향을 기준으로한 캐릭터 로컬 방향축 구하기
            Vector3 relativeDirection = transform.InverseTransformDirection(direction);
            // 캐릭터의 로컬 회전 방향 벡터를 이용해 이동하고 있는 방향의 타입 구하기
            int movementType = (int)DetermineMovementType(relativeDirection);
            // --------------------------                   


            RunDash();


            float dM = movemetSpeed.magnitude;

            // 캐릭터의 이동 애니메이션 파라미터값 설정
            animator.SetFloat("Move", dM);

            // 캐릭터의 이동 방향에 따른 애니메이션 파라미터값 설정
            animator.SetInteger("Direction", movementType);

            // 캐릭터를 이동 처리
            cc.Move(movemetSpeed * Time.deltaTime);


        }
        else // 현재 캐릭터가 정지 중이라면
        {
            animator.SetFloat("Move", 0f);
            time = 0;
        }

        DIRECTION_TYPE DetermineMovementType(Vector3 relativeDirection)
        {
            // 현재 로컬 회전 방향 벡터를 가지고 실질적인 회전 각도를 구하기 (아크탄젠트)
            float angle = Mathf.Atan2(relativeDirection.x, relativeDirection.z) * Mathf.Rad2Deg;

            if (angle < -120 || angle > 120)
            {
                return DIRECTION_TYPE.BACKWARD;
            }
            else if (angle > -60 && angle < 60)
            {
                return DIRECTION_TYPE.FORWARD;
            }
            else
            {
                return angle > 0 ? DIRECTION_TYPE.RIGHT : DIRECTION_TYPE.LEFT;
            }
        }

        // 지면 접촉 확인
        animator.SetBool("IsGround", Grounded);


        GravityDown();

        base.Update();
    }


   

}
