using UnityEngine;

/// <summary>
/// ボールの回転関連のクラス
/// </summary>
internal class BallRotation : MonoBehaviour
{
    private float previousAngle; //1フレーム前の角度
    private float previousRotationSpeed; //1フレーム前の回転速度
    [SerializeField]
    private float rotationSpeed;    //回転速度
    [SerializeField]
    private float rotationDirection;//回転方向

    void Start()
    {
        previousAngle = transform.rotation.eulerAngles.z;
        previousRotationSpeed = 0;
    }

    void Update()
    {
        /*現在と1フレ前の回転差分*/
        float deltaRotation = transform.rotation.eulerAngles.z - previousAngle;

        //回転速度の計算
        rotationSpeed = deltaRotation * Mathf.Deg2Rad / Time.deltaTime;

        //前のフレームの回転速度と比較してその回転速度の1.5倍以上差があったら
        //まだ取り逃している時がある
        if(Mathf.Abs(rotationSpeed) > Mathf.Abs(previousRotationSpeed * 1.5f))
        {
            rotationSpeed = previousRotationSpeed;    //異常値を前の速度に変える
        }

        rotationSpeed = CalculateRoundHalfUp(rotationSpeed, 2);

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

        previousAngle = transform.rotation.eulerAngles.z;
        previousRotationSpeed = deltaRotation * Mathf.Deg2Rad / Time.deltaTime;
    }

    internal float GetRotationSpeed
    {
        get { return rotationSpeed; }
    }

    /// <summary>
    /// 時計回り：マイナス
    /// 半時計回り：プラス
    /// </summary>
    internal float GetRotationDirection
    {
        get { return rotationDirection; }
    }

    /// <summary>
    /// 四捨五入メソッド
    /// </summary>
    /// <param name="value">四捨五入する値</param>
    /// <param name="digitsNum">少数何位か</param>
    /// <returns></returns>
    private float CalculateRoundHalfUp(float value, int digitsNum)
    {
        return float.Parse(value.ToString("F" + digitsNum.ToString()));
    }
}
