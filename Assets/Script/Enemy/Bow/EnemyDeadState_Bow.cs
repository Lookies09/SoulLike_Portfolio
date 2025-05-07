using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState_Bow : EnemyState
{
    // 사망 완료 처리 시간
    protected float time;
    [SerializeField] protected float deathDelayTime;

    // 사망 처리 이펙트
    [SerializeField] protected ParticleSystem bodyBloodParticlePrefab;

    [SerializeField] protected ParticleSystem neckBloodParticlePrefab2;

    // 목소리 오디오 소스
    [SerializeField] private AudioSource voiceAudioSource;

    // 히트 오디오 소스
    [SerializeField] private AudioSource hitAudioSource;

    // 죽는 목소리1
    [SerializeField] private AudioClip deadVoice1;

    // 피 튀는 소리
    [SerializeField] private AudioClip bloodSound;

    public override void EnterState(int state)
    {
        // 이동 중지
        navMeshAgent.isStopped = true;

        // 사망 애니메이션 재생
        animator.SetBool("Dead", true);
    }    

    public override void UpdateState()
    {
        time += Time.deltaTime;

        // 사망 처리 지연시간이 지났다면
        if (time >= deathDelayTime)
        {
            // 몬스터가 소멸됨

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
            // 출혈 소리
            hitAudioSource.clip = bloodSound;
            hitAudioSource.volume = 0.5f;
            hitAudioSource.Play();
        }
        else
        {
            neckBloodParticlePrefab2.Play();
            // 출혈 소리
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
