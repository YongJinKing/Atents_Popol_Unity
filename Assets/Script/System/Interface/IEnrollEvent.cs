using UnityEngine.Events;

public interface IEnrollEvent
{
    public void Enroll(UnityAction action);
}

public interface IEnrollEvent<T>
{
    public void Enroll(UnityAction<T> action);
}
