using UnityEngine;

public class IsometricCameraFollow : MonoBehaviour
{
    [Header("Objetivo")]
    public Transform target; // Arrastra a tu Player aquí

    [Header("Configuración de Posición Isométrica")]
    [SerializeField] private Vector3 offset = new Vector3(0f, 15f, -10f); // Altura e inclinación
    [SerializeField] private float smoothSpeed = 5f; // Suavizado de seguimiento

    void LateUpdate()
    {
        if (target == null) return;

        // Posición deseada sumando el offset al jugador
        Vector3 desiredPosition = target.position + offset;

        // Transición suave (Lerp) para evitar tirones
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // La cámara siempre mira fijamente hacia el jugador
        transform.LookAt(target.position);
    }
}