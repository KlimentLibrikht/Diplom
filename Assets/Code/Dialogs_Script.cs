using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialogs_Script : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject dialogUI;
    public Text nameLabel;
    public Text dialogLabel;

    void Update()
    {
        YanHit();
    }
    public void YanHit()
    {
        if (playerController._health <= 50)
        {
            dialogUI.SetActive(true);
            nameLabel.text = "Ян";
            dialogLabel.text = "Больно!";
            StartCoroutine(WaitExitDialog());
        }
    }

    IEnumerator WaitExitDialog()
    {
        yield return new WaitForSeconds(2);
        dialogUI.SetActive(false);
    }
}
