using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private float _zoomSpeed = 1.0f;
    [SerializeField] private float _rotationSpeed = 1.0f;

    private void Update()
    {
        var xAxisValue = Input.GetAxis("Horizontal");
        var zAxisValue = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(xAxisValue, 0.0f, zAxisValue) * _speed * Time.deltaTime);

        var scroll = -Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(new Vector3(0, scroll * _zoomSpeed, 0), Space.World);

        if (Input.GetMouseButton(1))
        {
            var mouseX = Input.GetAxis("Mouse X");
            var mouseY = Input.GetAxis("Mouse Y");
            transform.eulerAngles += new Vector3(-mouseY * _rotationSpeed, mouseX * _rotationSpeed, 0);
        }
    }
}