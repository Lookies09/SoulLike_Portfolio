using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIController : MonoBehaviour
{
    [SerializeField] private Canvas canvas;   
    


    // Update is called once per frame
    void Update()
    {
        canvas.transform.rotation = Quaternion.LookRotation(Camera.main.transform.position - transform.position);
    }
}
