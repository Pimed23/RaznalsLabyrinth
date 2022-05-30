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
    private int[] randomPos, currentY;
    private bool[] activeLines;
    void Start()
    {
        tileMinX = tileMap.cellBounds.xMin;
        tileMaxX = tileMap.cellBounds.xMax - 1;
        tileMinY = tileMap.cellBounds.yMin;
        tileMaxY = tileMap.cellBounds.yMax - 1;
        currentY = new int[] { tileMaxY, tileMaxY };
        currentTileAnimation = 0;
        currentFrame = 0;
        randomPos = new int[] { Random.Range(tileMinX, tileMaxX + 1), 0 };
        activeLines = new bool [] { true, false};
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

            for(int i = 0; i < currentY.Length; i++)
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
        if (!activeLines[1] && currentY[0] == 0)
            activeLines[1] = true;

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
        for (int j = 0; j < currentY.Length; j++)
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
        UpdateAnimationByFrames(15);
        DrawLineOfSpikes();//15 frames per sprite
    }
}
