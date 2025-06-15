using UnityEngine;

public class CreditsToggle : MonoBehaviour
{
    public GameObject creditsPanel;

    public void ToggleCredits()
    {
        creditsPanel.SetActive(!creditsPanel.activeSelf);
    }
}