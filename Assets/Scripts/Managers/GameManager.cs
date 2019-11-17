using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataStructures;


public class GameManager : MonoBehaviour {


    public Camera gameCamera;

    BoardManager boardManager;
    

    void Awake()
    {
        boardManager = Object.FindObjectOfType<BoardManager>();
        

    }
 

 
}
