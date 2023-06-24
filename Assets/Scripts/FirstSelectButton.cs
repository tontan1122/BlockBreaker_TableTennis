using UnityEngine;
using UnityEngine.UI;

public class FirstSelectButton : MonoBehaviour
{
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.Select();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            button.Select();
        }
    }
}
