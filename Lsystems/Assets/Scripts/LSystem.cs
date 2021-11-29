using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class TransformInfo
{
    ////https://www.youtube.com/watch?v=tUbTGWl-qus
    //stores position and rotation for the stack
    public Vector3 position;
    public Quaternion rotation;
}

public class LSystem : MonoBehaviour
{
    [SerializeField] private GameObject leaf;
    [SerializeField] private GameObject branch;
    [SerializeField] private float length = 5;
    [SerializeField] private double angle = 20;
    private Stack<TransformInfo> _stack;
    private Dictionary<char, string> _rules; //dictionary holds the rules of the tree
   // public Quaternion angle;
    private String _currentSentence;
    
    //variables for the dictionary

    private string _axiom = "X"; //holds the initial position of the tree

    // private string _newSentence; //holds the current sentence
    private StringBuilder _stringBuilder;
    private Vector3 _resetPosition;
    
    private int _iterations = 7; //number of iterations
    public Queue<GameObject> testGameLines = new Queue<GameObject>();

    private GameObject _treeBranch;
    private bool _iterationIncrements;
    
    private float speed = 50.0f;

    private bool _angleButtonPressed;
  
  //UI text

  public Text iterationsText;
  public Text treeNumber;
  
  

    // Start is called before the first frame update
    private void Start()
    {
       _stack = new Stack<TransformInfo>(); //initialises the stack
       _rules = new Dictionary<char, string> //initialises the rules and includes the first rules to dictionary
        {
            {'F', "FF"}, //replace rules with variables
            {'X', "F[+X][-X]FX"}
        };
        _stringBuilder = new StringBuilder();

        _angleButtonPressed = false;
        
        //generates tree
        GenerateTree();
        iterationsText.text = "this is the " + _iterations + " iteration";
    }

    public void Update()
    {
        
        _iterationIncrements = false;

        ChangeScene(); //changes the scene 
        if (Input.GetKeyDown(KeyCode.D))
        {
            _iterationIncrements = true;

            if (_iterationIncrements && _iterations <= 6)
            {
                _iterations++;

                //goes through the queue and deletes the objects
                foreach (var o in testGameLines) //destroys lines
                {
                    Destroy(o);
                   
                    transform.position = new Vector3(0, 0, 0); //sets the object's position to 0
                    _iterationIncrements = false; //ensures that it doesn't continue
                }

                GenerateTree();
            }
            else
            {
                _iterations = 7;
            }

        }

        //
        //
        //
        //go back one iteration
        if (Input.GetKeyDown(KeyCode.A))
        {
            _iterationIncrements = true;

            if (_iterationIncrements && _iterations >= 1)
            {
                _iterations--;

                foreach (var o in testGameLines) //destroys lines
                {
                    Destroy(o);
                    transform.position = new Vector3(0, 0, 0);
                    _iterationIncrements = false;
                }

                GenerateTree();
            }
            else
            {
                _iterations = 1;
            }




        }


        if (_angleButtonPressed)
        {
            GenerateTree();
        }
    }

    //end of update
 

    public void GenerateTree()
    {
        
        _currentSentence = _axiom; //assigns the value of "F" to newsentence

        Debug.Log(_currentSentence);
        for(int i = 0; i < _iterations; i++){
            iterationsText.text = "this is the " + _iterations + " iteration";
            {
                foreach (char c in _currentSentence)
                {
                    //Loops through all the values in the _newsentence variable
                    if (_rules.ContainsKey(c)) //if the rules contain this character in c
                    {
                        _stringBuilder.Append(_rules[c]);

                    }
                    else
                    {

                        _stringBuilder.Append(c.ToString());
                    }

                }
            }


            _currentSentence = _stringBuilder.ToString(); //stores the new values into the currentstring
            _stringBuilder = new StringBuilder();
            Debug.Log(_currentSentence);
        }
        
        BuildTree();


    }

    public void BuildTree()
    {
        transform.rotation = Quaternion.Euler(0,0,0) ;

        //loops through each character in the new sentence and carries out an action depending on the char found
        foreach (char c in _currentSentence)
        {
            switch (c)
            {
                case 'F':
                    Vector3 initialPosition = transform.position;
                    
                    
                    transform.Translate(Vector3.up * length);
                    
                    _treeBranch = Instantiate(branch); //instantiates the branch
                    
                    testGameLines.Enqueue(_treeBranch); //queues up the branches 
                    
                   
                    _treeBranch.GetComponent<LineRenderer>().SetPosition(0, initialPosition);
                    _treeBranch.GetComponent<LineRenderer>().SetPosition(1, transform.position);
                    
                    break;
                
                case 'X':
                    break;
                
                case '+':
                    transform.Rotate(Vector3.back * (float) angle);
                    break;

                case '-':
                    transform.Rotate(Vector3.forward * (float) angle);
                    break;
                
                case '[':
                    //saving transform info into stack
                    _stack.Push(new TransformInfo()
                    {
                        position = transform.position,
                        rotation = transform.rotation
                    });
                    break;

                case ']':
                    //returns stack to saved positions in stack
                    TransformInfo ti = _stack.Pop();
                    transform.position = ti.position;
                    transform.rotation = ti.rotation;
                    break;

                default:
                    throw new InvalidOperationException("Invalid L-Tree operation");
            }


        }
    }
    public void ChangeScene()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //cleans up the scene
            ClearTree();
            
           // transform.position = new Vector3(0, 0, 0);
         


            _iterations = 5;
            angle = 25.7; 
            _axiom = "F";
            
            _rules.Clear();
            _rules.Add('F', "F[+F]F[-F]F");

            treeNumber.text = "This is tree 1";
            GenerateTree();



        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //cleans up the scene
            ClearTree();
         //   transform.position = new Vector3(0, 0, 0);
          



            _iterations = 5;
            angle = 20; 
            _axiom = "F";
            
            _rules.Clear();
            _rules.Add('F', "F[+F]F[-F][F]");
            
            treeNumber.text = "This is tree 2";
            
            GenerateTree();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //cleans up the scene
            ClearTree();
          //  transform.position = new Vector3(0, 0, 0);
          


            _iterations = 4;
            angle = 22.5; 
            _axiom = "F";
            
            _rules.Clear();
            _rules.Add('F', "FF-[-F+F+F]+[+F-F-F]");
            
            treeNumber.text = "This is tree 3";
            GenerateTree();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //cleans up the scene
            ClearTree();

          //  transform.position = new Vector3(0, 0, 0);
          

            
            _iterations = 7;
            angle = 20; 
            _axiom = "X";
            
            _rules.Clear();
            _rules.Add('X', "F[+X]F[-X]+X");
            _rules.Add('F', "FF");
            
            treeNumber.text = "This is tree 4";
            GenerateTree();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            //cleans up the scene
            ClearTree();
           // transform.position = new Vector3(0, 0, 0);

            
            _iterations = 7;
            angle = 25.7; 
            _axiom = "X";
            
            _rules.Clear();
            _rules.Add('X', "F[+X][-X]FX");
            _rules.Add('F', "FF");
            
            treeNumber.text = "This is tree 5";
            GenerateTree();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            //cleans up the scene
            ClearTree();
          //  transform.position = new Vector3(0, 0, 0);
           



            _iterations = 5;
            angle = 22.5; 
            _axiom = "X";
            
            _rules.Clear();
            _rules.Add('X', "F-[[X]+X]+F[+FX]-X");
            _rules.Add('F', "FF");
            
            treeNumber.text = "This is tree 6";
            
            GenerateTree();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            
            //cleans up the scene
            ClearTree();
          //  transform.position = new Vector3(0, 0, 0);


//http://paulbourke.net/fractals/lsys/
            _iterations = 5;
            angle = 35; 
            _axiom = "F";
            
            _rules.Clear();
            _rules.Add('F', "F[+FF][-FF]F[-F][+F]F");
            
            treeNumber.text = "This is tree 7";
            
            GenerateTree();
            
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            //cleans up the scene
            ClearTree();
          //  transform.position = new Vector3(0, 0, 0);


            //algorithmicbotany.org/papers/abop/abop.pdf
            _iterations = 4;
            angle = 90; 
            _axiom = "F-F-F-F";
            
            _rules.Clear();
            _rules.Add('F', "FF-F-F-F-F-F+F");
            
            treeNumber.text = "This is tree 8";
            
            GenerateTree();
        }
    }

    
    //increment parameters
    public void IncrementAngle()
    {
        _angleButtonPressed = true;
        angle += 5;

    }

    public void IncrementLength()
    {
        length += 2;
    }

    //clears the instantiated branches to allow for a redraw
    private void ClearTree()
    {
        foreach (var o in testGameLines) //destroys lines
        {
            Destroy(o);
            transform.position = new Vector3(0, 0, 0);
               
        }
    }

 
}
