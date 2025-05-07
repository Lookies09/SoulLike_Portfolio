using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CartoonFX;

public class ThunderController : MonoBehaviour
{
    // 번개 이펙트 참조
    [SerializeField] private ParticleSystem[] lightingEffect;

    // 오디오 소스
    [SerializeField] private AudioSource audioSource;
        
    // 번개 딜레이 시간
    [SerializeField] private Vector2 delatTime;


    private void Start()
    {
        StartCoroutine("LightingCoroutine");
    }

    IEnumerator LightingCoroutine()
    {
        while (true)
        {
            float ranTime = Random.Range(delatTime.x, delatTime.y);
            yield return new WaitForSeconds(ranTime);
            int ran = Random.Range(0, lightingEffect.Length);


            lightingEffect[ran].Play();
            lightingEffect[ran].GetComponentInChildren<CFXR_Effect>().ResetState();

            audioSource.Play();
        }
    }

}
