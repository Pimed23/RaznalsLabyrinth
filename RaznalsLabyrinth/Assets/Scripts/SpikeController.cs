using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpikeController : MonoBehaviour
{
    // Start is called before the first frame update
    public Tilemap tileMap;
    public List<Tile> spikeTiles;
    private int tileMinX, tileMaxX, tileMinY, tileMaxY;
    private int currentTileAnimation, currentFrame;
    private int offsetLines;
    private List<int> randomPos, currentY;
    private List<bool> activeLines;
    
    void Start()
    {
        tileMinX = tileMap.cellBounds.xMin;
        tileMaxX = tileMap.cellBounds.xMax - 1;
        tileMinY = tileMap.cellBounds.yMin;
        tileMaxY = tileMap.cellBounds.yMax - 1;
        randomPos = new List<int>();
        currentY = new List<int>();
        activeLines = new List<bool>();
        currentTileAnimation = 0;
        currentFrame = 0;
        initLines(3);//Number of lines to be spawned
        offsetLines = tileMaxY + 1 + (int)(Mathf.Abs(tileMinY));
        offsetLines = (int)(offsetLines / activeLines.Count);
    }
    private void initLines(int n)
    {
        randomPos.Add(Random.Range(tileMinX, tileMaxX + 1));
        currentY.Add(tileMaxY);
        activeLines.Add(true);
        
        for(int i = 1; i < n; i++)
        {
            randomPos.Add(0);
            currentY.Add(tileMaxY);
            activeLines.Add(false);
        }

    }
    private void UpdateAnimationByFrames(int totalFrames)
    {
        if (++currentFrame >= totalFrames)
        {
            currentTileAnimation += 1;
            currentFrame = 0;
        }

        if (currentTileAnimation >= spikeTiles.Count)
        {
            currentTileAnimation = 0;

            for(int i = 0; i < currentY.Count; i++)
            {
                if (activeLines[i])
                {
                    CleanLineOfSpikes(currentY[i]);
                    if (--currentY[i] < tileMinY)
                    {
                        currentY[i] = tileMaxY;
                        randomPos[i] = Random.Range(tileMinX, tileMaxX + 1);
                    }
                }
                
            }
            
        }
        for (int i = 1; i < activeLines.Count; i++)
        {
            if(!activeLines[i] && currentY[i-1] == tileMaxY - 1 - offsetLines )
            {
                activeLines[i] = true;
            }
        }

    }
    private void CleanLineOfSpikes(int posY)
    {
        for (int i = tileMinX; i <= tileMaxX; i++)
        {
            Vector3Int localPlace = (new Vector3Int(i, posY, (int)tileMap.transform.position.y));
            tileMap.SetTile(localPlace, null);
        }
    }
    private void DrawLineOfSpikes()
    {
        for (int j = 0; j < currentY.Count; j++)
        {
            if (activeLines[j])
            {
                for (int i = tileMinX; i <= tileMaxX; i++)
                {
                    Vector3Int localPlace = (new Vector3Int(i, currentY[j], (int)tileMap.transform.position.y));
                    if (i == randomPos[j])
                    {
                        tileMap.SetTile(localPlace, null);
                        continue;
                    }

                    tileMap.SetTile(localPlace, spikeTiles[currentTileAnimation]);
                }
            }
       
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        UpdateAnimationByFrames(3);
        DrawLineOfSpikes();//15 frames per sprite
    }
}
