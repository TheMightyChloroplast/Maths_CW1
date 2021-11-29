using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;





public class TransformInfo2
{
    ////https://www.youtube.com/watch?v=tUbTGWl-qus
    //stores position and rotation for the stack
    public Vector3 position;
    public Quaternion rotation;
}

public class LSystem2 : MonoBehaviour
{
     [SerializeField] private GameObject branch;
       [SerializeField] private float length = 5;
       [SerializeField] private double angle = 20;
       private Stack<TransformInfo> _stack;
       private Dictionary<char, string> _rules; //dictionary holds the rules of the tree
      // public Quaternion angle;
       private String _currentSentence;
       public String test;
       public char test2;
   
       private string _axiom = "X"; //holds the initial position of the tree
   
       // private string _newSentence; //holds the current sentence
       private StringBuilder _stringBuilder;
       
       private int _iterations = 4; //number of iterations
       public GameObject[] test5;
       
     //  public GameObject inputField;
     //  private string theNumber;
     private int _numtest2;
     
   
   
   
       // Start is called before the first frame update
       private void Start()
       {
          _stack = new Stack<TransformInfo>(); //initialises the stack
          _rules = new Dictionary<char, string> //initialises the rules and includes the first rules to dictionary
           {
               {test2, test}, //replace rules with variables
               {'X', "F[+X][-X]FX"}
           };
           _stringBuilder = new StringBuilder();
           GenerateTree();
       }
   
       public void Update()
       {
   
           ChangeScene();
           if(Input.GetKeyDown(KeyCode.A))
           {
               _numtest2++;
               Debug.Log(_numtest2);
               
           }
           
           
           
          
       }
   
       public void GenerateNewTree()
       {
           if (_numtest2 == 6)
           {
               foreach (var o in GameObject.FindGameObjectsWithTag("Branch")) Destroy(o);
           }
   
           if (_numtest2 == 8)
           {
               GenerateTree();
           }
   
       }
   
       public void GenerateTree()
       {
   
        // theNumber = inputField.GetComponent<Text>().text;
         //  _iterations = int.Parse(theNumber); //See if i can get this hotkey working
           _currentSentence = _axiom; //assigns the value of "F" to newsentence
   
           Debug.Log(_currentSentence);
           for(int i = 0; i < _iterations; i++){
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
           //loops through each character in the new sentence and carries out an action depending on the char found
           foreach (char c in _currentSentence)
           {
               switch (c)
               {
                   case 'F':
                       Vector3 initialPosition = transform.position;
                       transform.Translate(Vector3.up * length);
                       
                       GameObject  treeBranch = Instantiate(branch);
                       treeBranch.GetComponent<LineRenderer>().SetPosition(0, initialPosition);
                       treeBranch.GetComponent<LineRenderer>().SetPosition(1, transform.position);
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
               SceneManager.LoadScene("Tree1");
           }
           if (Input.GetKeyDown(KeyCode.Alpha2))
           {
               SceneManager.LoadScene("Tree2");
           }
           if (Input.GetKeyDown(KeyCode.Alpha3))
           {
               SceneManager.LoadScene("Tree3");
           }
           if (Input.GetKeyDown(KeyCode.Alpha4))
           {
               SceneManager.LoadScene("Tree4");
           }
           if (Input.GetKeyDown(KeyCode.Alpha5))
           {
               SceneManager.LoadScene("Tree5");
           }
           if (Input.GetKeyDown(KeyCode.Alpha6))
           {
               SceneManager.LoadScene("Tree6");
           }
           if (Input.GetKeyDown(KeyCode.Alpha7))
           {SceneManager.LoadScene("Tree7");
               
           }if (Input.GetKeyDown(KeyCode.Alpha8))
           {
               SceneManager.LoadScene("Tree8");
           }
       }
    // public void StoreNumber()
   //  {
    //     theNumber = inputField.GetComponent<Text>().text;
    //     _iterations = int.Parse(theNumber); //See if i can get this hotkey working
    //     _currentSentence = _axiom;
     //    GenerateTree();
     //    Debug.Log(_iterations);
    // }


}
