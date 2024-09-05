using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject landscapePanel;
    [SerializeField] private GameObject portraitPanel; 

    private void Start()
    {
        CheckOrientation();
    }

    private void Update()
    {
        CheckOrientation();
    }

    private void CheckOrientation()
    {
        if (Screen.width > Screen.height)
        {   
            landscapePanel.SetActive(true);
            portraitPanel.SetActive(false);
        }
        else
        {
            landscapePanel.SetActive(false);
            portraitPanel.SetActive(true);
        }
    }
}
