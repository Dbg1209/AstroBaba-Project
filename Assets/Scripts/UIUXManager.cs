using UnityEngine;

public class UIUXManager : MonoBehaviour
{
    [SerializeField] private GameObject victoryCanvas, defeatCanvas, controlCanvas, creditsCanvas;


    private void Start()
    {
        victoryCanvas.SetActive(false);
        defeatCanvas.SetActive(false);
        controlCanvas.SetActive(false);
        creditsCanvas.SetActive(false);
    }

    public void ShowControls()
    {
        controlCanvas.SetActive(true);
    }

    public void HideControls()
    {
        controlCanvas.SetActive(false);
    }

    public void ShowCredits()
    {
        creditsCanvas.SetActive(true);
    }

    public void HideCredits()
    {
        creditsCanvas.SetActive(false);
    }

    public void ShowVictory()
    {
        victoryCanvas.SetActive(true);
    }

    public void HideVictory()
    {
        victoryCanvas?.SetActive(false);
    }
    public void ShowGameOver()
    {
        defeatCanvas.SetActive(true);
    }

    public void HideGameOver()
    {
        defeatCanvas.SetActive(false);
    }
}
