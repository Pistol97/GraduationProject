//노트 언락을 알리기 위한 퍼블리셔
public interface NoteUnlockPublisher
{
    void AddObserver(NoteUnlockObserver observer);
    void DeleteObserver(NoteUnlockObserver observer);
    void NotifyObserver();
}