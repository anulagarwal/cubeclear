using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] float yOffset;
    [SerializeField] float baseYOffset;


    [Header("Component References")]
    [SerializeField] Transform selectedCube;
    [SerializeField] GameObject cube;
    [SerializeField] List<Cube> cubes;


    public static StackManager Instance = null;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectCube(Transform c)
    {
        selectedCube = c;
    }

    public void StackOnSelected(Transform t)
    {

        selectedCube.SetParent(t);
        selectedCube.localPosition = new Vector3(0,  ((t.childCount ) *( yOffset)), 0);
        foreach (Transform tr in selectedCube.GetComponentsInChildren<Transform>())
        {
            tr.SetParent(t);
        }
        selectedCube = t;
        SoundManager.Instance.Play(Sound.Pop);
    }
    public void StackOnEmpty(Tile t)
    {
        selectedCube.position = new Vector3(0, baseYOffset, 0);
        selectedCube.SetParent(t.transform);
        selectedCube.localPosition = new Vector3(0, baseYOffset, 0);
        t.SetCube(selectedCube);
        SoundManager.Instance.Play(Sound.Pop);

    }

    public void RemoveFromSelected(Tile tile)
    {
        tile.SetCube(selectedCube.GetChild(selectedCube.childCount - 1));
        selectedCube.GetChild(selectedCube.childCount-1).SetParent(tile.transform);
        tile.cube.localPosition = new Vector3(0, baseYOffset, 0);
    }
    public void AddCube(Tile t)
    {
       
    }
    public void GenerateCubes()
    {
        foreach (Tile t in GridManager.Instance.GetTiles())
        {
            if (t.health > 0)
            {
                for(int i = 0; i< t.health; i++)
                {
                    GameObject g= Instantiate(cube, Vector3.zero, Quaternion.identity);
                    if (t.cube == null)
                    {
                        g.transform.SetParent(t.transform);
                        g.transform.localPosition = new Vector3(0, baseYOffset + (i * yOffset), 0);
                        t.cube = g.transform;
                    }
                    else
                    {
                        g.transform.SetParent(t.cube);
                        g.transform.localPosition = new Vector3(0, ((i) * yOffset), 0);

                    }
                    g.GetComponent<MeshRenderer>().material.color = ColorManager.Instance.GetColCube(t.color);
                    cubes.Add(g.GetComponent<Cube>());
                }
            }
        }

        Invoke("Shake", 0.05f);

    }

    void Shake()
    {
        foreach (Cube c in cubes)
        {
            c.PlayShake();
        }
    }
}
