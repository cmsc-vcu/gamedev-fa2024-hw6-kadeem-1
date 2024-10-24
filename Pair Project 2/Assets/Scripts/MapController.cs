using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkRadius;
    public LayerMask terrainMask;
    public GameObject currentChunk;
    PlayerMovement pm;
    Vector3 playerLastPos; 

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    GameObject latestChunk;
    public float maxOpDist;
    float opDist;
    float optimizerCD;
    public float optimizerCDDur;

    // Start is called before the first frame update
    void Start()
    {
        playerLastPos = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }

    void ChunkChecker(){
        if(!currentChunk){
            return;
        }

        Vector3 moveDir = player.transform.position - playerLastPos;
        playerLastPos = player.transform.position;

        string directionName = GetDirectionName(moveDir);

        CheckAndSpawnChunk(directionName);
        
        if(directionName.Contains("Up")){
            CheckAndSpawnChunk("Up");
        }
        if(directionName.Contains("Down")){
            CheckAndSpawnChunk("Down");
        }
        if(directionName.Contains("Right")){
            CheckAndSpawnChunk("Right");
        }
        if(directionName.Contains("Left")){
            CheckAndSpawnChunk("Left");
        }
    }

    void CheckAndSpawnChunk(string direction) {
    Transform directionTransform = currentChunk.transform.Find(direction);
    if (directionTransform == null) {
        Debug.LogError($"Direction '{direction}' not found in current chunk.");
        return;
    }

    if (!Physics2D.OverlapCircle(directionTransform.position, checkRadius, terrainMask)) {
        SpawnChunk(directionTransform.position);
    }
}

    string GetDirectionName(Vector3 direction){
        direction = direction.normalized;
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y)){
            if(direction.y > 0.5f){
                return direction.x > 0 ? "Up Right" : "Up Left";
            } else if (direction.y < -0.5f) {
                return direction.x > 0 ? "Down Right" : "Down Left";
            } else {
                return direction.x > 0 ? "Right" : "Left";
            }
        } else {
            if(direction.x > 0.5f){
                return direction.y > 0 ? "Up Right" : "Down Right";
            } else if (direction.x < -0.5f) {
                return direction.y > 0 ? "Up Left" : "Down Left";
            } else {
                return direction.y > 0 ? "Up" : "Down";
            }
        }
    }

    void SpawnChunk(Vector3 spawnPos){
        int rand = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[rand], spawnPos, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    void ChunkOptimizer(){
        optimizerCD -= Time.deltaTime;
        if(optimizerCD <= 0f){
            optimizerCD = optimizerCDDur;
        } else { return; }
        
        foreach(GameObject chunk in spawnedChunks){
            opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
            if(opDist > maxOpDist){
                chunk.SetActive(false);
            } else {
                chunk.SetActive(true);
            }
        }
    }
}
