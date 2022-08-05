using UnityEngine;

public class WalkAnimationState : MonoBehaviour
{
    private AudioSource m_AudioSource;
    public AudioClip[] m_Clip;
    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }
    public void Step_sound_play()
    {
        m_AudioSource.PlayOneShot(m_Clip[Random.Range(0, m_Clip.Length)]);
    }

}
