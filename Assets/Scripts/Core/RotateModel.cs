using UnityEngine;

namespace Core
{
    public class RotateModel : MonoBehaviour
    {
        [SerializeField]private float rotationSpeed = 100.0f;
    
        private bool _isRotating;

        void Update()
        {
            if (Input.GetMouseButtonDown(2))
            {
                _isRotating = true;
            }
            if (Input.GetMouseButtonUp(2))
            {
                _isRotating = false;
            }

            if (_isRotating)
            {
                float h = rotationSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
                float v = rotationSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime;
                transform.Rotate(Vector3.up, h, Space.World);
                transform.Rotate(Vector3.right, v, Space.Self);
            }
        }
    }
}
