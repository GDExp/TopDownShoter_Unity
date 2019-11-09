using UnityEngine;

[ExecuteInEditMode]
class CameraController : MonoBehaviour
{
    private const float MinCameraValue = 0f;
    private const float MaxCameraValue = 50f;

    private Camera _camera;
    [SerializeField] private Transform target;
    public Vector3 cameraEuler;
    public Vector3 cameraOffset;
    
    private void Start()
    {
        if (target == null) target = FindObjectOfType<Character.PlayerCharacter>().transform;
        _camera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        LookAtTarget();
    }

    private void LookAtTarget()
    {
        if (target is null) return;
        _camera.transform.localRotation = Quaternion.Euler(cameraEuler);
        _camera.transform.localPosition = target.localPosition + cameraOffset;
    }
}
