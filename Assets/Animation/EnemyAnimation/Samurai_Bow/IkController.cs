using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkController : MonoBehaviour
{
    [SerializeField] private Transform traget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       transform.rotation = Quaternion.LookRotation(traget.transform.position - transform.position);
    }
}
