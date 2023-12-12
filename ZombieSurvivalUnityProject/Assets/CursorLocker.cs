using UnityEngine;

public class CursorLocker : MonoBehaviour
{
    private bool _locked;

    private void Start()
    {
        SetupCursorLock();
    }

    private void SetupCursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _locked = true;
    }

    private void Update()
    {
        bool switchLock = Input.GetKeyDown(KeyCode.Escape);
        if (switchLock)
        {
            if (_locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
