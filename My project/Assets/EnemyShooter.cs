using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject enemyBulletPrefab; // Arrastra aquí el prefab de la bala enemiga
    public Transform firePoint;          // Desde dónde sale la bala (opcional, si no tienes, saldrá del centro)
    public float fireRate = 2f;          // Tiempo en segundos entre cada disparo
    private float nextFireTime = 0f;
    public float attackRange = 10f;      // Distancia a la que empieza a disparar

    private Transform player;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Si el jugador está dentro del rango de ataque
        if (distanceToPlayer <= attackRange)
        {
            // Girar para mirar siempre al jugador
            transform.LookAt(player);

            // Disparar cada cierto tiempo (fireRate)
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void Shoot()
    {
        if (enemyBulletPrefab != null)
        {
            // Si creaste un punto de salida (firePoint), úsalo; si no, usa la posición del enemigo
            Vector3 spawnPos = firePoint != null ? firePoint.position : transform.position + transform.forward;
            Instantiate(enemyBulletPrefab, spawnPos, transform.rotation);
        }
    }
}