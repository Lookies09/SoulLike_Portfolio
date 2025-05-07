using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHit : MonoBehaviour
{
    // ȭ�� ������
    [SerializeField] private int nomalDMG;

    // ��Ʈ ����Ʈ
    [SerializeField] protected GameObject hitEffect;

    // ȭ�� ������
    [SerializeField] ArrowMovement movement;

    // ȭ�� �����ٵ�
    [SerializeField] Rigidbody rigid;

    // ȭ�� �浹 ����
    private bool isParry;

    // ����ĳ��Ʈ ����
    private RaycastHit hit;

    // ����ĳ��Ʈ ����
    [SerializeField] private float maxRayLength;

    // ����ĳ��Ʈ ���� ��ġ
    [SerializeField] private Transform raycastStartpos;

    // ����ĳ��Ʈ �浹 ���̾�
    [SerializeField] private LayerMask layerMask;

    // ȯ�� �浹 ���̾�
    [SerializeField] private LayerMask enviroLayerMask;

    // �浹 ���ӿ�����Ʈ
    private GameObject hitGameobject;

    public bool IsParry { get => isParry; set => isParry = value; }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enviroment")
        {

            Destroy(gameObject);

            ContactPoint contact = collision.contacts[0];
            Vector3 pos = contact.point;

            Instantiate(hitEffect, pos, Quaternion.identity);
            return;
        }

        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Character_Defense>().IsDefense)
            {
                Debug.Log("�÷��̾ �����");
                collision.gameObject.GetComponent<Character_Defense>().DefenseHit(nomalDMG);
                isParry = false;
                Destroy(gameObject);

                /*
                movement.Speed = 0f;
                rigid.useGravity = true;
                */
                return;

            }
            else
            {
                if (collision.gameObject.GetComponent<ObjectHealth>().Health > 0)
                {
                    Debug.Log("�÷��̾ ����");
                    ContactPoint contact = collision.contacts[0];
                    Vector3 pos = contact.point;

                    Instantiate(hitEffect, pos, Quaternion.identity);

                    collision.gameObject.GetComponent<ObjectHealth>().Hit(nomalDMG);
                    Destroy(gameObject);
                    return;
                }                
            }
        }
    }
    private void Update()
    {
        bool hit1 = Physics.Raycast(
             raycastStartpos.position, // ����ĳ��Ʈ ������ġ 
             raycastStartpos.forward, // ����ĳ��Ʈ ���� ����
             out hit, // ����ĳ��Ʈ �浹 ��ġ
             maxRayLength+3, // ����ĳ��Ʈ ����
             enviroLayerMask // ����ĳ��Ʈ �浹 ���̾�
             );                

        if (!hit1)
        {
            isParry = Physics.Raycast(
              raycastStartpos.position, // ����ĳ��Ʈ ������ġ 
              raycastStartpos.forward, // ����ĳ��Ʈ ���� ����
              out hit, // ����ĳ��Ʈ �浹 ��ġ
              maxRayLength, // ����ĳ��Ʈ ����
              layerMask // ����ĳ��Ʈ �浹 ���̾�
              );
        }

        Debug.DrawRay(raycastStartpos.position, raycastStartpos.forward * maxRayLength, Color.red);

        // �÷��̾��� ȭ�� ���� �⵹ ���θ� �־���
        GameObject.Find("Player").GetComponent<Character_Defense>().ArrowRayHit = isParry;

        if (GameObject.Find("Player").GetComponent<Character_Defense>().AfterParry)
        {
            Destroy(gameObject);
        }
    }

    
    

}
