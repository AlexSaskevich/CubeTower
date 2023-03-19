using UnityEngine;

public class DollarIconSpawner : ObjectPool
{
    [SerializeField] private GameObject _originalDollarIcon;
    [SerializeField] private Collector _collector;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        Initialize(_originalDollarIcon);
    }

    private void OnEnable()
    {
        _collector.DollarCollected += OnDollarCollected;
    }

    private void OnDisable()
    {
        _collector.DollarCollected -= OnDollarCollected;
    }

    private void OnDollarCollected(Dollar dollar)
    {
        if (TryGetObject(out GameObject dollarIcon) == false)
            return;

        SetDollarIcon(dollarIcon, dollar.transform.position);

        dollarIcon.GetComponent<DollarIconAnimation>().StartMove();
        dollarIcon.GetComponent<DollarIconAnimation>().StartScale();
    }

    private void SetDollarIcon(GameObject dollarIcon, Vector3 spawnPoint)
    {
        Vector3 screenPosition = _camera.WorldToScreenPoint(spawnPoint);
        dollarIcon.transform.position = screenPosition;
        dollarIcon.SetActive(true);
    }
}