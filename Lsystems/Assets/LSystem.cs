using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class LSystem : MonoBehaviour
{
    private string _axiom;
    private string _rules;
    private string _newAxiom;
    private int _n ;
    
    // Start is called before the first frame update
    private void Start()
    {
        _n = 1;
        _axiom = "F";
        _rules = "F[+F]F[-F]F";
        
    }

    // Update is called once per frame
    private void Update()
    {
        GenerateTree();
    }
    

    private void GenerateTree()
    {
        for (int i = 0; i <= _n; i++)
        {
            foreach (var c in _axiom)
            {
                if (c == 'F')
                {
                    Debug.Log(_n);
                    Debug.Log(c);
                    Debug.Log(_axiom);
                    
                    _axiom = _axiom.Replace("F", "F[+F]F[-F]F");
                    
                }
            }
        }

    }
}
