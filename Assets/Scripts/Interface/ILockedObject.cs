using UnityEngine;

public abstract class LockedObject : MonoBehaviour
{
    protected bool _isLocked;
    public bool IsLocked
    {
        get
        {
            return _isLocked;
        }

        set
        {
            _isLocked = value;
        }
    }

    public abstract void TryUnlock(Inventory inventory);
}
