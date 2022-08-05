using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;
    public bool isBossFight;
    public GameObject _player;
    public GameObject _boss;
    
    void Start()
    {
        transform.position = new Vector3(target.position.x,target.position.y,transform.position.z);
    }

   
    void LateUpdate()
    {
        if(transform.position != target.position && isBossFight == false)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
        else if(transform.position != target.position && isBossFight == true)
        {
            if (_player.transform.position.x > _boss.transform.position.x)
            {
                Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
                targetPosition.x = Mathf.Clamp(targetPosition.x + 300, minPosition.x, maxPosition.x);
                targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
                transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
            }
            else if (_player.transform.position.x < _boss.transform.position.x)
            {
                Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
                targetPosition.x = Mathf.Clamp(targetPosition.x - 300, minPosition.x, maxPosition.x);
                targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
                transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
            }
        }
    }
}
