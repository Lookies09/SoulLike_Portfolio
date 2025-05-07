using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    // ȭ�� �ӵ�
    [SerializeField] private float speed;

    // ȭ�� ����
    [SerializeField] public Vector3 direction = Vector3.zero;

    public float Speed { get => speed; set => speed = value; }

    private void Update()
    {
        transform.Translate(transform.forward * Speed * Time.deltaTime, Space.World);
    }



}
