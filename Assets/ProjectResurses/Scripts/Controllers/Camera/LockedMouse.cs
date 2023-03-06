using UnityEngine;

/// <summary>
/// ����������� ����
/// </summary>
public class LockedMouse : MonoBehaviour
{
    /// <summary>
    /// ������������� �����
    /// </summary>
    /// <param name="isLock"></param>
    public void LockMouse(bool isLock)
    {
        Cursor.lockState = isLock ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
