using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Row
{
    public GameObject[] data;
}

public class GridManager : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] float speed;
    [SerializeField] public Tile previousTile;
    [SerializeField] int red;
    [SerializeField] int blue;
    [SerializeField] int green;
    [SerializeField] int yellow;
    [SerializeField] Vector2 baseOffset;
    [SerializeField] float cubeDistance;


    [Header("Component References")]
    [SerializeField] List<Tile> tiles;
    [SerializeField] GameObject cube;   
    public Row[] gridLayout;



    public static GridManager Instance = null;

    

    private void Awake()
    {
        Application.targetFrameRate = 100;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach(Tile t in GetComponentsInChildren<Tile>())
        {
            tiles.Add(t);
        }
        GenerateTiles();
        StackManager.Instance.GenerateCubes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateTiles()
    {
        for(int y = 0; y < gridLayout.Length;y++)
        {
            for(int x =0; x < gridLayout[y].data.Length; x++)
            {
                //GameObject g = Instantiate(cube, new Vector3(baseOffset.x, 0, baseOffset.y), Quaternion.identity);
                //tiles.Add(g.GetComponent<Tile>());
                gridLayout[y].data[x].GetComponent<Tile>().x = x;
                gridLayout[y].data[x].GetComponent<Tile>().y = y;
            }
        }
    }

    public void ClearSelection()
    {
        previousTile = null;
    }
    public void SelectTile(Tile t)
    {
        previousTile = t;
    }
    public bool IsAdjacent(Tile t1, Tile t2)
    {

        if(Mathf.Abs(t1.x - t2.x) <=1 && Mathf.Abs(t1.y - t2.y) <= 1)
        {
            if(Mathf.Abs(t1.x - t2.x) ==1 && Mathf.Abs(t1.y-t2.y) == 1)
            {
                return false;
            }
            return true;
        }
        return false;
    }
    public void EnterTile(Tile t)
    {
        if (previousTile != null && t!=previousTile && IsAdjacent(previousTile,t))
        {
            if (t.color == previousTile.color)
            {
                t.AddTile(previousTile);
                previousTile.ClearTile();
                previousTile = t;
                if (t.cube != null)
                {
                    StackManager.Instance.SelectCube(t.cube);

                }
                int total=0;
                foreach(Tile t1 in tiles)
                {
                    if(t1.color == t.color)
                    {
                        total += t1.health;
                    }
                }
                if(t.cube.childCount+1== total)
                {
                    //t.cube.GetComponent<Cube>().PlayShake();
                }
                CheckWin();
            }
            if(t.color == ColorType.White && previousTile.health>1)
            {
                //StackManager.Instance.StackOnEmpty(t);
                previousTile.health--;
                t.AddTile(previousTile);
                previousTile.ClearTile();
                previousTile.AddCube(t);
                previousTile = t;
                CheckWin();
            }
        }
    }

    public void GenerateCubes()
    {
        foreach(Tile t in tiles)
        {

        }
    }
    void CheckWin()
    {
        int r = 0;
        int g = 0;
        int b = 0;
        int y = 0;
        foreach (Tile t in tiles)
        {
            switch (t.color)
            {
                case ColorType.Red:
                    r++;
                    break;

                case ColorType.Blue:
                    b++;
                    break;

                case ColorType.Yellow:
                    y++;
                    break;
                case ColorType.Green:
                    g++;
                    break;
            }
            
        }
        if (r == red && b == blue && y== yellow && g == green)
        {
            GameManager.Instance.WinLevel();
        }
    }
    public List<Tile> GetTiles()
    {
        return tiles;
    }
}
