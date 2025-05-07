using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CartoonFX;

public class JUMP_AT_Pattern : AttackPatternState
{
    [SerializeField] private ParticleSystem groundHitEffect;

    public override void EnterState(int state)
    {
        // 점프공격 애니메이션 재생
        animator.SetInteger("AttackPattern", state);
        
    }


    public override void ExitState()
    {
        
    }


    public void JumpAttackEffectEvent()
    {       
        groundHitEffect.GetComponentInChildren<CFXR_Effect>().ResetState();
        groundHitEffect.Play();
        groundHitEffect.GetComponent<AudioSource>().Play();
    }

}
