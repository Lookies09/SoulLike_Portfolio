    using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ���� forward�� ������ ������ �ִ� ������
public class TargetFocus_Movement : Movement
{
    // ������ ī�޶�
    [SerializeField] private GameObject FreeLook1;

    // Ÿ�� ���� ī�޶�
    [SerializeField] private GameObject targetCam;

    // Ÿ�� ��ġ
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


        // ī�޶� y���� yȸ�������� �������� ĳ������ �ü� ���⺤�͸� ����
        direction = Quaternion.LookRotation((target.transform.position - transform.position).normalized, transform.up) * direction;        
        direction.Normalize(); // ���⺤�� ����ȭ��




        // ������ ������ ����
        MovementSpeedCtrl(onAttack);



        if (character_Defense.IsDefense)
        {
            // ���� * ��� �ȱ� �ӵ�
            movemetSpeed = direction * defenseWalkSpeed;
        }
        else
        {
            // ���� * �ȱ� �ӵ�
            movemetSpeed = direction * walkSpeed;
        }

        // ���� ĳ���Ͱ� �̵� ���̸�
        if (direction.magnitude >= 0.1f)
        {
            time += Time.deltaTime;
            animator.SetFloat("Time", time);

            // --------------------------
            // ������ ������ ���������� ĳ���� ���� ������ ���ϱ�
            Vector3 relativeDirection = transform.InverseTransformDirection(direction);
            // ĳ������ ���� ȸ�� ���� ���͸� �̿��� �̵��ϰ� �ִ� ������ Ÿ�� ���ϱ�
            int movementType = (int)DetermineMovementType(relativeDirection);
            // --------------------------                   


            RunDash();


            float dM = movemetSpeed.magnitude;

            // ĳ������ �̵� �ִϸ��̼� �Ķ���Ͱ� ����
            animator.SetFloat("Move", dM);

            // ĳ������ �̵� ���⿡ ���� �ִϸ��̼� �Ķ���Ͱ� ����
            animator.SetInteger("Direction", movementType);

            // ĳ���͸� �̵� ó��
            cc.Move(movemetSpeed * Time.deltaTime);


        }
        else // ���� ĳ���Ͱ� ���� ���̶��
        {
            animator.SetFloat("Move", 0f);
            time = 0;
        }

        DIRECTION_TYPE DetermineMovementType(Vector3 relativeDirection)
        {
            // ���� ���� ȸ�� ���� ���͸� ������ �������� ȸ�� ������ ���ϱ� (��ũź��Ʈ)
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

        // ���� ���� Ȯ��
        animator.SetBool("IsGround", Grounded);


        GravityDown();

        base.Update();
    }


   

}
