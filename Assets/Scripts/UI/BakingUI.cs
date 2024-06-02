using UnityEngine;
using UnityEngine.UI;

public class BakingUI : MonoBehaviour
{
    public Button bakeStrawberryPieButton;
    public Button bakeBreadButton;
    public Button bakeCookieButton;
    public Button closeButton;  // Add a reference to the close button
    public BakingManager bakingManager;

    private void Start()
    {
        bakeStrawberryPieButton.onClick.AddListener(() => StartBaking(CollectableType.StrawberryPie));
        bakeBreadButton.onClick.AddListener(() => StartBaking(CollectableType.Bread));
        bakeCookieButton.onClick.AddListener(() => StartBaking(CollectableType.Cookie));
        closeButton.onClick.AddListener(CloseBakingPanel);  // Add listener to close the panel
    }

    private void StartBaking(CollectableType type)
    {
        bakingManager.StartBaking(type);
    }

    private void CloseBakingPanel()
    {
        bakingManager.CloseBakingPanel();
    }
}