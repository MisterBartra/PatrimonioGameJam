using UnityEngine;
using UnityEngine.UI;

public class CreditsToggle : MonoBehaviour
{
    public GameObject creditsPanel;

    public void ToggleCredits()
    {
        bool isPanelActive = creditsPanel.activeSelf; // Guarda estado antes de cambiarlo
        creditsPanel.SetActive(!isPanelActive); // Alterna visibilidad del panel
        transform.GetComponentInChildren<Text>().text = (!isPanelActive) ? "Cerrar" : "Créditos";
    }
    public void ToggleExit()
    {
        Application.Quit();
    }
}