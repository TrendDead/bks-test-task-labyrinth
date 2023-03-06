using UnityEngine;

/// <summary>
/// Блокировщик мыши
/// </summary>
public class LockedMouse : MonoBehaviour
{
    /// <summary>
    /// Заблокировать мышку
    /// </summary>
    /// <param name="isLock"></param>
    public void LockMouse(bool isLock)
    {
        Cursor.lockState = isLock ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
