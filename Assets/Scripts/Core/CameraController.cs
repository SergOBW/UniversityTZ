using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;

    private bool _isRightMouseButtonHeld = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _isRightMouseButtonHeld = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            _isRightMouseButtonHeld = false;
        }

        if (_isRightMouseButtonHeld)
        {
            // Перемещение камеры вперед и назад в направлении взгляда
            float moveForward = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            transform.Translate(Vector3.forward * moveForward);

            // Перемещение камеры влево и вправо
            float moveRight = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            transform.Translate(Vector3.right * moveRight);

            // Вращение камеры вокруг оси Y
            float h = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float v = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
            transform.Rotate(0, h, 0, Space.World);
            transform.Rotate(v, 0, 0, Space.Self);
        }
    }
}
