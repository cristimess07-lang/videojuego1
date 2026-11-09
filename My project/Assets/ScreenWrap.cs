using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    public float minX = -9.5f;
    public float maxX = 9.5f;
    public float minZ = -9.5f;
    public float maxZ = 9.5f;

    void Update()
    {
        Vector3 pos = transform.position;
        bool wrapped = false;

        // Eje X
        if (pos.x < minX)
        {
            pos.x = maxX - 0.3f;
            wrapped = true;
        }
        else if (pos.x > maxX)
        {
            pos.x = minX + 0.3f;
            wrapped = true;
        }

        // Eje Z
        if (pos.z < minZ)
        {
            pos.z = maxZ - 0.3f;
            wrapped = true;
        }
        else if (pos.z > maxZ)
        {
            pos.z = minZ + 0.3f;
            wrapped = true;
        }

        if (wrapped)
        {
            // Si el objeto usa CharacterController (como el Player), hay que desactivarlo para teletransportar sin errores
            CharacterController cc = GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false;
                transform.position = pos;
                cc.enabled = true;
            }
            else
            {
                transform.position = pos;

                // Si usa Rigidbody (como el enemigo)
                Rigidbody rb = GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                }
            }
        }
    }
}