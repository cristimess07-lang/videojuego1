using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Arrastra aquí el prefab del enemigo
    public float spawnInterval = 3.5f; // Tiempo entre cada aparición
    public float mapLimit = 9f; // Cerca de los bordes del plano (10)

    void Start()
    {
        // Comienza a generar oleadas infinitas
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Generar una posición aleatoria en los bordes del mapa
            Vector3 spawnPos = GetRandomEdgePosition();

            // Instanciar el enemigo
            if (enemyPrefab != null)
            {
                Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            }
        }
    }

    Vector3 GetRandomEdgePosition()
    {
        // Decide aleatoriamente si aparece en el eje X o en el eje Z (en los bordes)
        bool spawnOnX = Random.value > 0.5f;
        float x = 0f;
        float z = 0f;

        if (spawnOnX)
        {
            x = Random.Range(-mapLimit, mapLimit);
            z = Random.value > 0.5f ? mapLimit : -mapLimit;
        }
        else
        {
            x = Random.value > 0.5f ? mapLimit : -mapLimit; // Nota: Random.Value con V minúscula o Random.Range
            z = Random.Range(-mapLimit, mapLimit);
        }

        // Devolvemos la posición con Y fija a la altura del suelo (1)
        return new Vector3(x, 1f, z);
    }
}