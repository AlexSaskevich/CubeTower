using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private int _capacity;
    
    private List<GameObject> _pool;

    private void Awake()
    {
        _pool = new List<GameObject>();
    }

    protected void Initialize(GameObject original)
    {
        for (int i = 0; i < _capacity; i++)
        {
            GameObject dollarIcon = Instantiate(original, transform);
            dollarIcon.SetActive(false);
            _pool.Add(dollarIcon);
        }
    }

    protected bool TryGetObject(out GameObject gameObject)
    {
        gameObject = _pool.FirstOrDefault(gameObject => gameObject.activeSelf == false);

        return gameObject != null;
    }
}