using UnityEngine;

public class AudioClip_Trigger : MonoBehaviour
{
    public AudioSource musicAudio;
    private bool isTriggered = false;
    void Start()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Actor" && musicAudio.isPlaying == false)
        {
            if (isTriggered == false)
            {
                isTriggered = true;
                musicAudio.Play();
            }
        }
        else if (other.tag == "Actor" && musicAudio.isPlaying == true)
        {
            musicAudio.UnPause();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Actor" && musicAudio.isPlaying == true)
        {
            if (isTriggered == true) 
            {
                isTriggered = false;
                musicAudio.Pause();
            }
        }
    }
}
