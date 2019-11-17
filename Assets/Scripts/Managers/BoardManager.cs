using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using LibFever;
using DataStructures;


[Serializable]
public enum TypeOfMarker
{
    BlackStone,
    WhiteStone,
    Empty
        
}
[Serializable]
public struct ObjectBiblio
{
    [SerializeField] public TypeOfMarker marker_type;
    [SerializeField] public List<GameObject> prefabs;
}
[Serializable]
public struct GridObject
{
    [Range(0, 7)]
    public int x;
    [Range(0, 7)]
    public int y;
    [SerializeField] public TypeOfMarker obstacle_type;
}




public class BoardManager : MonoBehaviour
{
    
    private Board GameBoard;
    public void Teleport(Transform tran, Point p, float z)
    {
            GameBoard.Teleport(tran, p, z);
    }
    private void Start()
    {
        GameBoard = UnityEngine.Object.FindObjectOfType<Board>();
        if(GameBoard == null)
        {
            Debug.LogError("BoardManager: GameBoard is Null");
        }
    }

}
