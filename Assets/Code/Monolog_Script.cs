using UnityEngine;

public class Monolog_Script : MonoBehaviour
{
    public AudioSource _audio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Actor")
        {
            if (_audio != null)
            {
                _audio.Play();
                this.gameObject.SetActive(false);
            }
        }
    }
}
