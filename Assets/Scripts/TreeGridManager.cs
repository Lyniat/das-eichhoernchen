using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGridManager : MonoBehaviour
{
    [SerializeField] GameObject zero;
    [SerializeField] GameObject one;
    [SerializeField] GameObject two;
    [SerializeField] GameObject three;
    static int arrayWidth = 10;
    static int arrayHeight = 15;
    int[,] gridArray = new int[arrayWidth, arrayHeight];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < arrayWidth; i++)
        {
            for (int j = 0; j < arrayHeight; j++)
            {
                Vector2 position = new Vector2(i, j);
                if (i == 0)
                {
                    Instantiate(two, position, Quaternion.identity);
                    continue;
                }
                else if (i == arrayWidth - 1)
                {
                    Instantiate(three, position, Quaternion.identity);
                    continue;
                }
                gridArray[i, j] = Random.Range(0, 2);
                if (gridArray[i, j] == 0)
                {
                    Instantiate(zero, position, Quaternion.identity);
                }
                else if (gridArray[i, j] == 1)
                {
                    if (i > 0)
                    {
                        if (gridArray[i - 1, j] == 1)
                        {
                            gridArray[i, j] = 0;
                            Instantiate(zero, position, Quaternion.identity);
                            continue;
                        }
                    }
                    if (j > 0)
                    {
                        if (gridArray[i, j - 1] == 1)
                        {
                            gridArray[i, j] = 0;
                            Instantiate(zero, position, Quaternion.identity);
                            continue;
                        }
                    }
                    Instantiate(one, position, Quaternion.identity);
                }
            }
        }
    }
}

