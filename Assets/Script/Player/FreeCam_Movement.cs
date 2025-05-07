using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


// ī�޶� ȸ���� ���� ���� ��ȯ
public class FreeCam_Movement : Movement
{
    // ������ ī�޶�
    [SerializeField] private GameObject FreeLook1;

    // Ÿ�� ���� ī�޶�
    [SerializeField] private GameObject targetCam;

    // ������ ���� ��ġ
    [SerializeField] public Transform overlapTransform;

    // ������ ����
    [SerializeField] private float overlapRadius;

    // �浹 ���� ���̾� ����
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

            // ������ �浹�� �Ͼ ��� ���ӿ�����Ʈ�� �߿�
            foreach (Collider collider in overlapColliders)
            {
                // ������ ������ ����
                if (collider.tag != "Enemy") break;
                
                    // gameObject.GetComponent<TargetFocus_Movement>().target = null;

                    // �ִٸ� Ÿ������ ����
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


        // ī�޶� y���� yȸ�������� �������� ĳ������ �ü� ���⺤�͸� ����
        direction = Quaternion.AngleAxis(Camera.main.transform.rotation.eulerAngles.y, Vector3.up) * direction;
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
            // ���� ���� ������ ���ʹϾ��� ��ȯ��
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);            

            // ĳ���͸� ȸ����
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 720 * Time.deltaTime);
            // --------------------------
            // Debug.Log(transform.rotation.eulerAngles.y);

            RunDash();


            float dM = movemetSpeed.magnitude;

            // ĳ������ �̵� �ִϸ��̼� �Ķ���Ͱ� ����
            animator.SetFloat("Move", dM);

            // ĳ������ �̵� ���⿡ ���� �ִϸ��̼� �Ķ���Ͱ� ����
            animator.SetInteger("Direction", 0);

            


            // ĳ���͸� �̵� ó��
            cc.Move(movemetSpeed * Time.deltaTime);

            
        }
        else // ���� ĳ���Ͱ� ���� ���̶��
        {
            animator.SetFloat("Move", 0f);
            time = 0;

        }


        

        // ���� ���� Ȯ��
        animator.SetBool("IsGround", Grounded);

        GravityDown();

        base.Update();

    }

    private void OnDrawGizmosSelected()
    {
        Color color = new Color(0, 1, 0, 0.3f);
        Gizmos.color = color; // ����� ���� : �ʷ�



        // ���̾� ���� ������ �׷���
        Gizmos.DrawSphere(overlapTransform.position, overlapRadius);



    }

    


}
