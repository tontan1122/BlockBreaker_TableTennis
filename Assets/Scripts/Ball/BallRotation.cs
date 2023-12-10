using UnityEngine;

/// <summary>
/// �{�[���̉�]�֘A�̃N���X
/// </summary>
internal class BallRotation : MonoBehaviour
{
    private float previousAngle; //1�t���[���O�̊p�x
    private float previousRotationSpeed; //1�t���[���O�̉�]���x
    [SerializeField]
    private float rotationSpeed;    //��]���x
    [SerializeField]
    private float rotationDirection;//��]����

    void Start()
    {
        previousAngle = transform.rotation.eulerAngles.z;
        previousRotationSpeed = 0;
    }

    void Update()
    {
        /*���݂�1�t���O�̉�]����*/
        float deltaRotation = transform.rotation.eulerAngles.z - previousAngle;

        //��]���x�̌v�Z
        rotationSpeed = deltaRotation * Mathf.Deg2Rad / Time.deltaTime;

        //�O�̃t���[���̉�]���x�Ɣ�r���Ă��̉�]���x��1.5�{�ȏ㍷����������
        //�܂���蓦���Ă��鎞������
        if(Mathf.Abs(rotationSpeed) > Mathf.Abs(previousRotationSpeed * 1.5f))
        {
            rotationSpeed = previousRotationSpeed;    //�ُ�l��O�̑��x�ɕς���
        }

        rotationSpeed = CalculateRoundHalfUp(rotationSpeed, 2);

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

        previousAngle = transform.rotation.eulerAngles.z;
        previousRotationSpeed = deltaRotation * Mathf.Deg2Rad / Time.deltaTime;
    }

    internal float GetRotationSpeed
    {
        get { return rotationSpeed; }
    }

    /// <summary>
    /// ���v���F�}�C�i�X
    /// �����v���F�v���X
    /// </summary>
    internal float GetRotationDirection
    {
        get { return rotationDirection; }
    }

    /// <summary>
    /// �l�̌ܓ����\�b�h
    /// </summary>
    /// <param name="value">�l�̌ܓ�����l</param>
    /// <param name="digitsNum">�������ʂ�</param>
    /// <returns></returns>
    private float CalculateRoundHalfUp(float value, int digitsNum)
    {
        return float.Parse(value.ToString("F" + digitsNum.ToString()));
    }
}
