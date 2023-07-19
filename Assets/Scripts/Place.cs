using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Place : MonoBehaviour
{
    int black =0, white= 0;
    public int[, ,] vertices= new int[8,8,2];
    public GameObject cube;
    public GameObject disc;
    int val = 0;
    void Start()
    {
        CreateShape();
        vertices[3, 3, 0] = 1;
        vertices[3, 4, 0] = -1;
        vertices[4, 3, 0] = -1;
        vertices[4, 4, 0] = 1;
    }
    void CreateShape()
    {
        for (int x = -4; x <= +3; x++)
        {
            for (int z = -4; z <= 3; z++)
            {
                vertices[x + 4, z + 4,0] = 0;
                vertices[x+4, z + 4,1] = 0;
                Instantiate(cube, new Vector3(x + 0.5f, 0, z + 0.5f), Quaternion.identity);
            }
        }
    }
    void Count()
    {
        for (int x = -4; x <= +3; x++)
        {
            for (int z = -4; z <= 3; z++)
            {
                if (vertices[x + 4, z + 4, 0] == 1)
                { 
                    black++; 
                }
                if (vertices[x + 4, z + 4, 0] == -1)
                { 
                    white++; 
                }
            }
        }
        //Debug.Log(black);
        //Debug.Log(white);
    }
    /*/void turn()
    {
        for (int z = 1; z <= 8; z++)
        {
            for (int x = 1; x <= 8; x++)
            {
                if (val % 2 == 0)
                {
                    //row
                    if (vertices[x - 1, z - 1, 0] == 1 && vertices[x, z - 1, 0] == -1)
                    {
                        int i = x;
                        while (vertices[i++, z - 1, 0] == -1 && x <= 7) ;
                        if (vertices[i - 1, z - 1, 0] == 1)
                        {
                            for (; x < i - 1; x++) 
                            {
                                vertices[x, z - 1, 0] = 1;
                            }
                        }
                    }
                    //coloumn
                    if (vertices[z - 1, x - 1, 0] == 1 && vertices[z - 1, x, 0] == -1)
                    {
                        int i = x;
                        while (x <= 7 && vertices[z - 1, i++, 0] == -1 ) ;
                        if (vertices[z - 1, i - 1, 0] == 1)
                        {
                            for (; x <i - 1; x++) 
                            {
                                vertices[z - 1, x, 0] = 1;
                            }
                        }
                        else
                        {
                            vertices[z - 1, x, 1] = 0;
                        }
                    }

                }
                if (val % 2 != 0)
                {
                    //rows
                    if (vertices[x - 1, z - 1, 0] == -1 && vertices[x, z - 1, 0] == 1)
                    {
                        int i=x;
                        while (x <= 7 && vertices[i++, z - 1, 0] == 1) ;
                        if (vertices[i - 1, z - 1, 0] == 0)
                        {
                            for (; x < i - 1; x++)
                            {
                                vertices[x,z-1, 0]= -1;
                            }
                        }
                    }
                    //column
                    if (vertices[z - 1, x - 1, 0] == -1 && vertices[z - 1, x, 0] == 1)
                    {
                        int i=x;
                        while (i<=7 && vertices[z - 1, i++, 0] == 1 ) ;
                        if (vertices[z - 1, i - 1, 0] == 0)
                        {
                            for (; x < i; x++)
                            {
                                vertices[z - 1, x, 0] = -1;
                            }
                        }
                    }
                }
            }
        }
    }/*/

    void ValidMove()
    {
        for (int x = -4; x <= +3; x++)
        {
            for (int z = -4; z <= 3; z++)
            {
                vertices[x + 4, z + 4, 1] = 0;
            }
        }
        for (int z = 1; z <= 8; z++)
        {
            for (int x = 1; x <= 8; x++)
            {
                if (val % 2 == 0)
                {
                    //rows L-R
                    if (x <= 6 && vertices[x - 1, z - 1, 0] == 0 && vertices[x, z - 1, 0] == -1)
                    {
                        int i = x;
                        while (i <= 7 && vertices[i++, z - 1, 0] == -1) ;
                        if (vertices[i - 1, z - 1, 0] == 1)
                        {
                            vertices[x - 1, z - 1, 1] = 1;
                        }
                        else
                        {
                            vertices[x - 1, z - 1, 1] = 0;
                        }
                    }
                    //rows R-L
                    if (x <= 6 && vertices[x - 1, z - 1, 0] == 1 && vertices[x, z - 1, 0] == -1)
                    {
                        int i = x;
                        while (i <= 7 && vertices[i++, z - 1, 0] == -1) ;
                        if (vertices[i - 1, z - 1, 0] == 0)
                        {
                            vertices[i - 1, z - 1, 1] = 1;
                        }
                        else
                        {
                            vertices[i - 1, z - 1, 1] = 0;
                        }
                    }

                }
                if (val % 2 != 0)
                {
                    //rows L-R
                    if (x <= 6 && vertices[x - 1, z - 1, 0] == 0 && vertices[x, z - 1, 0] == 1)
                    {
                        int i = x;
                        while (i <= 7 && vertices[i++, z - 1, 0] == 1) ;
                        if (vertices[i - 1, z - 1, 0] == -1)
                        {
                            vertices[x - 1, z - 1, 1] = 1;
                        }
                        else
                        {
                            vertices[x - 1, z - 1, 1] = 0;
                        }
                    }
                    //rows R-L
                    if (x <= 6 && vertices[x - 1, z - 1, 0] == -1 && vertices[x, z - 1, 0] == 1)
                    {
                        int i= x;
                        while (i <= 7 && vertices[i++, z - 1, 0] == 1) ;
                        if (vertices[i - 1, z - 1, 0] == 0)
                        {
                            vertices[i - 1, z - 1, 1] = 1;
                        }
                        else
                        {
                            vertices[i - 1, z - 1, 1] = 0;
                        }
                    }
                }
            }
        }
        for (int z = 1; z <= 8; z++)
        {
            for (int x = 1; x <= 8; x++)
            {
                if (val % 2 == 0)
                {
                    //coloumn B-T
                    if (x <= 6 && vertices[z - 1, x - 1, 0] == 0 && vertices[z - 1, x, 0] == -1)
                    {
                        int i = x;
                        while (i <= 7 && vertices[z - 1, i++, 0] == -1) ;
                        if (vertices[z - 1, i - 1, 0] == 1)
                        {
                            vertices[z - 1, x - 1, 1] = 1;
                        }
                    }
                    //coloumn T-B
                    if (x <= 6 && vertices[z - 1, x - 1, 0] == 1 && vertices[z - 1, x, 0] == -1)
                    {
                        int i = x;
                        while (i <= 7 && vertices[z - 1, i++, 0] == -1) ;
                        if (vertices[z - 1, i - 1, 0] == 0)
                        {
                            vertices[z - 1, i - 1, 1] = 1;
                        }
                    }
                }
                if (val % 2 != 0)
                {
                    //coloumn B-T
                    if (x <= 6 && vertices[z - 1, x - 1, 0] == 0 && vertices[z - 1, x, 0] == 1)
                    {
                        int i = x;
                        while (i <= 7 && vertices[z - 1, i++, 0] == 1) ;
                        if (vertices[z - 1, i - 1, 0] == -1)
                        {
                            vertices[z - 1, x - 1, 1] = 1;
                        }
                    }
                    //column T-B
                    if (x <= 6 && vertices[z - 1, x - 1, 0] == -1 && vertices[z - 1, x, 0] == 1)
                    {
                        int i = x;
                        while (i <= 7 && vertices[z - 1, i++, 0] == 1) ;
                        if (vertices[z - 1, i - 1, 0] == 0)
                        {
                            vertices[z - 1, i - 1, 1] = 1;
                        }
                    }
                }
            }
        }
        //Diagonal R-L
        for (int z = 1; z <= 8; z++)
        {
            for (int x = 1; x <= 8; x++)
            {
                if (val % 2 == 0)
                {
                    //diagonal D-U
                    if (x >= 3 && z <= 6 && vertices[x - 1, z - 1, 0] == 0 && vertices[x-2, z, 0] == -1)
                    {
                        int i = x - 2, j = z;
                        while (i >= 0 && j <= 7 && vertices[i--, j++, 0] == -1) ;
                        if (vertices[i + 1, j - 1, 0] == 1)
                        {
                            vertices[x - 1, z - 1, 1] = 1;
                        }
                        else
                        {
                            vertices[x - 1, z - 1, 1] = 0;
                        }
                    }
                    //diagonal U-D
                    if (x >= 3 && z <= 6 && vertices[x - 1, z - 1, 0] == 1 && vertices[x-2, z, 0] == -1)
                    {
                        int i = x - 2, j = z;
                        while (i >= 0 && j <= 7 && vertices[i--, j++, 0] == -1) ;
                        if (vertices[i + 1, j - 1, 0] == 0)
                        {
                            vertices[i + 1, j - 1, 1] = 1;
                        }
                        else
                        {
                            vertices[i + 1, j - 1, 1] = 0;
                        }
                    }

                }
                if (val % 2 != 0)
                {
                    //diagonal D-U
                    if (x >= 3 && z <= 6 && vertices[x - 1, z - 1, 0] == 0 && vertices[x - 2, z, 0] == 1)
                    {
                        int i = x-2, j = z;
                        while (i >= 0 && j <= 7 && vertices[i--, j++, 0] == 1) ;
                        if (vertices[i + 1, j - 1, 0] == -1)
                        {
                            vertices[x - 1, z - 1, 1] = 1;
                        }
                        else
                        {
                            vertices[x - 1, z - 1, 1] = 0;
                        }
                    }
                    //diagonal U-D
                    if (x >= 3 && z <= 6 && vertices[x - 1, z - 1, 0] == -1 && vertices[x-2, z, 0] == 1)
                    {
                        int i = x - 2, j = z;
                        while (i >= 0 && j <= 7 && vertices[i--, j++, 0] == 1) ;
                        if (vertices[i + 1, j - 1, 0] == 0)
                        {
                            vertices[i + 1, j - 1, 1] = 1;
                        }
                        else
                        {
                            vertices[i + 1, j - 1, 1] = 0;
                        }
                    }
                }
            }
        }
        //Diagonal L-R
        for (int z = 1; z <= 8; z++)
        {
            for (int x = 1; x <= 8; x++)
            {
                if (val % 2 == 0)
                {
                    //diagonal D-U
                    if (x <= 6 && z <= 6 && vertices[x - 1, z - 1, 0] == 0 && vertices[x, z, 0] == -1)
                    {
                        int i = x, j = z;
                        while (i <= 7 && j<=7 && vertices[i++, j++, 0] == -1) ;
                        if (vertices[i - 1, j - 1, 0] == 1)
                        {
                            vertices[x - 1, z - 1, 1] = 1;
                        }
                        else
                        {
                            vertices[x - 1, z - 1, 1] = 0;
                        }
                    }
                    //diagonal U-D
                    if (x <= 6 && z <= 6 && vertices[x - 1, z - 1, 0] == 1 && vertices[x, z, 0] == -1)
                    {
                        int i = x, j = z;
                        while (i <= 7 && j<= 7 && vertices[i++, j++, 0] == -1) ;
                        if (vertices[i - 1, j - 1, 0] == 0)
                        {
                            vertices[i - 1, j - 1, 1] = 1;
                        }
                        else
                        {
                            vertices[i - 1, j - 1, 1] = 0;
                        }
                    }

                }
                if (val % 2 != 0)
                {
                    //diagonal D-U
                    if (x <= 6 && z <= 6 && vertices[x - 1, z - 1, 0] == 0 && vertices[x, z, 0] == 1)
                    {
                        int i = x, j = z;
                        while (i <= 7 && j<=7 &&vertices[i++, j++, 0] == 1) ;
                        if (vertices[i - 1, j - 1, 0] == -1)
                        {
                            vertices[x - 1, z - 1, 1] = 1;
                        }
                        else
                        {
                            vertices[x - 1, z - 1, 1] = 0;
                        }
                    }
                    //diagonal U-D
                    if (x <= 6 && z<=6 && vertices[x - 1, z - 1, 0] == -1 && vertices[x, z, 0] == 1)
                    {
                        int i = x, j = z;
                        while (i <= 7 && j<=7 &&vertices[i++, j++, 0] == 1) ;
                        if (vertices[i - 1, j - 1, 0] == 0)
                        {
                            vertices[i - 1, j - 1, 1] = 1;
                        }
                        else
                        {
                            vertices[i - 1, j - 1, 1] = 0;
                        }
                    }
                }
            }
        }

    }

    void Update()
    {
        Count();
        ValidMove();
        //turn();
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.collider.tag == "cube" && vertices[(int)(hit.collider.transform.position.x + 3.5), (int)(hit.collider.transform.position.z + 3.5), 1] == 1)
                    if (val % 2 == 0)
                    {
                        Instantiate(disc, new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y + 0.375f, hit.collider.transform.position.z), Quaternion.identity);
                        vertices[(int)(hit.collider.transform.position.x + 3.5) , (int)(hit.collider.transform.position.z + 3.5),0] = 1;
                        Debug.Log((int)(hit.collider.transform.position.x + 3.5));
                        Debug.Log((int)(hit.collider.transform.position.z + 3.5));
                        val++;
                    }
                    else if (val % 2 != 0)
                    {
                        Instantiate(disc, new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y + 0.375f, hit.collider.transform.position.z), Quaternion.Euler(180, 0, 0));
                        vertices[(int)(hit.collider.transform.position.x + 3.5), (int)(hit.collider.transform.position.z + 3.5),0] = -1;
                        Debug.Log((int)(hit.collider.transform.position.x + 3.5));
                        Debug.Log((int)(hit.collider.transform.position.z + 3.5));
                        val++;
                    }
            }
        }  
    }
}
