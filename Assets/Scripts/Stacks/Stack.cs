using UnityEngine;
using UnityEngine.Events;

public class Stack : MonoBehaviour
{
    [SerializeField] protected CapacitySlot CapacitySlot;
    
    private const int DefaultCapacity = 15;

    private int _upgradedCapacity;

    public event UnityAction Fulled;
    public event UnityAction CapacityChanged;

    public int CurrentCapacity { get; private set; }

    private void Awake()
    {
        _upgradedCapacity = DefaultCapacity;
        CurrentCapacity = DefaultCapacity;
    }

    public virtual bool TryAdd(Cube cube)
    {
        return CheckCapacity() != false;
    }

    protected void IncreasePosition(Cube cube)
    {
        transform.position += new Vector3(0, cube.Size, 0);
    }

    protected void ReducePosition(Cube cube)
    {
        transform.position -= new Vector3(0, cube.Size, 0);
        CurrentCapacity++;
    }

    protected void IncreaseCapacity()
    {
        CurrentCapacity++;
    }

    protected void UpdateCapacity()
    {
        CurrentCapacity = _upgradedCapacity;
    }

    protected void OnUpgraded(int level)
    {
        CurrentCapacity++;
        _upgradedCapacity++;
        CapacityChanged?.Invoke();
    }

    private bool CheckCapacity()
    {
        if (CurrentCapacity <= 0)
            return false;

        if (CurrentCapacity - 1 <= 0)
            Fulled?.Invoke();

        CurrentCapacity--;

        return true;
    }
}