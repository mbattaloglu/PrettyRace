public interface IObserver {
    void OnValueChanged(NotificationType type, Player player);
}