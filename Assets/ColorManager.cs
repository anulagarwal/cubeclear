using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{

    public static ColorManager Instance = null;

    [Header("Attributes")]
    [SerializeField] Color cubeRed;
    [SerializeField] Color cubeBlue;
    [SerializeField] Color blockRed;
    [SerializeField] Color blockBlue;
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

    public Color GetCol(ColorType c)
    {
        switch (c)
        {
            case ColorType.Blue:
                return blockBlue;

            case ColorType.Red:
                return blockRed;
            default:
                return Color.white;               
        }

    }
    public Color GetColCube(ColorType c)
    {
        switch (c)
        {
            case ColorType.Blue:
                return cubeBlue;

            case ColorType.Red:
                return cubeRed;
            default:
                return Color.white;
        }

    }
}
