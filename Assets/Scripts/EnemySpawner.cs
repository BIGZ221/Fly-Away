using Random = System.Random;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{

    public MapGenerator mapGen;
    public Transform ground;
    public double spawnRate; // 1 every x seconds
    public float speedModifier;
    public float spawnHeightOffset = 10;
    public float spawnHeightMultiplier = 1.5f;
    public int maxEnemyCount = 100;
    public GameObject player;
    public GameObject enemyPrefab;
    public AnimationCurve enemySpeedCurve;
    public Text deathText;
    public Text winText;

    private MeshData meshData;
    private double timeSinceLastSpawn;
    private Random rng;
    private int spawnCount;

    void Start() {
        rng = new Random(42);
        meshData = mapGen.meshData;
        timeSinceLastSpawn = 0;
        spawnCount = 0;
    }

    void FixedUpdate() {
        if (meshData == null) meshData = mapGen.meshData;
        if (canSpawn()) spawnEnemy();
        timeSinceLastSpawn += Time.deltaTime;
        PlayerController pc = player.GetComponent<PlayerController>();
        if (pc.isDead) {
            deathText.text = deathText.text.Replace("X", spawnCount.ToString());
            winText.text = winText.text.Replace("X", spawnCount.ToString());
        }

    }

    bool canSpawn() {
        PlayerController pc = player.GetComponent<PlayerController>();
        return spawnCount < maxEnemyCount && timeSinceLastSpawn > spawnRate && pc.isDead == false;
    }

    void spawnEnemy() {
        GameObject enemy = Instantiate(enemyPrefab, getRandomSpawnPoint(), Quaternion.identity);
        EnemyController ec = enemy.GetComponent<EnemyController>();
        ec.player = player;
        ec.speedModifier += enemySpeedCurve.Evaluate(Mathf.InverseLerp(0, maxEnemyCount, spawnCount)) * speedModifier;
        spawnCount += 1;
        timeSinceLastSpawn = 0;
    }

    Vector3 getRandomSpawnPoint() {
        int verticesLen = meshData.vertices.GetLength(0);
        int index = rng.Next(verticesLen);
        Vector3 spawnPoint = meshData.vertices[index];
        spawnPoint.Scale(ground.localScale);
        float y = (spawnPoint.y + spawnHeightOffset) * spawnHeightMultiplier;
        spawnPoint.y = y;
        return spawnPoint;
    }

}
