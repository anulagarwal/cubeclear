using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Tile : MonoBehaviour
{
    [Header("Attributes")]
    public ColorType color;
    public int health;
    public int x;
    public int y;


    [Header("Component References")]
    public MeshRenderer mesh;
    public TextMeshPro text;
    public Transform cube;

    private void Start()
    {
        UpdateHealth(health);       
    }
    private void OnMouseEnter()
    {
        //mesh.material.color = Color.red;
        
            GetComponentInParent<GridManager>().EnterTile(this);      
        
    }

    private void OnMouseExit()
    {
        //mesh.material.color = Color.white;
    }

    private void OnMouseDown()
    {
        if (color != ColorType.White && health > 0)
        {
            GetComponentInParent<GridManager>().SelectTile(this);
            StackManager.Instance.SelectCube(cube);
        }
    }

    private void OnMouseUp()
    {
        GetComponentInParent<GridManager>().ClearSelection();
    }
    void UpdateHealth(int h)
    {
        health = h;
        if(health == 0)
        {
            text.text = "";
            color = ColorType.White;
        }
        else
        {
            text.text = health + "";
        }
        mesh.material.color = ColorManager.Instance.GetCol(color);

    }

    public void AddTile(Tile t)
    {
        UpdateHealth(health + t.health);
        color = t.color;
        mesh.material.color = ColorManager.Instance.GetCol(color);
        if (cube != null)
        {
            StackManager.Instance.StackOnSelected(cube);
        }
        else
        {
            StackManager.Instance.StackOnEmpty(this);
            
        }
    }
    public void AddCube(Tile t)
    {
        StackManager.Instance.RemoveFromSelected(this);
        UpdateHealth(1);
        color = t.color;
        mesh.material.color = ColorManager.Instance.GetCol(color);            
    }

    public void SetCube(Transform c)
    {
        cube = c;
    }
    public void ClearTile()
    {
        UpdateHealth(0);
        cube = null;
    }

    public void AddCube(Transform c)
    {
        cube = c;       
    }
}
