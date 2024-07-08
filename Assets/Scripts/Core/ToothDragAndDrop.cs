using UnityEngine;

public class ToothDragAndDrop : MonoBehaviour
{
    private Vector3 _mOffset;
    private float _mZCoord;

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        transform.parent = null;
        Vector3 mPosition = gameObject.transform.position;
        _mZCoord = _mainCamera.WorldToScreenPoint(mPosition).z;
        _mOffset = mPosition - GetMouseAsWorldPoint();
        
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = _mZCoord;
        return _mainCamera.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseAsWorldPoint() + _mOffset;
    }
}
