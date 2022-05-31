using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LeverMapController : MonoBehaviour
{
    public Tile lever;
    public Tilemap area;
    private int randomX, randomY, minX, minY, maxX, maxY;
    // Start is called before the first frame update
    void Start()
    {
        minX = area.cellBounds.xMin;
        minY = area.cellBounds.yMin;
        maxX = area.cellBounds.xMax;
        maxY = area.cellBounds.yMax;
        randomX = Random.Range(minX, maxX);
        randomY = Random.Range(minY, maxY);
        Vector3Int localPlace = (new Vector3Int(randomX, randomY, (int)area.transform.position.y));
        area.SetTile(localPlace, lever);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Vector3Int localPlace = (new Vector3Int(randomX, randomY, (int)area.transform.position.y));
            area.SetTile(localPlace, null);
            randomX = Random.Range(minX, maxX);
            randomY = Random.Range(minY, maxY);
            localPlace = (new Vector3Int(randomX, randomY, (int)area.transform.position.y));
            area.SetTile(localPlace, lever);
        }
    }
}
