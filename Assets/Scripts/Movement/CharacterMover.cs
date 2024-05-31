using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void Move(float offsetX, float offsetZ)
    {
        if (_characterController.isGrounded)
        {
            var moveVector = new Vector3(offsetX, 0, offsetZ);

            _characterController.Move(moveVector * _speed * Time.deltaTime);
        }

        _characterController.Move(Physics.gravity);
    }
}