using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private Material _material;
    
    void Start()
    {
        _material = GetComponent<Renderer>().material;
    }

    public void TurnRed()
    {
        _material.color = Color.red;
    }
    
    public void TurnGreen()
    {
        _material.color = Color.green;
    }
    
    public void TurnBlue()
    {
        _material.color = Color.blue;
    }
}
