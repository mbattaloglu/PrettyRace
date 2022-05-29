public interface ISubejct {
    void Register(IObserver observer);
    void NotifyObservers(NotificationType type, Player player);
}