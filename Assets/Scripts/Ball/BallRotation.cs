using UnityEngine;

/// <summary>
/// �{�[���̉�]�֘A�̃N���X
/// </summary>
public class BallRotation : MonoBehaviour
{
    private float previousRotation; //1�t���[���O�̊p�x
    private float previousRotationSpeed; //1�t���[���O�̉�]���x
    [SerializeField]
    private float rotationSpeed;    //��]���x
    [SerializeField]
    private float rotationDirection;//��]����

    void Start()
    {
        previousRotation = transform.rotation.eulerAngles.z;
        previousRotationSpeed = 0;
    }

    void Update()
    {
        /*���݂�1�t���O�̉�]����*/
        float deltaRotation = transform.rotation.eulerAngles.z - previousRotation;

        //��]���x�̌v�Z
        rotationSpeed = deltaRotation * Mathf.Deg2Rad / Time.deltaTime;

        //�O�̃t���[���̉�]���x�Ɣ�r���Ă��̉�]���x��1.5�{�ȏ㍷����������
        //�ُ�l��O�̑��x�ɕς���
        //�܂���蓦���Ă��鎞������
        if(Mathf.Abs(rotationSpeed) > Mathf.Abs(previousRotationSpeed * 1.5f))
        {
            //Debug.LogError($"�ُ�l�ł�:{rotationSpeed} > {previousRotationSpeed} * 1.5");
            rotationSpeed = previousRotationSpeed;
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
        previousRotationSpeed = deltaRotation * Mathf.Deg2Rad / Time.deltaTime;
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
