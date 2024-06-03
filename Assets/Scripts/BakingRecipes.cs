using UnityEngine;

public class BakingRecipes : MonoBehaviour
{
    public GameObject bakingInstructionsCanvas;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleBakingInstructions();
        }
    }

    void ToggleBakingInstructions()
    {
        if (bakingInstructionsCanvas != null)
        {
            bool isActive = bakingInstructionsCanvas.activeSelf;
            bakingInstructionsCanvas.SetActive(!isActive);
        }
    }
}
