using UnityEngine;

[ExecuteInEditMode]
class CameraController : MonoBehaviour
{
    private Transform _camera;
    [SerializeField] private Transform target;
    public Vector3 cameraEuler;
    public Vector3 cameraOffset;

    private void Start()
    {
        _camera = transform;
    }

    private void LateUpdate()
    {
        LookAtTarget();
    }

    private void LookAtTarget()
    {
        if (target is null) return;
        _camera.localRotation = Quaternion.Euler(cameraEuler);
        _camera.localPosition = target.localPosition + cameraOffset;
    }
}
