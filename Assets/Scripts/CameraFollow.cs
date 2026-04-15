using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    private float currentAngle;

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Плавное закрепление вращения камеры за персонажем
        float targetAngle = target.eulerAngles.y;
        currentAngle = Mathf.LerpAngle(currentAngle, targetAngle, smoothSpeed);
        transform.rotation = Quaternion.Euler(10f, currentAngle, 0f);
    }
}