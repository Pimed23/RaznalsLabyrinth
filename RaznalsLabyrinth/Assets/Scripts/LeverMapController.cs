using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class LeverMapController : MonoBehaviour
{
    public Tile lever;
    public Tilemap area;
    public TextMeshProUGUI pointsText;
    public UnityEngine.UI.Image resetImage;
    public UnityEngine.UI.Image winImage;

    private int randomX, randomY, minX, minY, maxX, maxY;
    private int points = 0;
 
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
        winImage.enabled = false;
    }

    void SetPointsText() {
        pointsText.text = "Puntos: " + points.ToString();
        if(points >= 3) {
            resetImage.color = new Color(0, 0, 0, 1);
            winImage.enabled = true;
            Time.timeScale = 0;
            pointsText.text = "";
        }
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
            points++;
            SetPointsText();
        }
    }
}
