using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    private float speed=20f;
    public FloatingJoystick floatingJoy;
    public Rigidbody rb;

    public void Update()
    {
        Vector3 direction = Vector3.forward * floatingJoy.Vertical + Vector3.right * floatingJoy.Horizontal;
        rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
}