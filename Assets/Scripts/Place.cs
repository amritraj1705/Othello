using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Place : MonoBehaviour
{
    int black =0, white= 0;
    public int[, ,] vertices= new int[8,8,2];
    public GameObject cube;
    public GameObject disc;
    public GameObject highlight; 
    int val = 0;
    int end = 0;
    int Ourscore;
    int OpponentScore;
    void Start()
    {
        CreateShape();
        vertices[3, 3, 0] = 1;
        vertices[3, 4, 0] = -1;
        vertices[4, 3, 0] = -1;
        vertices[4, 4, 0] = 1;
        Instantiate(disc, new Vector3(0.5f, 0.375f,0.5f), Quaternion.identity);
        Instantiate(disc, new Vector3(-0.5f, 0.375f, -0.5f), Quaternion.identity);
        Instantiate(disc, new Vector3(0.5f, 0.375f, -0.5f), Quaternion.Euler(180,0,0));
        Instantiate(disc, new Vector3(-0.5f, 0.375f, 0.5f), Quaternion.Euler(180, 0, 0));
        ValidMove(vertices,val);
        Instantiate(highlight, new Vector3(-0.5f, 0.3f, 1.5f), Quaternion.identity);
        Instantiate(highlight, new Vector3(-1.5f, 0.3f, 0.5f), Quaternion.identity);
        Instantiate(highlight, new Vector3(0.5f, 0.3f, -1.5f), Quaternion.identity);
        Instantiate(highlight, new Vector3(1.5f, 0.3f, -0.5f), Quaternion.identity);
    }
    void CreateShape()
    {
        for (int x = -4; x <= +3; x++)
        {
            for (int z = -4; z <= 3; z++)
            {
               // Debug.Log("1");
                vertices[x + 4, z + 4,0] = 0;
                vertices[x+4, z + 4,1] = 0;
                Instantiate(cube, new Vector3(x + 0.5f, 0, z + 0.5f), Quaternion.identity);
            }
        }
    }
    int Score(int[,,]place)
    {
        if (place[0, 0, 0] == 1) Ourscore = Ourscore + 4;
        if (place[7, 0, 0] == 1) Ourscore = Ourscore + 4;
        if (place[0, 7, 0] == 1) Ourscore = Ourscore + 4;
        if (place[7, 7, 0] == 1) Ourscore = Ourscore + 4;
        
        if (place[0, 1, 0] == 1) Ourscore = Ourscore - 3;
        if (place[1, 0, 0] == 1) Ourscore = Ourscore - 3;
        if (place[6, 0, 0] == 1) Ourscore = Ourscore - 3;
        if (place[0, 6, 0] == 1) Ourscore = Ourscore - 3;
        if (place[7, 1, 0] == 1) Ourscore = Ourscore - 3;
        if (place[1, 7, 0] == 1) Ourscore = Ourscore - 3;
        if (place[7, 6, 0] == 1) Ourscore = Ourscore - 3;
        if (place[6, 7, 0] == 1) Ourscore = Ourscore - 3;

        for(int i = 2; i <= 5; i++) 
        {
            if (place[i, 0, 0] == 1) Ourscore = Ourscore + 2;
            if (place[0, i, 0] == 1) Ourscore = Ourscore + 2;
            if (place[i, 7, 0] == 1) Ourscore = Ourscore + 2;
            if (place[7, i, 0] == 1) Ourscore = Ourscore + 2;
        }

        if (place[1, 1, 0] == 1) Ourscore = Ourscore - 4;
        if (place[6, 1, 0] == 1) Ourscore = Ourscore - 4;
        if (place[1, 6, 0] == 1) Ourscore = Ourscore - 4;
        if (place[6, 6, 0] == 1) Ourscore = Ourscore - 4;

        for (int i = 2; i <= 5; i++)
        {
            if (place[i, 1, 0] == 1) Ourscore = Ourscore - 1;
            if (place[1, i, 0] == 1) Ourscore = Ourscore - 1;
            if (place[i, 6, 0] == 1) Ourscore = Ourscore - 1;
            if (place[6, i, 0] == 1) Ourscore = Ourscore - 1;
        }

        if (place[2, 2, 0] == 1) Ourscore = Ourscore + 1;
        if (place[2, 5, 0] == 1) Ourscore = Ourscore + 1;
        if (place[5, 2, 0] == 1) Ourscore = Ourscore + 1;
        if (place[5, 5, 0] == 1) Ourscore = Ourscore + 1;

        for (int i = 3; i <= 4; i++)
        {
            if (place[i, 2, 0] == 1) Ourscore = Ourscore + 0;
            if (place[2, i, 0] == 1) Ourscore = Ourscore + 0;
            if (place[i, 5, 0] == 1) Ourscore = Ourscore + 0;
            if (place[5, i, 0] == 1) Ourscore = Ourscore + 0;
        }

        if (place[3, 3, 0] == 1) Ourscore = Ourscore + 1;
        if (place[3, 4, 0] == 1) Ourscore = Ourscore + 1;
        if (place[4, 3, 0] == 1) Ourscore = Ourscore + 1;
        if (place[4, 4, 0] == 1) Ourscore = Ourscore + 1;


        if (place[0, 0, 0] == -1) OpponentScore = OpponentScore + 4;
        if (place[7, 0, 0] == -1) OpponentScore = OpponentScore + 4;
        if (place[0, 7, 0] == -1) OpponentScore = OpponentScore + 4;
        if (place[7, 7, 0] == -1) OpponentScore = OpponentScore + 4;

        if (place[0, 1, 0] == -1) OpponentScore = OpponentScore - 3;
        if (place[1, 0, 0] == -1) OpponentScore = OpponentScore - 3;
        if (place[6, 0, 0] == -1) OpponentScore = OpponentScore - 3;
        if (place[0, 6, 0] == -1) OpponentScore = OpponentScore - 3;
        if (place[7, 1, 0] == -1) OpponentScore = OpponentScore - 3;
        if (place[1, 7, 0] == -1) OpponentScore = OpponentScore - 3;
        if (place[7, 6, 0] == -1) OpponentScore = OpponentScore - 3;
        if (place[6, 7, 0] == -1) OpponentScore = OpponentScore - 3;

        for (int i = 2; i <= 5; i++)
        {
            if (place[i, 0, 0] == -1) OpponentScore = OpponentScore + 2;
            if (place[0, i, 0] == -1) OpponentScore = OpponentScore + 2;
            if (place[i, 7, 0] == -1) OpponentScore = OpponentScore + 2;
            if (place[7, i, 0] == -1) OpponentScore = OpponentScore + 2;
        }

        if (place[1, 1, 0] == -1) OpponentScore = OpponentScore - 4;
        if (place[6, 1, 0] == -1) OpponentScore = OpponentScore - 4;
        if (place[1, 6, 0] == -1) OpponentScore = OpponentScore - 4;
        if (place[6, 6, 0] == -1) OpponentScore = OpponentScore - 4;

        for (int i = 2; i <= 5; i++)
        {
            if (place[i, 1, 0] == -1) OpponentScore = OpponentScore - 1;
            if (place[1, i, 0] == -1) OpponentScore = OpponentScore - 1;
            if (place[i, 6, 0] == -1) OpponentScore = OpponentScore - 1;
            if (place[6, i, 0] == -1) OpponentScore = OpponentScore - 1;
        }

        if (place[2, 2, 0] == -1) OpponentScore = OpponentScore + 1;
        if (place[2, 5, 0] == -1) OpponentScore = OpponentScore + 1;
        if (place[5, 2, 0] == -1) OpponentScore = OpponentScore + 1;
        if (place[5, 5, 0] == -1) OpponentScore = OpponentScore + 1;

        for (int i = 3; i <= 4; i++)
        {
            if (place[i, 2, 0] == -1) OpponentScore = OpponentScore + 0;
            if (place[2, i, 0] == -1) OpponentScore = OpponentScore + 0;
            if (place[i, 5, 0] == -1) OpponentScore = OpponentScore + 0;
            if (place[5, i, 0] == -1) OpponentScore = OpponentScore + 0;
        }

        if (place[3, 3, 0] == -1) OpponentScore = OpponentScore + 1;
        if (place[3, 4, 0] == -1) OpponentScore = OpponentScore + 1;
        if (place[4, 3, 0] == -1) OpponentScore = OpponentScore + 1;
        if (place[4, 4, 0] == -1) OpponentScore = OpponentScore + 1;

        return Ourscore -OpponentScore;
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
     void ValidMove(int[,,]place,int value)
    {
        for (int x = -4; x <= +3; x++)
        {
            for (int z = -4; z <= 3; z++)
            {
                place[x + 4, z + 4, 1] = 0;
            }
        }
        for (int z = 1; z <= 8; z++)
        {
            for (int x = 1; x <= 8; x++)
            {
                if (value % 2 == 0)
                {
                    //rows L-R
                    if (x <= 6 && place[x - 1, z - 1, 0] == 0 && place[x, z - 1, 0] == -1)
                    {
                        int i = x;
                        while (i <= 7 && place[i++, z - 1, 0] == -1) ;
                        if (place[i - 1, z - 1, 0] == 1)
                        {
                            place[x - 1, z - 1, 1] = 1;
                        }
                    }
                    //rows R-L
                    if (x <= 6 && place[x - 1, z - 1, 0] == 1 && place[x, z - 1, 0] == -1)
                    {
                        int i = x;
                        while (i <= 7 && place[i++, z - 1, 0] == -1) ;
                        if (place[i - 1, z - 1, 0] == 0)
                        {
                            place[i - 1, z - 1, 1] = 1;
                        }
                    }

                }
                if (value % 2 != 0)
                {
                    //rows L-R
                    if (x <= 6 && place[x - 1, z - 1, 0] == 0 && place[x, z - 1, 0] == 1)
                    {
                        int i = x;
                        while (i <= 7 && place[i++, z - 1, 0] == 1) ;
                        if (place[i - 1, z - 1, 0] == -1)
                        {
                            place[x - 1, z - 1, 1] = 1;
                        }
                    }
                    //rows R-L
                    if (x <= 6 && place[x - 1, z - 1, 0] == -1 && place[x, z - 1, 0] == 1)
                    {
                        int i= x;
                        while (i <= 7 && place[i++, z - 1, 0] == 1) ;
                        if (place[i - 1, z - 1, 0] == 0)
                        {
                            place[i - 1, z - 1, 1] = 1;
                        }
                    }
                }
            }
        }
        for (int z = 1; z <= 8; z++)
        {
            for (int x = 1; x <= 8; x++)
            {
                if (value % 2 == 0)
                {
                    //coloumn B-T
                    if (x <= 6 && place[z - 1, x - 1, 0] == 0 && place[z - 1, x, 0] == -1)
                    {
                        int i = x;
                        while (i <= 7 && place[z - 1, i++, 0] == -1) ;
                        if (place[z - 1, i - 1, 0] == 1)
                        {
                            place[z - 1, x - 1, 1] = 1;
                        }
                    }
                    //coloumn T-B
                    if (x <= 6 && place[z - 1, x - 1, 0] == 1 && place[z - 1, x, 0] == -1)
                    {
                        int i = x;
                        while (i <= 7 && place[z - 1, i++, 0] == -1) ;
                        if (place[z - 1, i - 1, 0] == 0)
                        {
                            place[z - 1, i - 1, 1] = 1;
                        }
                    }
                }
                if (val % 2 != 0)
                {
                    //coloumn B-T
                    if (x <= 6 && place[z - 1, x - 1, 0] == 0 && place[z - 1, x, 0] == 1)
                    {
                        int i = x;
                        while (i <= 7 && place[z - 1, i++, 0] == 1) ;
                        if (place[z - 1, i - 1, 0] == -1)
                        {
                            place[z - 1, x - 1, 1] = 1;
                        }
                    }
                    //column T-B
                    if (x <= 6 && place[z - 1, x - 1, 0] == -1 && place[z - 1, x, 0] == 1)
                    {
                        int i = x;
                        while (i <= 7 && place[z - 1, i++, 0] == 1) ;
                        if (place[z - 1, i - 1, 0] == 0)
                        {
                            place[z - 1, i - 1, 1] = 1;
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
                if (value % 2 == 0)
                {
                    //diagonal D-U
                    if (x >= 3 && z <= 6 && place[x - 1, z - 1, 0] == 0 && place[x-2, z, 0] == -1)
                    {
                        int i = x - 2, j = z;
                        while (i >= 0 && j <= 7 && place[i--, j++, 0] == -1) ;
                        if (place[i + 1, j - 1, 0] == 1)
                        {
                            place[x - 1, z - 1, 1] = 1;
                        }
                    }
                    //diagonal U-D
                    if (x >= 3 && z <= 6 && place[x - 1, z - 1, 0] == 1 && place[x - 2, z, 0] == -1)
                    {
                        int i = x - 2, j = z;
                        while (i >= 0 && j <= 7 && place[i--, j++, 0] == -1) ;
                        if (place[i + 1, j - 1, 0] == 0)
                        {
                            place[i + 1, j - 1, 1] = 1;
                        }
                    }
                }
                if (value % 2 != 0)
                {
                    //diagonal D-U
                    if (x >= 3 && z <= 6 && place[x - 1, z - 1, 0] == 0 && place[x - 2, z, 0] == 1)
                    {
                        int i = x-2, j = z;
                        while (i >= 0 && j <= 7 && place[i--, j++, 0] == 1) ;
                        if (place[i + 1, j - 1, 0] == -1)
                        {
                            place[x - 1, z - 1, 1] = 1;
                        }
                    }
                    //diagonal U-D
                    if (x >= 3 && z <= 6 && place[x - 1, z - 1, 0] == -1 && place[x-2, z, 0] == 1)
                    {
                        int i = x - 2, j = z;
                        while (i >= 0 && j <= 7 && place[i--, j++, 0] == 1) ;
                        if (place[i + 1, j - 1, 0] == 0)
                        {
                            place[i + 1, j - 1, 1] = 1;
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
                if (value % 2 == 0)
                {
                    //diagonal D-U
                    if (x <= 6 && z <= 6 && place[x - 1, z - 1, 0] == 0 && place[x, z, 0] == -1)
                    {
                        int i = x, j = z;
                        while (i <= 7 && j<=7 && place[i++, j++, 0] == -1) ;
                        if (place[i - 1, j - 1, 0] == 1)
                        {
                            place[x - 1, z - 1, 1] = 1;
                        }
                    }
                    //diagonal U-D
                    if (x <= 6 && z <= 6 && place[x - 1, z - 1, 0] == 1 && place[x, z, 0] == -1)
                    {
                        int i = x, j = z;
                        while (i <= 7 && j<= 7 && place[i++, j++, 0] == -1) ;
                        if (place[i - 1, j - 1, 0] == 0)
                        {
                            place[i - 1, j - 1, 1] = 1;
                        }
                    }

                }
                if (value % 2 != 0)
                {
                    //diagonal D-U
                    if (x <= 6 && z <= 6 && place[x - 1, z - 1, 0] == 0 && place[x, z, 0] == 1)
                    {
                        int i = x, j = z;
                        while (i <= 7 && j<=7 && place[i++, j++, 0] == 1) ;
                        if (place[i - 1, j - 1, 0] == -1)
                        {
                            place[x - 1, z - 1, 1] = 1;
                        }
                    }
                    //diagonal U-D
                    if (x <= 6 && z<=6 && place[x - 1, z - 1, 0] == -1 && place[x, z, 0] == 1)
                    {
                        int i = x, j = z;
                        while (i <= 7 && j<=7 && place[i++, j++, 0] == 1) ;
                        if (place[i - 1, j - 1, 0] == 0)
                        {
                            place[i - 1, j - 1, 1] = 1;
                        }
                    }
                }
            }
        }
    }

    void Update()
    {
        Count();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.collider.tag == "cube" && vertices[(int)(hit.collider.transform.position.x + 3.5), (int)(hit.collider.transform.position.z + 3.5), 1] == 1)
                { if (val % 2 == 0)
                    {
                        //Instantiate(disc, new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y + 0.375f, hit.collider.transform.position.z), Quaternion.identity);
                        vertices[(int)(hit.collider.transform.position.x + 3.5), (int)(hit.collider.transform.position.z + 3.5), 0] = 1;
                        int x = (int)(hit.collider.transform.position.x + 3.5f), z = (int)(hit.collider.transform.position.z + 3.5f);
                        //Debug.Log(x);
                        //Debug.Log(z);
                        //Debug.Log(vertices[x, z, 0]);
                        //rows L-R
                        int i = x + 1;
                        while (i <= 7 && vertices[i++, z, 0] == -1) ;
                        if (vertices[i - 1, z, 0] == 1)
                        {
                            for (int j = x + 1; j < i - 1; j++)
                            {
                                vertices[j, z, 0] = 1;
                            }
                        }
                        //rows R-L
                        int k = x - 1;
                        while (k >= 0 && vertices[k--, z, 0] == -1) ;
                        if (vertices[k + 1, z, 0] == 1)
                        {
                            for (int j = x - 1; j > k + 1; j--)
                            {
                                vertices[j, z, 0] = 1;
                            }
                        }
                        int m = z + 1;
                        //coloumn B-T
                        while (m <= 7 && vertices[x, m++, 0] == -1) ;
                        if (vertices[x, m - 1, 0] == 1)
                        {
                            for (int j = z + 1; j < m -1; j++)
                            {
                                vertices[x, j, 0] = 1;
                            }
                        }
                        int p = z - 1;
                        //coloumn T-B
                        while (p >= 0 && vertices[x, p--, 0] == -1) ;
                        if (vertices[x, p + 1, 0] == 1)
                        {
                            for (int j = z - 1; j > p+1; j--)
                            {
                                vertices[x, j, 0] = 1;
                            }
                        }
                        i = x + 1;
                        k = x - 1;
                        //diagonal D-U
                        m = z + 1;
                        while (k >= 0 && m <= 7 && vertices[k--, m++, 0] == -1) ;
                        if (vertices[k + 1, m - 1, 0] == 1)
                        {
                            for (int j = x - 1, n = z + 1; j > k +1 && n < m-1; j--, n++)
                            {
                                vertices[j, n, 0] = 1;
                            }
                        }
                        //diagonal U-D
                        p = z - 1;
                        while (p >= 0 && i <= 7 && vertices[i++, p--, 0] == -1) ;
                        if (vertices[i - 1, p + 1, 0] == 1)
                        {
                            for (int j = x + 1, n = z - 1; j < i - 1 && n > p+1; j++, n--)
                            {
                                vertices[j, n, 0] = 1;
                            }
                        }
                        p = z - 1;
                        m = z + 1;
                        i = x + 1;
                        k = x - 1;
                        //diagonal D-U
                        while (i <= 7 && m <= 7 && vertices[i++, m++, 0] == -1) ;
                        if (vertices[i - 1, m - 1, 0] == 1)
                        {
                            for (int j = x + 1, n = z + 1; j < i - 1 && n < m-1; j++, n++)
                            {
                                vertices[j, n, 0] = 1;
                            }
                        }
                        //diagonal U-D
                        while (k >= 1 && p >= 1 && vertices[k--, p--, 0] == -1) ;
                        if (vertices[k + 1, p + 1, 0] == 1)
                        {
                            for (int j = x - 1, n = z - 1; j > k + 1 && n > p+1; j--, n--)
                            {
                                vertices[j, n, 0] = 1;
                            }
                        }
                        //Debug.Log(vertices[x, z, 0]);
                        val++;
                    }
                    else if (val % 2!= 0)
                    {if (ModeSelector.Mode == 0)
                        {
                            //Instantiate(disc, new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y + 0.375f, hit.collider.transform.position.z), Quaternion.Euler(180, 0, 0));
                            vertices[(int)(hit.collider.transform.position.x + 3.5), (int)(hit.collider.transform.position.z + 3.5), 0] = -1;
                            int x = (int)(hit.collider.transform.position.x + 3.5f), z = (int)(hit.collider.transform.position.z + 3.5f);
                            //Debug.Log(vertices[x, z, 0]);
                            //rows L-R
                            int i = x + 1;
                            while (i <= 7 && vertices[i++, z, 0] == 1) ;
                            if (vertices[i - 1, z, 0] == -1)
                            {
                                for (int j = x + 1; j < i - 1; j++)
                                {
                                    vertices[j, z, 0] = -1;
                                }
                            }
                            //rows R-L
                            int k = x - 1;
                            while (k >= 0 && vertices[k--, z, 0] == 1) ;
                            if (vertices[k + 1, z, 0] == -1)
                            {
                                for (int j = x - 1; j > k + 1; j--)
                                {
                                    vertices[j, z, 0] = -1;
                                }
                            }
                            i = x + 1;
                            k = x - 1;
                            int m = z + 1;
                            //coloumn B-T
                            while (m <= 7 && vertices[x, m++, 0] == 1) ;
                            if (vertices[x, m - 1, 0] == -1)
                            {
                                for (int j = z + 1; j < m - 1; j++)
                                {
                                    vertices[x, j, 0] = -1;
                                }
                            }
                            int p = z - 1;
                            //coloumn T-B
                            while (p >= 0 && vertices[x, p--, 0] == 1) ;
                            if (vertices[x, p + 1, 0] == -1)
                            {
                                for (int j = z - 1; j > p + 1; j--)
                                {
                                    vertices[x, j, 0] = -1;
                                }
                            }
                            i = x + 1;
                            k = x - 1;
                            //diagonal D-U
                            m = z + 1;
                            while (k >= 0 && m <= 7 && vertices[k--, m++, 0] == 1) ;
                            if (vertices[k + 1, m - 1, 0] == -1)
                            {
                                for (int j = x - 1, n = z + 1; j > k + 1 && n < m - 1; j--, n++)
                                {
                                    vertices[j, n, 0] = -1;
                                }
                            }
                            //diagonal U-D
                            p = z - 1;
                            while (p >= 0 && i <= 7 && vertices[i++, p--, 0] == 1) ;
                            if (vertices[i - 1, p + 1, 0] == -1)
                            {
                                for (int j = x + 1, n = z - 1; j < i - 1 && n > p + 1; j++, n--)
                                {
                                    vertices[j, n, 0] = -1;
                                }
                            }
                            p = z - 1;
                            m = z + 1;
                            i = x + 1;
                            k = x - 1;
                            //diagonal D-U
                            while (i <= 7 && m <= 7 && vertices[i++, m++, 0] == 1) ;
                            if (vertices[i - 1, m - 1, 0] == -1)
                            {
                                for (int j = x + 1, n = z + 1; j < i - 1 && n < m - 1; j++, n++)
                                {
                                    vertices[j, n, 0] = -1;
                                }
                            }
                            //diagonal U-D
                            while (k >= 1 && p >= 1 && vertices[k--, p--, 0] == 1) ;
                            if (vertices[k + 1, p + 1, 0] == -1)
                            {
                                for (int j = x - 1, n = z - 1; j > k + 1 && n > p + 1; j--, n--)
                                {
                                    vertices[j, n, 0] = -1;
                                }
                            }
                            val++;
                        }
                    }
                }
                GameObject[] destroy = GameObject.FindGameObjectsWithTag("disc");
                foreach (GameObject wdisc in destroy) { 
                    GameObject.Destroy(wdisc);
                    //Debug.Log("Yes");
                }
                GameObject[] destroy2 = GameObject.FindGameObjectsWithTag("highlight");
                foreach (GameObject hlight in destroy2)
                {
                    GameObject.Destroy(hlight);
                    //Debug.Log("Yes");
                }
                ValidMove(vertices,val);
                int sum = 0;
                for (int x = -4; x <= +3; x++)
                {
                    for (int z = -4; z <= 3; z++)
                    {
                        if (vertices[x + 4, z + 4, 1] == 1) { Instantiate(highlight, new Vector3(x+0.5f, 0.3f, z+0.5f), Quaternion.identity); sum++; }
                        if (vertices[x + 4, z + 4, 0] == 1)
                        {
                            Instantiate(disc, new Vector3(x + 0.5f, 0.375f, z + 0.5f), Quaternion.identity);
                        }
                        if (vertices[x + 4, z + 4, 0] == -1)
                        {
                            Instantiate(disc, new Vector3(x + 0.5f, 0.375f, z + 0.5f), Quaternion.Euler(180, 0, 0));
                        }
                    }
                }
                if (sum != 0) { end = 0; }
                if (sum == 0) 
                {
                    if (val % 2 == 0)
                    { Debug.Log("No Valid Move For Black"); end++; }
                    if (val % 2 != 0)
                    { Debug.Log("No Valid Move For White"); end++; }
                    val++; 
                }
                if (end == 2) 
                {
                    if (black > white) { SceneManager.LoadScene("Win"); }
                    if (black < white) { SceneManager.LoadScene("Lose"); }
                    if (black == white) { SceneManager.LoadScene("Draw"); }
                }
            }
        }
        if (ModeSelector.Mode == 1 && val%2!=0)
        {
            Move bestMove = findBestMove(vertices,val);

            Console.Write("The Optimal Move is :\n");
            Console.Write("ROW: {0} COL: {1}\n\n",
                    bestMove.row, bestMove.col);
            //Instantiate(disc, new Vector3(bestMove.row - 3.5f, 0.375f, bestMove.col - 3.5f), Quaternion.Euler(180, 0, 0));
            int x = bestMove.row, z = bestMove.col;
            vertices[x,z,0] = -1;
            //Debug.Log(vertices[x, z, 0]);
            //rows L-R
            int i = x + 1;
            while (i <= 7 && vertices[i++, z, 0] == 1) ;
            if (vertices[i - 1, z, 0] == -1)
            {
                for (int j = x + 1; j < i - 1; j++)
                {
                    vertices[j, z, 0] = -1;
                }
            }
            //rows R-L
            int k = x - 1;
            while (k >= 0 && vertices[k--, z, 0] == 1) ;
            if (vertices[k + 1, z, 0] == -1)
            {
                for (int j = x - 1; j > k + 1; j--)
                {
                    vertices[j, z, 0] = -1;
                }
            }
            i = x + 1;
            k = x - 1;
            int m = z + 1;
            //coloumn B-T
            while (m <= 7 && vertices[x, m++, 0] == 1) ;
            if (vertices[x, m - 1, 0] == -1)
            {
                for (int j = z + 1; j < m - 1; j++)
                {
                    vertices[x, j, 0] = -1;
                }
            }
            int p = z - 1;
            //coloumn T-B
            while (p >= 0 && vertices[x, p--, 0] == 1) ;
            if (vertices[x, p + 1, 0] == -1)
            {
                for (int j = z - 1; j > p + 1; j--)
                {
                    vertices[x, j, 0] = -1;
                }
            }
            i = x + 1;
            k = x - 1;
            //diagonal D-U
            m = z + 1;
            while (k >= 0 && m <= 7 && vertices[k--, m++, 0] == 1) ;
            if (vertices[k + 1, m - 1, 0] == -1)
            {
                for (int j = x - 1, n = z + 1; j > k + 1 && n < m - 1; j--, n++)
                {
                    vertices[j, n, 0] = -1;
                }
            }
            //diagonal U-D
            p = z - 1;
            while (p >= 0 && i <= 7 && vertices[i++, p--, 0] == 1) ;
            if (vertices[i - 1, p + 1, 0] == -1)
            {
                for (int j = x + 1, n = z - 1; j < i - 1 && n > p + 1; j++, n--)
                {
                    vertices[j, n, 0] = -1;
                }
            }
            p = z - 1;
            m = z + 1;
            i = x + 1;
            k = x - 1;
            //diagonal D-U
            while (i <= 7 && m <= 7 && vertices[i++, m++, 0] == 1) ;
            if (vertices[i - 1, m - 1, 0] == -1)
            {
                for (int j = x + 1, n = z + 1; j < i - 1 && n < m - 1; j++, n++)
                {
                    vertices[j, n, 0] = -1;
                }
            }
            //diagonal U-D
            while (k >= 1 && p >= 1 && vertices[k--, p--, 0] == 1) ;
            if (vertices[k + 1, p + 1, 0] == -1)
            {
                for (int j = x - 1, n = z - 1; j > k + 1 && n > p + 1; j--, n--)
                {
                    vertices[j, n, 0] = -1;
                }
            }

            StartCoroutine(Delay());
            val++;
            GameObject[] destroy = GameObject.FindGameObjectsWithTag("disc");
            foreach (GameObject wdisc in destroy)
            {
                GameObject.Destroy(wdisc);
                //Debug.Log("Yes");
            }
            GameObject[] destroy2 = GameObject.FindGameObjectsWithTag("highlight");
            foreach (GameObject hlight in destroy2)
            {
                GameObject.Destroy(hlight);
                //Debug.Log("Yes");
            }
            ValidMove(vertices, val);
            int sum = 0;
            for (int a = -4; a <= +3; a++)
            {
                for (int b = -4; b <= 3; b++)
                {
                    if (vertices[a + 4, b + 4, 1] == 1) { Instantiate(highlight, new Vector3(a + 0.5f, 0.3f, b + 0.5f), Quaternion.identity); sum++; }
                    if (vertices[a + 4, b + 4, 0] == 1)
                    {
                        Instantiate(disc, new Vector3(a + 0.5f, 0.375f, b + 0.5f), Quaternion.identity);
                    }
                    if (vertices[a + 4, b + 4, 0] == -1)
                    {
                        Instantiate(disc, new Vector3(a + 0.5f, 0.375f, b + 0.5f), Quaternion.Euler(180, 0, 0));
                    }
                }
            }
            if (sum != 0) { end = 0; }
            if (sum == 0)
            {
                if (val % 2 == 0)
                { Debug.Log("No Valid Move For Black"); end++; }
                if (val % 2 != 0)
                { Debug.Log("No Valid Move For White"); end++; }
                val++;
            }
            if (end == 2)
            {
                if (black > white) { SceneManager.LoadScene("Win"); }
                if (black < white) { SceneManager.LoadScene("Lose"); }
                if (black == white) { SceneManager.LoadScene("Draw"); }
            }

        }
    }

    IEnumerator Delay() 
    {
        yield return new WaitForSeconds(5); 
    }

    // This will return the best possible
    // move for the player
    Move findBestMove(int[,,] board, int value)
    {
        int bestVal = -1000;
        Move bestMove = new Move();
        bestMove.row = -1;
        bestMove.col = -1;

        // Traverse all cells, evaluate minimax function
        // for all empty cells. And return the cell
        // with optimal value.
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                // Check if cell is valid
                if (board[i, j,1] == 1)
                {
                    // Make the move
                    board[i, j,0] = -1;
                    ValidMove(board, value);
                    // compute evaluation function for this
                    // move.
                    int moveVal = minimax(board, 0, value);

                    // Undo the move
                    board[i, j,0] = 0;
                    ValidMove(board, value);
                    // If the value of the current move is
                    // more than the best value, then update
                    // best/
                    if (moveVal > bestVal)
                    {
                        bestMove.row = i;
                        bestMove.col = j;
                        bestVal = moveVal;
                    }
                }
            }
        }

        Debug.Log(bestVal);

        return bestMove;
    }







    class Move
    {
        public int row, col;
    }

    // This function returns true if there are moves
    // remaining on the board. It returns false if
    // there are no moves left to play.
    Boolean isMovesLeft(int[,,] board)
    {
        for (int i = 0; i < 8; i++)
            for (int j = 0; j < 8; j++)
                if (board[i, j,1] == 1)
                    return true;
        return false;
    }
          

    // This is the minimax function. It considers all
    // the possible ways the game can go and returns
    // the value of the board
    int minimax(int[,,] board,
                    int depth, int value)
    {
        int score = Score(board);
        Ourscore = 0;
        OpponentScore = 0;
        if (!isMovesLeft(board)) { return score; }
        if (depth == GameSpecification.Gamediff+1)
            return score;

        // If this maximizer's move
        if (value %2 ==0)
        {
            int best = -1000;

            // Traverse all cells
            for (int z = 0; z < 8; z++)
            {
                for (int x = 0; x < 8; x++)
                {
                    // Check if cell is empty
                    if (board[x, z,1] == 1)
                    {
                        // Make the move
                        board[x, z,0] = 1;
                        ValidMove(board, value);

                        // Call minimax recursively and choose
                        // the maximum value
                        best = Math.Max(best, minimax(board,
                                        depth + 1, value+1));

                        // Undo the move
                        board[x, z, 0] = 0;
                        ValidMove(board, value);
                    }
                }
            }
            return best;
        }

        // If this minimizer's move
        else
        {
            int best = 1000;

            // Traverse all cells
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    // Check if cell is empty
                    if (board[i, j, 1] == 1)
                    {
                        // Make the move
                        board[i, j, 0] = -1;
                        ValidMove(board, value);

                        // Call minimax recursively and choose
                        // the minimum value
                        best = Math.Min(best, minimax(board,
                                        depth + 1, value+1));

                        // Undo the move
                        board[i, j, 0] = 0;
                        ValidMove(board, value);
                    }
                }
            }
            return best;
        }
    }
}


