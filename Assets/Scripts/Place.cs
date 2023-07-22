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
        ValidMove();
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
                Debug.Log("1");
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
                    }
                    //diagonal U-D
                    if (x >= 3 && z <= 6 && vertices[x - 1, z - 1, 0] == 1 && vertices[x - 2, z, 0] == -1)
                    {
                        int i = x - 2, j = z;
                        while (i >= 0 && j <= 7 && vertices[i--, j++, 0] == -1) ;
                        if (vertices[i + 1, j - 1, 0] == 0)
                        {
                            vertices[i + 1, j - 1, 1] = 1;
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
                    }
                }
            }
        }

    }

    void Update()
    {
        Count();
        //turn();
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.collider.tag == "cube" && vertices[(int)(hit.collider.transform.position.x + 3.5), (int)(hit.collider.transform.position.z + 3.5), 1] == 1)
                { if (val % 2 == 0)
                    {
                        //Instantiate(disc, new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y + 0.375f, hit.collider.transform.position.z), Quaternion.identity);
                        vertices[(int)(hit.collider.transform.position.x + 3.5), (int)(hit.collider.transform.position.z + 3.5), 0] = 1;
                        int x = (int)(hit.collider.transform.position.x + 3.5f), z = (int)(hit.collider.transform.position.z + 3.5f);
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
                        val++;
                    }
                    else if (val % 2 != 0)
                    {
                        //Instantiate(disc, new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y + 0.375f, hit.collider.transform.position.z), Quaternion.Euler(180, 0, 0));
                        vertices[(int)(hit.collider.transform.position.x + 3.5), (int)(hit.collider.transform.position.z + 3.5), 0] = -1;
                        int x = (int)(hit.collider.transform.position.x + 3.5f), z = (int)(hit.collider.transform.position.z + 3.5f);
                        //rows L-R
                        int i = x + 1;
                        while (i <= 7 && vertices[i++, z, 0] == 1) ;
                        if (vertices[i - 1, z, 0] == -1)
                        {
                            for (int j = x + 1; j < i-1; j++)
                            {
                                vertices[j, z, 0] = -1;
                            }
                        }
                        //rows R-L
                        int k = x - 1;
                        while (k >= 0 && vertices[k--, z, 0] == 1) ;
                        if (vertices[k + 1, z, 0] == -1)
                        {
                            for (int j = x - 1; j > k+1; j--)
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
                            for (int j = z + 1; j < m -1; j++)
                            {
                                vertices[x, j, 0] = -1;
                            }
                        }
                        int p = z - 1;
                        //coloumn T-B
                        while (p >= 0 && vertices[x, p--, 0] == 1) ;
                        if (vertices[x, p + 1, 0] == -1)
                        {
                            for (int j = z - 1; j > p+1; j--)
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
                            for (int j = x - 1, n = z + 1; j > k+1 && n < m-1; j--, n++)
                            {
                                vertices[j, n, 0] = -1;
                            }
                        }
                        //diagonal U-D
                        p = z - 1;
                        while (p >= 0 && i <= 7 && vertices[i++, p--, 0] == 1) ;
                        if (vertices[i - 1, p + 1, 0] == -1)
                        {
                            for (int j = x + 1, n = z - 1; j < i - 1 && n > p+1; j++, n--)
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
                            for (int j = x + 1, n = z + 1; j < i - 1 && n < m-1; j++, n++)
                            {
                                vertices[j, n, 0] = -1;
                            }
                        }
                        //diagonal U-D
                        while (k >= 1 && p >= 1 && vertices[k--, p--, 0] == 1) ;
                        if (vertices[k + 1, p + 1, 0] == -1)
                        {
                            for (int j = x - 1, n = z - 1; j > k + 1 && n > p+1; j--, n--)
                            {
                                vertices[j, n, 0] = -1;
                            }
                        }
                        val++;
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
                    Debug.Log("Yes");
                }
                ValidMove();
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
    }
}
