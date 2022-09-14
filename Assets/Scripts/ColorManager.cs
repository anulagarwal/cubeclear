using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorManager : MonoBehaviour
{

    public static ColorManager Instance = null;

    [Header("Attributes")]
    [SerializeField] Color cubeRed;
    [SerializeField] Color cubeBlue;
    [SerializeField] Color cubeYellow;
    [SerializeField] Color cubeGreen;



    [SerializeField] Color blockRed;
    [SerializeField] Color blockBlue;
    [SerializeField] Color blockYellow;
    [SerializeField] Color blockGreen;

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
            case ColorType.Yellow:
                return blockYellow;
            case ColorType.Green:
                return blockGreen;
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

            case ColorType.Yellow:
                return cubeYellow;
            case ColorType.Green:
                return cubeGreen;
            default:
                return Color.white;
        }

    }
}
