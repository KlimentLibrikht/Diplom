using UnityEngine;

public class TutorialWalkScript : MonoBehaviour
{
    public GameObject _nextBorder;
    public GameObject _tutorialCollider2D;
    public GameObject _MessagesUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.gameObject.SetActive(false);
            if (_nextBorder != null)
            {
                _nextBorder.gameObject.SetActive(true);
            }
            if (_tutorialCollider2D != null)
            {
                _tutorialCollider2D.gameObject.SetActive(false);
                _MessagesUI.gameObject.SetActive(false);
            }
        }
    }
}
