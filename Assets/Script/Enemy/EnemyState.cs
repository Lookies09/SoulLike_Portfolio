using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyState : MonoBehaviour
{
    // ���� ���ѻ��±�� ��Ʈ�ѷ�
    protected EnemyController controller;

    // �ִϸ����� ������Ʈ
    protected Animator animator;

    // �׺���̼� ������Ʈ
    protected NavMeshAgent navMeshAgent;

    // ����� �ҽ�
    protected AudioSource audioSource;

    // ü�� ���� ����
    protected ObjectHealth objectHealth;

    public virtual void Awake()
    {
        controller = GetComponent<EnemyController>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        objectHealth = GetComponent<ObjectHealth>();
    }


    // ���� ���� ���� �������̽�(�����������̽��ƴ�) �޼ҵ� ����

    // ���� ���� ����(�ٸ����¿��� ���̵�) �޼ҵ�
    public abstract void EnterState(int state);

    // ���� ���� ������Ʈ �߻� �޼ҵ� (���� ���� ����)
    public abstract void UpdateState();

    // ���� ���� ����(�ٸ����·� ���̵�) �޼ҵ�
    public abstract void ExitState();

}
