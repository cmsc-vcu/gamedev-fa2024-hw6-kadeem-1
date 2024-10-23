using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkRadius;
    Vector3 noTerrainPosition;
    public LayerMask terrainMask;
    public GameObject currentChunk;
    PlayerMovement pm;

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
        pm = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        ChunkChecker();
    }

    void ChunkChecker(){
        if(!currentChunk){
            return;
        }

        if(pm.moveDir.x > 0 && pm.moveDir.y == 0){ // right
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Right").position, checkRadius, terrainMask)){
                noTerrainPosition = currentChunk.transform.Find("Right").position;
                SpawnChunk();
            }
        } else if(pm.moveDir.x < 0 && pm.moveDir.y == 0){ // left
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Left").position, checkRadius, terrainMask)){
                noTerrainPosition = currentChunk.transform.Find("Left").position;
                SpawnChunk();
            }
        } else if(pm.moveDir.x == 0 && pm.moveDir.y > 0){ // up
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Up").position, checkRadius, terrainMask)){
                noTerrainPosition = currentChunk.transform.Find("Up").position;
                SpawnChunk();
            }
        } else if(pm.moveDir.x == 0 && pm.moveDir.y < 0){ // down
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Down").position, checkRadius, terrainMask)){
                noTerrainPosition = currentChunk.transform.Find("Down").position;
                SpawnChunk();
            }
        } else if(pm.moveDir.x > 0 && pm.moveDir.y > 0){ // up right
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Up Right").position, checkRadius, terrainMask)){
                noTerrainPosition = currentChunk.transform.Find("Up Right").position;
                SpawnChunk();
            }
        } else if(pm.moveDir.x < 0 && pm.moveDir.y > 0){ // up left
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Up Left").position, checkRadius, terrainMask)){
                noTerrainPosition = currentChunk.transform.Find("Up Left").position;
                SpawnChunk();
            }
        } else if(pm.moveDir.x > 0 && pm.moveDir.y < 0){ // down right
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Down Right").position, checkRadius, terrainMask)){
                noTerrainPosition = currentChunk.transform.Find("Down Right").position;
                SpawnChunk();
            }
        } else if(pm.moveDir.x < 0 && pm.moveDir.y < 0){ // down left
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Down Left").position, checkRadius, terrainMask)){
                noTerrainPosition = currentChunk.transform.Find("Down Left").position;
                SpawnChunk();
            }
        }
    }

    void SpawnChunk(){
        int rand = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);
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
