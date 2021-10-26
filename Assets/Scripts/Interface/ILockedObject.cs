public interface ILockedObject
{
    void TryUnlock(string name);
    bool IsPair(string name);
}
