using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class Collector : MonoBehaviour
{
    private const float OptimalColliderSize = 0.65f;
    private const int Divisor = 2;
    private const float Step = 0.03f;
    private const int WaitForSeconds = 5;
    private const int MaxAvailableCubeSize = 20;

    [SerializeField] private ProgressBar _progressBar;
    [SerializeField] private SizeSlot _sizeSlot;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private List<GameObject> _stacks = new();

    private BoxCollider _collider;
    private bool _isCollect;

    public event UnityAction<Cube> CubeCollected;
    public event UnityAction<Dollar> DollarCollected;
    public event UnityAction AllCollected;
    public event UnityAction AvailableCubeSizeChanged;

    public float AvailableCubeSize { get; private set; } = CubeSize.XS;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();

        SetCollider();
    }

    private void OnEnable()
    {
        _sizeSlot.Upgraded += OnUpgraded;
        _progressBar.Reached += OnReached;
    }

    private void OnDisable()
    {
        _sizeSlot.Upgraded -= OnUpgraded;
        _progressBar.Reached -= OnReached;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cube cube) && cube.Size <= AvailableCubeSize)
        {
            foreach (var stack in _stacks)
                _isCollect = stack.GetComponent<Stack>().TryAdd(cube);

            if (_isCollect)
                CubeCollected?.Invoke(cube);
        }

        if (other.TryGetComponent(out Dollar dollar))
        {
            _wallet.AddCurrency(dollar.NominalValue);
            DollarCollected?.Invoke(dollar);
            Destroy(dollar.gameObject);
        }
    }

    private void SetCollider()
    {
        switch (AvailableCubeSize)
        {
            case CubeSize.XS:
            case CubeSize.S:
            case CubeSize.M:
                _collider.size = Vector3.one * OptimalColliderSize;
                _collider.center = Vector3.up * OptimalColliderSize / Divisor;
                break;
            case CubeSize.L:
            case CubeSize.XL:
                _collider.size = Vector3.one * AvailableCubeSize;
                _collider.center = Vector3.up * AvailableCubeSize / Divisor;
                break;
            default:
                _collider.size = Vector3.one * AvailableCubeSize;
                _collider.center = Vector3.up * AvailableCubeSize / Divisor;
                break;
        }
    }

    private void OnUpgraded(int level)
    {
        SetAvailableCubeSizeLevel(level);

        SetCollider();

        AvailableCubeSizeChanged?.Invoke();
    }

    private void SetAvailableCubeSizeLevel(int level)
    {
        if (level > _sizeSlot.MaxCubeSizeLevel)
        {
            AvailableCubeSize += Step;
            return;
        }

        AvailableCubeSize = CubeSize.Get((AvailableCubeSizeLevel)level);
    }

    private void OnReached()
    {
        StartCoroutine(Collect());
    }

    private IEnumerator Collect()
    {
        AvailableCubeSize += MaxAvailableCubeSize;
        _collider.size = Vector3.one * AvailableCubeSize;
        _collider.center = Vector3.up * AvailableCubeSize / Divisor;
        yield return new WaitForSeconds(WaitForSeconds);
        AllCollected?.Invoke();
    }
}