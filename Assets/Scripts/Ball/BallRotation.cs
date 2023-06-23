using UnityEngine;

public class BallRotation : MonoBehaviour
{
    private float previousRotation; //1フレーム前の角度
    [SerializeField]
    private float rotationSpeed;    //回転速度
    [SerializeField]
    private float rotationDirection;//回転方向

    void Start()
    {
        previousRotation = transform.rotation.eulerAngles.z;
    }

    void Update()
    {
        /*現在と1フレ前の回転差分*/
        float deltaRotation = transform.rotation.eulerAngles.z - previousRotation;

        //回転速度の計算
        rotationSpeed = deltaRotation * Mathf.Deg2Rad / Time.deltaTime;

        if(rotationSpeed > 300)
        {
            Debug.Log("修正");
            rotationSpeed = -20;
        }
        else if(rotationSpeed < -300)
        {
            Debug.Log("修正");
            rotationSpeed = 20;
        }
        rotationSpeed = CalculateRoundHalfUp(rotationSpeed, 2);
        //Debug.Log(rotationSpeed);

        //回転方向の計算
        if (deltaRotation > 0)
        {
            rotationDirection = 1;
        }
        else if (deltaRotation < 0)
        {
            rotationDirection = -1;
        }
        else
        {
            rotationDirection = 0;
        }

        previousRotation = transform.rotation.eulerAngles.z;
    }

    public float GetRotationSpeed
    {
        get { return rotationSpeed; }
    }

    /// <summary>
    /// 時計回り：マイナス
    /// 半時計回り：プラス
    /// </summary>
    public float GetRotationDirection
    {
        get { return rotationDirection; }
    }

    private float CalculateRoundHalfUp(float value, int digitsNum)
    {
        return float.Parse(value.ToString("F" + digitsNum.ToString()));
    }
}
