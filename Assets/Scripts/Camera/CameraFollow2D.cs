using UnityEngine;
using Cinemachine;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private Transform _target; 
    [SerializeField] private float _offsetY = 5f; 
    [SerializeField] private float _smoothSpeed = 0.125f; 

    private void LateUpdate()
    {
        if (_target != null)
        {

            Vector3 desiredPosition = new Vector3(_target.position.x, _target.position.y + _offsetY, _virtualCamera.transform.position.z);


            Vector3 smoothedPosition = Vector3.Lerp(_virtualCamera.transform.position, desiredPosition, _smoothSpeed);

            _virtualCamera.transform.position = smoothedPosition;
        }
    }
}
