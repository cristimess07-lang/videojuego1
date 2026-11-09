using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;
    public int attackDamage = 10; // Daño que le hace al jugador al tocarlo
    public float attackCooldown = 1f; // Tiempo entre ataques
    private float lastAttackTime;

    void Start()
    {
        // Buscar automáticamente al jugador por su Tag si no se asignó en el Inspector
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
    }

    void Update()
    {
        if (player != null)
        {
            // 1. Mirar hacia el jugador
            transform.LookAt(player);

            // 2. Moverse hacia el jugador lentamente
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    // Si el enemigo toca al jugador (usando triggers o colisiones normales)
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Comprobar el tiempo de espera (cooldown) para no hacer daño en cada frame
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(attackDamage);
                    lastAttackTime = Time.time; // Reiniciar el temporizador de ataque
                }
            }
        }
    }
}