using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CartoonFX;

public class ThunderController : MonoBehaviour
{
    // ���� ����Ʈ ����
    [SerializeField] private ParticleSystem[] lightingEffect;

    // ����� �ҽ�
    [SerializeField] private AudioSource audioSource;
        
    // ���� ������ �ð�
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
