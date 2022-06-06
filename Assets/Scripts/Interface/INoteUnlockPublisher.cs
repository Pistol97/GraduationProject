//노트 언락을 알리기 위한 Subject
public interface INoteUnlockPublisher
{
    void AddObserver(INoteUnlockObserver observer);
    void DeleteObserver(INoteUnlockObserver observer);
    void NotifyObserver();
}