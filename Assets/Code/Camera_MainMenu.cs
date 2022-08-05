using UnityEngine;

public class Camera_MainMenu : MonoBehaviour
{
    public Vector2 DefaultResolution = new Vector2( x: 1080, y: 1920);
    [Range(0f, 1f)] public float WidthOrHeight = 0;

    private Camera _camera;

    private float initialSize;
    private float targetAspect;
    private void Start()
    {
        _camera = GetComponent<Camera>();
        initialSize = _camera.orthographicSize;

        targetAspect = DefaultResolution.x / DefaultResolution.y;
    }

    private void Update()
    {
        if (_camera.orthographic)
        {
            float constantWidthSize = initialSize * (targetAspect / _camera.aspect);
            _camera.orthographicSize = Mathf.Lerp( a: constantWidthSize, b: initialSize, t: WidthOrHeight);
        }
    }
}
