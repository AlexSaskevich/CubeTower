using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingRoad : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.isKinematic = true;
    }

    private void FixedUpdate()
    {
        Vector3 position = _rigidBody.position;
        _rigidBody.position += Vector3.forward * _speed * Time.fixedDeltaTime;
        _rigidBody.MovePosition(position);
    }
}