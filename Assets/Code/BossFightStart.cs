using UnityEngine;

public class BossFightStart : MonoBehaviour
{
    public GameObject Boss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Boss.SetActive(true);
        this.gameObject.SetActive(false);     
    }
}
