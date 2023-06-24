using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFrameRate : MonoBehaviour
{
    [SerializeField, Header("フレームレート")]
    private int FrameRate = 60;

    private void Start()
    {
        Application.targetFrameRate = FrameRate;   //フレームレートの設定
    }
}
