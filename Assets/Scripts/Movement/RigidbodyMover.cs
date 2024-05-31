using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _minDistance;
    [SerializeField] private Transform _target;

    private Rigidbody _rigidbody;
    private Transform _transfrom;
    private float _stepHeigh = 0.3f;
    private float _stepDetection = 0.1f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transfrom = transform;
    }

    private void FixedUpdate()
    {
        var currentDistance = Vector3.Distance(_transfrom.position, _target.position);

        if (currentDistance > _minDistance)
        {
            Move();
        }
        else
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }

    private void Move()
    {
        _transfrom.LookAt(_target);

        var timedSpeed = _speed * Time.fixedDeltaTime;
        var newVelocity = GetVelocity();

        _rigidbody.velocity = newVelocity * timedSpeed;
    }

    private Vector3 GetVelocity()
    {
        var defaultVelocity = _transfrom.forward;
        defaultVelocity.y = 0;

        if (Physics.Raycast(new Ray(_transfrom.position, Vector3.down), out RaycastHit hitInfo, _transfrom.localScale.y * 2))
        {
            if (hitInfo.normal.y == 1)
            {
                if (_rigidbody.SweepTest(defaultVelocity, out RaycastHit firstHitInfo, _stepDetection))
                {
                    var rbPosition = new Vector3(_rigidbody.position.x, _rigidbody.position.y, _rigidbody.position.z);
                    rbPosition.y += _stepHeigh;

                    _rigidbody.position = rbPosition;

                    if (_rigidbody.SweepTest(defaultVelocity, out RaycastHit osffsetHitInfo, _stepDetection))
                    {
                        rbPosition.y -= _stepHeigh;
                        _rigidbody.position = rbPosition;
                        return defaultVelocity;
                    }
                    else
                    {
                        return defaultVelocity + firstHitInfo.collider.transform.up;
                    }
                }
            }
            else
            {
                return Vector3.ProjectOnPlane(defaultVelocity, hitInfo.normal);
            }
        }

        return defaultVelocity;
    }
}