using UnityEngine.Events;

public interface ISlot
{
    public event UnityAction<int> Upgraded;

    public void Show();
}