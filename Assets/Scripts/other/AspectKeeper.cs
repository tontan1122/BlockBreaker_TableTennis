using UnityEngine;

public class AspectKeeper : MonoBehaviour
{
    [SerializeField] private Vector2 aspectVec;

    private Camera targetCamera;

    private void Start()
    {
        targetCamera= GetComponent<Camera>();
    }

    void Update()
    {
        var screenAspect = Screen.width / (float)Screen.height;
        var targetAspect = aspectVec.x / aspectVec.y;

        var magRate = targetAspect / screenAspect;

        var viewportRect = new Rect(0, 0, 1, 1);

        if (magRate < 1)
        {
            viewportRect.width = magRate;
            viewportRect.x = 0.5f - viewportRect.width * 0.5f;
        }
        else
        {
            viewportRect.height = 1 / magRate;
            viewportRect.y = 0.5f - viewportRect.height * 0.5f;
        }
        targetCamera.rect = viewportRect;
    }
}
