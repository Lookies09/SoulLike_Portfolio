using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CartoonFX;

public class JUMP_AT_LastBoss : AttackPatternState
{
    private int RanSpAT;
    [SerializeField] private ParticleSystem groundHitEffect;

    public override void EnterState(int state)
    {
        // ���� ���� �ִϸ��̼� ���
        animator.SetInteger("AttackPattern", state);


        RanSpAT = Random.Range(0, 2);

        animator.SetInteger("JumpAT_Ran_SP", RanSpAT);
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
