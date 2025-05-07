using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlInfoPanel : MonoBehaviour
{

    [SerializeField] private GameObject ctrlInfoPanel;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ctrlInfoPanel.SetActive(false);
        }
    }
}
