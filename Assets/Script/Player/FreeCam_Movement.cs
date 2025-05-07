using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


// 카메라 회전에 따라 방향 전환
public class FreeCam_Movement : Movement
{
    // 프리룩 카메라
    [SerializeField] private GameObject FreeLook1;

    // 타겟 고정 카메라
    [SerializeField] private GameObject targetCam;

    // 오버랩 생성 위치
    [SerializeField] public Transform overlapTransform;

    // 오버랩 범위
    [SerializeField] private float overlapRadius;

    // 충돌 가능 레이어 설정
    [SerializeField] private LayerMask overlapLayer;

    // Update is called once per frame
    public override void Update()
    {
        if (!Grounded)
        {
            jTime += Time.deltaTime;
            animator.SetFloat("JumpTIme", jTime);
        }
        else
        {
            jTime = 0;
        }

        if (Input.GetMouseButtonDown(2))
        {
            
            Collider[] overlapColliders = Physics.OverlapSphere(overlapTransform.position, overlapRadius, overlapLayer);

            // 오버랩 충돌이 일어난 모든 게임오브젝트들 중에
            foreach (Collider collider in overlapColliders)
            {
                // 적군이 없으면 리턴
                if (collider.tag != "Enemy") break;
                
                    // gameObject.GetComponent<TargetFocus_Movement>().target = null;

                    // 있다면 타겟으로 설정
                    FreeLook1.SetActive(false);
                    targetCam.SetActive(true);

                    gameObject.GetComponent<TargetFocus_Movement>().enabled = true;
                    gameObject.GetComponent<TargetFocus_Movement>().target = collider.gameObject;
                    gameObject.GetComponent<FreeCam_Movement>().enabled = false;

            }
            return;
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (!onAttack)
        {
            Jump();
        }


        direction = new Vector3(h, 0f, v);
        direction = direction.normalized;


        // 카메라 y축의 y회전각도를 기준으로 캐릭터의 시선 방향벡터를 설정
        direction = Quaternion.AngleAxis(Camera.main.transform.rotation.eulerAngles.y, Vector3.up) * direction;
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
            // 현재 방향 기준의 쿼터니언을 반환함
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);            

            // 캐릭터를 회전함
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 720 * Time.deltaTime);
            // --------------------------
            // Debug.Log(transform.rotation.eulerAngles.y);

            RunDash();


            float dM = movemetSpeed.magnitude;

            // 캐릭터의 이동 애니메이션 파라미터값 설정
            animator.SetFloat("Move", dM);

            // 캐릭터의 이동 방향에 따른 애니메이션 파라미터값 설정
            animator.SetInteger("Direction", 0);

            


            // 캐릭터를 이동 처리
            cc.Move(movemetSpeed * Time.deltaTime);

            
        }
        else // 현재 캐릭터가 정지 중이라면
        {
            animator.SetFloat("Move", 0f);
            time = 0;

        }


        

        // 지면 접촉 확인
        animator.SetBool("IsGround", Grounded);

        GravityDown();

        base.Update();

    }

    private void OnDrawGizmosSelected()
    {
        Color color = new Color(0, 1, 0, 0.3f);
        Gizmos.color = color; // 기즈모 색상 : 초록



        // 와이어 원형 라인을 그려줌
        Gizmos.DrawSphere(overlapTransform.position, overlapRadius);



    }

    


}
