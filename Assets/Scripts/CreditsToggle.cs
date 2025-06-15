using UnityEngine;
using UnityEngine.UI;

public class CreditsToggle : MonoBehaviour
{
    public GameObject creditsPanel;

    public void ToggleCredits()
    {
        creditsPanel.SetActive(!creditsPanel.activeSelf);
        transform.GetComponentInChildren<Text>().text = (!creditsPanel.activeSelf) ? "Créditos" : "Cerrar";
    }
}