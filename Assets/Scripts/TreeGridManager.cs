using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGridManager : MonoBehaviour
{
    [SerializeField] GameObject zero;
    [SerializeField] GameObject one;
    [SerializeField] GameObject two;
    [SerializeField] GameObject three;
    [SerializeField] private int arrayWidth = 10;
    [SerializeField] private int arrayHeight = 15;
    [SerializeField] private float tileSize = 2f;
    private int[,] gridArray;
    [SerializeField] private int xOffset = 0;

    // Start is called before the first frame update
    void Start()
    {
        var offset = new Vector2(xOffset - (arrayWidth*tileSize)/2f, 0);
        gridArray = new int[arrayWidth, arrayHeight];
        for (int i = 0; i < arrayWidth; i++)
        {
            for (int j = 0; j < arrayHeight; j++)
            {
                Vector2 position = new Vector2(i, j);
                if (i == 0)
                {
                    var obj = Instantiate(two, position * tileSize + offset, Quaternion.identity);
                    obj.transform.localScale = new Vector3(tileSize, tileSize, 1);
                    continue;
                }
                else if (i == arrayWidth - 1)
                {
                    var obj = Instantiate(three, position * tileSize + offset, Quaternion.identity);
                    obj.transform.localScale = new Vector3(tileSize, tileSize, 1);
                    continue;
                }
                gridArray[i, j] = Random.Range(0, 15) == 1? 1 : 0;
                if (gridArray[i, j] == 0)
                {
                    var obj = Instantiate(zero, position * tileSize + offset, Quaternion.identity);
                    obj.transform.localScale = new Vector3(tileSize, tileSize, 1);
                }
                else if (gridArray[i, j] == 1)
                {
                    if (i > 0)
                    {
                        if (gridArray[i - 1, j] == 1)
                        {
                            gridArray[i, j] = 0;
                            var obj = Instantiate(zero, position * tileSize + offset, Quaternion.identity);
                            obj.transform.localScale = new Vector3(tileSize, tileSize, 1);
                            continue;
                        }
                    }
                    if (j > 0)
                    {
                        if (gridArray[i, j - 1] == 1)
                        {
                            gridArray[i, j] = 0;
                            var obj = Instantiate(zero, position * tileSize + offset, Quaternion.identity);
                            obj.transform.localScale = new Vector3(tileSize, tileSize, 1);
                            continue;
                        }
                    }
                    var obj2 = Instantiate(one, position * tileSize + offset, Quaternion.identity);
                    obj2.transform.localScale = new Vector3(tileSize, tileSize, 1);
                }
            }
        }
    }
}

