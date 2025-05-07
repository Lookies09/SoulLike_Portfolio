using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState_Bow : EnemyState
{
    // ��� �Ϸ� ó�� �ð�
    protected float time;
    [SerializeField] protected float deathDelayTime;

    // ��� ó�� ����Ʈ
    [SerializeField] protected ParticleSystem bodyBloodParticlePrefab;

    [SerializeField] protected ParticleSystem neckBloodParticlePrefab2;

    // ��Ҹ� ����� �ҽ�
    [SerializeField] private AudioSource voiceAudioSource;

    // ��Ʈ ����� �ҽ�
    [SerializeField] private AudioSource hitAudioSource;

    // �״� ��Ҹ�1
    [SerializeField] private AudioClip deadVoice1;

    // �� Ƣ�� �Ҹ�
    [SerializeField] private AudioClip bloodSound;

    public override void EnterState(int state)
    {
        // �̵� ����
        navMeshAgent.isStopped = true;

        // ��� �ִϸ��̼� ���
        animator.SetBool("Dead", true);
    }    

    public override void UpdateState()
    {
        time += Time.deltaTime;

        // ��� ó�� �����ð��� �����ٸ�
        if (time >= deathDelayTime)
        {
            // ���Ͱ� �Ҹ��

            Destroy(gameObject);
        }
    }
    public override void ExitState()
    {

    }

    public void ExecutionEvent(int a)
    {
        if (a == 0)
        {
            bodyBloodParticlePrefab.Play();
            // ���� �Ҹ�
            hitAudioSource.clip = bloodSound;
            hitAudioSource.volume = 0.5f;
            hitAudioSource.Play();
        }
        else
        {
            neckBloodParticlePrefab2.Play();
            // ���� �Ҹ�
            hitAudioSource.clip = bloodSound;
            hitAudioSource.volume = 0.5f;
            hitAudioSource.Play();
        }
    }

    public void DVoice1()
    {
        voiceAudioSource.clip = deadVoice1;
        voiceAudioSource.Play();
    }
}
