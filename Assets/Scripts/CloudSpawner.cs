using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Kommt auf den Game Manager
public class CloudSpawner : MonoBehaviour
{
    [SerializeField] GameObject cloud;

    static int arrayWidth = 30;
    static int arrayHeight = 20;
    int[,] gridArray = new int[arrayWidth, arrayHeight];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < arrayWidth; i++)
        {
            for (int j = 0; j < arrayHeight; j++)
            {
                Vector2 position = new Vector2(i-10, j);
                gridArray[i, j] = Random.Range(0, 16);
                if (gridArray[i, j] == 1)
                {
                    if (i > 2)
                    {
                        if (gridArray[i - 2, j] == 1)
                        {
                            gridArray[i, j] = 0;
                            continue;
                        }
                    }
                    if (j > 2)
                    {
                        if (gridArray[i, j-2] == 1)
                        {
                            gridArray[i, j] = 0;
                            continue;
                        }
                    }
                    Instantiate(cloud, position, Quaternion.identity);
                }
            }
        }
    }
}
