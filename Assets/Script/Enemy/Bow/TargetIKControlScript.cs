using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIKControlScript : MonoBehaviour
{
    [SerializeField] private Transform targetpos;

    private void Update()
    {
        transform.position = targetpos.position;

        transform.rotation = targetpos.rotation;
    }
}
