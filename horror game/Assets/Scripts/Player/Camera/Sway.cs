using UnityEngine;

public class Sway : MonoBehaviour
{
    [SerializeField] private float _swayMultiplier;
    [SerializeField] private float _smooth;
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * _swayMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * _swayMultiplier;

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, _smooth * Time.deltaTime);
    }
}
