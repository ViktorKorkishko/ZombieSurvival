using UnityEngine;

public class CursorLocker : MonoBehaviour
{
    public bool Locked { get; private set; }

    private void Start()
    {
        SetupCursorLock();
    }

    private void Update()
    {
        bool switchLock = Input.GetKeyDown(KeyCode.Escape);
        if (switchLock)
        {
            SetLocked(switchLock);
        }
    }

    private void SetupCursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Locked = true;
    }

    private void SetLocked(bool @lock)
    {
        if (Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Locked = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Locked = true;
        }
    }
}
