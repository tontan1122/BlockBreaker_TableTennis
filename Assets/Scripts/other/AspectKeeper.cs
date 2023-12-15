using UnityEngine;

/// <summary>
/// アスペクト比の固定化クラス
/// </summary>
internal class AspectKeeper : MonoBehaviour
{
    private static readonly Vector2 aspectVec = new Vector2(960,540);

    private Camera targetCamera;

    private void Start()
    {
        targetCamera= GetComponent<Camera>();
    }

    private void Update()
    {
        float screenAspect = Screen.width / (float)Screen.height;
        float targetAspect = aspectVec.x / aspectVec.y;

        float magRate = targetAspect / screenAspect;

        Rect viewportRect = new Rect(0, 0, 1, 1);

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
