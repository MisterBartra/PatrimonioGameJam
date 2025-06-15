using UnityEngine;

public class audioManager : MonoBehaviour
{
    public AudioClip efectoFlecha;
    public AudioClip efectoDañoEnte;
    public AudioClip efectoDañoHuman;
    public AudioClip reloj;
    public AudioClip win;
    public AudioClip lose;

    public AudioSource audioS;
    public void playCualquierSound(AudioClip x)
    {
        audioS.PlayOneShot(x);
    }




}
