using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakingPanelOpener : MonoBehaviour
{
    public GameObject bakingPanel;

    private void OnMouseDown()
    {
        if (bakingPanel != null)
        {
            bakingPanel.SetActive(true);
        }
    }
}