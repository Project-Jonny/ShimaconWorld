using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] TextAsset stageFile;
    [SerializeField] GameObject[] prefabs;

    enum TILE_TYPE
    {
        NULL,
        ENEMY_POINT,
        DRAG,
        BLOCK,
        BLOCK_HARD,
        PLAYER,
        BEAR,
        ARROW_UP,
        ARROW_DOWN,
        ARROW_LEFT,
        ARROW_RIGHT,
    }
    TILE_TYPE[,] tileTable;
    float tileSize;

    Vector2 centerPosition;

    void Start()
    {
        LoadTileData();
        CreatStage();
    }

    void LoadTileData()
    {
        string[] lines = stageFile.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);

        int colums = lines[0].Split(new[] { ',' }).Length;
        int rows = lines.Length;

        tileTable = new TILE_TYPE[colums, rows];

        for (int y = 0; y < rows; y++)
        {
            string[] values = lines[y].Split(new[] { ',' });
            for (int x = 0; x < colums; x++)
            {
                tileTable[x, y] = (TILE_TYPE)int.Parse(values[x]);
            }
        }
    }

    void CreatStage()
    {
        tileSize = prefabs[0].GetComponent<SpriteRenderer>().bounds.size.x;

        // 画面の中央にセットさせるための計算
        centerPosition.x = (tileTable.GetLength(0) / 2) * tileSize;
        centerPosition.y = (tileTable.GetLength(1) / 2) * tileSize;

        for (int y = 0; y < tileTable.GetLength(1); y++)
        {
            for (int x = 0; x < tileTable.GetLength(0); x++)
            {
                Vector2Int position = new Vector2Int(x, y);
                TILE_TYPE tileType = tileTable[x, y];
                GameObject obj = Instantiate(prefabs[(int)tileType]);
                obj.transform.position = GetScreenPositionFromTileTable(position);
            }
        }
    }

    Vector2 GetScreenPositionFromTileTable(Vector2Int position)
    {
        return new Vector2(position.x * tileSize - centerPosition.x + 0.5f, -(position.y * tileSize - centerPosition.y + 0.5f));
    }
}
