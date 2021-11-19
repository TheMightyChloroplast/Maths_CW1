using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSystem : MonoBehaviour
{
    private string _axiom = "F"; //holds the initial position
    private Dictionary<char, string> _rules =  new Dictionary<char, string>(); //dictionary holds the rules of the tree
    private string _newSentence; //holds the current sentence
    [SerializeField]
    private int _iterations ; //number of iterations
    
    
    // Start is called before the first frame update
    private void Start()
    {
        _iterations = 1;
        _rules.Add('F',"F[+F]F[-F]F");
        _newSentence = _axiom; //starting point is the axiom
        
        
    }

    // Update is called once per frame
    private void Update()
    {
        GenerateTree();
    }
    

    private void GenerateTree()
    {
        for (int i = 0; i <= _iterations; i++)
        {
            foreach (var c in _axiom)
            {
                if (c == 'F')
                {
                    
                    Debug.Log(c);
                    Debug.Log(_axiom);
                    
                    _axiom = _axiom.Replace("F", "F[+F]F[-F]F");
                    
                }
            }
        }

    }
}
