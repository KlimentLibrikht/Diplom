using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource _focusable;
    public AudioSource _click;

    public void HoverSound()
    {
        _focusable.Play();
    }

    public void ClickSound()
    {
        _click.Play();
    }
}
