using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKControllerScript : MonoBehaviour
{
    [SerializeField] GameObject playertarget;

    private void Update()
    {
        Vector3 direction = playertarget.transform.position - transform.position;
        direction = direction.normalized;

        Quaternion looktarget = Quaternion.LookRotation(direction);

        transform.rotation = looktarget;
    }

}
