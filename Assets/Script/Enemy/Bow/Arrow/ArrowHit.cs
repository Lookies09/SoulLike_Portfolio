using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHit : MonoBehaviour
{
    // 화살 데미지
    [SerializeField] private int nomalDMG;

    // 히트 이펙트
    [SerializeField] protected GameObject hitEffect;

    // 화살 움직임
    [SerializeField] ArrowMovement movement;

    // 화살 리짓바디
    [SerializeField] Rigidbody rigid;

    // 화살 충돌 여부
    private bool isParry;

    // 레이캐스트 정보
    private RaycastHit hit;

    // 레이캐스트 길이
    [SerializeField] private float maxRayLength;

    // 레이캐스트 시작 위치
    [SerializeField] private Transform raycastStartpos;

    // 레이캐스트 충돌 레이어
    [SerializeField] private LayerMask layerMask;

    // 환경 충돌 레이어
    [SerializeField] private LayerMask enviroLayerMask;

    // 충돌 게임오브젝트
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
                Debug.Log("플레이어가 방어함");
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
                    Debug.Log("플레이어가 맞음");
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
             raycastStartpos.position, // 레이캐스트 시작위치 
             raycastStartpos.forward, // 레이캐스트 생성 방향
             out hit, // 레이캐스트 충돌 위치
             maxRayLength+3, // 레이캐스트 길이
             enviroLayerMask // 레이캐스트 충돌 레이어
             );                

        if (!hit1)
        {
            isParry = Physics.Raycast(
              raycastStartpos.position, // 레이캐스트 시작위치 
              raycastStartpos.forward, // 레이캐스트 생성 방향
              out hit, // 레이캐스트 충돌 위치
              maxRayLength, // 레이캐스트 길이
              layerMask // 레이캐스트 충돌 레이어
              );
        }

        Debug.DrawRay(raycastStartpos.position, raycastStartpos.forward * maxRayLength, Color.red);

        // 플레이어의 화살 레이 출돌 여부를 넣어줌
        GameObject.Find("Player").GetComponent<Character_Defense>().ArrowRayHit = isParry;

        if (GameObject.Find("Player").GetComponent<Character_Defense>().AfterParry)
        {
            Destroy(gameObject);
        }
    }

    
    

}
