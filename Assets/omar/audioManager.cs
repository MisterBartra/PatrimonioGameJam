using UnityEngine;

public class audioManager : MonoBehaviour
{
    public AudioClip efectoFlecha;
    public AudioClip efectoDa�oEnte;
    public AudioClip efectoDa�oHuman;
    public AudioClip reloj;
    public AudioClip win;
    public AudioClip lose;

    public AudioSource audioS;
    public void playCualquierSound(AudioClip x)
    {
        audioS.PlayOneShot(x);
    }




}
