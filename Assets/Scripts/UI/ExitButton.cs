using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(ExitApp);
    }

    void ExitApp()
    {
        Application.Quit();
    }
}
