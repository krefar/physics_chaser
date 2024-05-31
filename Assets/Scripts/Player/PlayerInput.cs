using UnityEngine;

[RequireComponent(typeof(CharacterMover))]
public class PlayerInput : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    private CharacterMover _mover;

    private void Awake()
    {
        _mover = GetComponent<CharacterMover>();
    }

    private void Update()
    {
        var offsetX = Input.GetAxis(Horizontal);
        var offsetZ = Input.GetAxis(Vertical);

        _mover.Move(offsetX, offsetZ);
    }
}