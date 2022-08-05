using System.Collections;
using UnityEngine;

public class ArrowEnd : MonoBehaviour
{
    public bool _active;

    private void Start()
    {
        _active = true;
    }
    void Update()
    {
      if (_active == true)
        {
            StartCoroutine(Stop());
        }   
      else if (_active == false)
        {
            this.gameObject.SetActive(false);
            _active = true;
        }
    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(5.0f);
        _active = false;
    }
}
