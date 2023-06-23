using UnityEngine;

public class BallRotation : MonoBehaviour
{
    private float previousRotation; //1�t���[���O�̊p�x
    [SerializeField]
    private float rotationSpeed;    //��]���x
    [SerializeField]
    private float rotationDirection;//��]����

    void Start()
    {
        previousRotation = transform.rotation.eulerAngles.z;
    }

    void Update()
    {
        /*���݂�1�t���O�̉�]����*/
        float deltaRotation = transform.rotation.eulerAngles.z - previousRotation;

        //��]���x�̌v�Z
        rotationSpeed = deltaRotation * Mathf.Deg2Rad / Time.deltaTime;

        if(rotationSpeed > 300)
        {
            Debug.Log("�C��");
            rotationSpeed = -20;
        }
        else if(rotationSpeed < -300)
        {
            Debug.Log("�C��");
            rotationSpeed = 20;
        }
        rotationSpeed = CalculateRoundHalfUp(rotationSpeed, 2);
        //Debug.Log(rotationSpeed);

        //��]�����̌v�Z
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
    /// ���v���F�}�C�i�X
    /// �����v���F�v���X
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
