using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Tiles")]
    public Transform[] tilePrefabs;
    [Header("Ground")]
    public Transform ground;
    public Vector2 mapSize;
    public float tileSize = 10;

    int tileToSpawn;

    void Start(){
        tileToSpawn = tilePrefabs.Length;
        GenerateMap();
    }

    public void GenerateMap() {
        for (int x = 0; x < mapSize.x; x++) {
            for (int y = 0; y < mapSize.y; y++) {
                tileToSpawn = Random.Range(0, tilePrefabs.Length);
                Vector3 _tilePos = new Vector3(x * tileSize + ground.position.x, 0, y * tileSize + ground.position.z);
                Transform _newTile = Instantiate(tilePrefabs[tileToSpawn], _tilePos, Quaternion.Euler(0,  Random.Range(0, 4)* 45, 0)); 
                _newTile.transform.parent = transform;
            }
        }
    }
}
