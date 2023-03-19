using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerSpeedup))]
public class PlayerMovement : MonoBehaviour
{
    private const int Divisor = 10;

    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private SpeedSlot _speedSlot;
    [SerializeField] private Collector _collector;
    [SerializeField] private float _defaultMovementSpeed;
    [SerializeField] private float _rotationSpeed;

    private CharacterController _characterController;
    private Player _player;
    private PlayerSpeedup _speedup;
    private Vector3 _movementVector;
    private Vector2 _keyboardInput;
    private float _currentMovementSpeed;
    private float _upgradedMovementSpeed;

    public event UnityAction Moved;
    public event UnityAction Stopped;

    public PlayerInput PlayerInput { get; private set; }

    private void Awake()
    {
        _upgradedMovementSpeed = _defaultMovementSpeed;
        _currentMovementSpeed = _defaultMovementSpeed;
        _movementVector = Vector3.zero;
        _characterController = GetComponent<CharacterController>();
        _player = GetComponent<Player>();
        _speedup = GetComponent<PlayerSpeedup>();
        PlayerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _speedup.Activated += OnActivated;
        _speedup.Deactivated += OnDeactivated;
        _player.StartedSelling += OnStartedSelling;
        _speedSlot.Upgraded += OnUpgraded;
        _collector.AllCollected += OnAllCollected;
        PlayerInput.Enable();
    }

    private void OnDisable()
    {
        _speedup.Activated -= OnActivated;
        _speedup.Deactivated -= OnDeactivated;
        _player.StartedSelling -= OnStartedSelling;
        _speedSlot.Upgraded -= OnUpgraded;
        _collector.AllCollected -= OnAllCollected;
        PlayerInput.Disable();
    }

    private void Update()
    {
        Move();
        Rotate();
    }

    private void OnActivated(float speedModifier)
    {
        _currentMovementSpeed *= speedModifier;
    }

    private void OnDeactivated(float speedModifier)
    {
        _currentMovementSpeed = _upgradedMovementSpeed;
    }

    private void OnStartedSelling()
    {
        _currentMovementSpeed = _upgradedMovementSpeed;
    }

    private void Move()
    {
        _keyboardInput = PlayerInput.Player.Move.ReadValue<Vector2>();

        if (_keyboardInput != Vector2.zero)
            _movementVector = new Vector3(_keyboardInput.x, Vector2.zero.y, _keyboardInput.y);
        else
            _movementVector = new Vector3(_joystick.Horizontal, Vector3.zero.y, _joystick.Vertical);

        _movementVector *= _currentMovementSpeed;
        _characterController.SimpleMove(_movementVector);

        if (_characterController.velocity != Vector3.zero)
            Moved?.Invoke();
        else
            Stopped?.Invoke();
    }

    private void Rotate()
    {
        Vector3 direction = Vector3.RotateTowards(transform.forward, _movementVector, _rotationSpeed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void OnUpgraded(int level)
    {
        float modifier = (float)level / Divisor;
        _upgradedMovementSpeed += modifier / 2;
        _currentMovementSpeed = _upgradedMovementSpeed;

        if (_speedup.IsActivated)
            _currentMovementSpeed *= _speedup.SpeedModifier;
    }

    private void OnAllCollected()
    {
        enabled = false;
        Stopped?.Invoke();
    }
}