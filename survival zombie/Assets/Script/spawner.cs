using UnityEngine;

public class spawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject prefab;
    public Transform[] spawnPos;
    private int spawnCount = 10;
    void Start()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Instantiate(prefab, spawnPos[Random.Range(0, spawnPos.Length)]);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
