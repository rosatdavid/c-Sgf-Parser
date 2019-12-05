using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationTree : MonoBehaviour
{

    private Game _game;
    private GameNode _current;
    public GameObject lineHolder;
    

    public NavigationTree(Game g)
    {
        _game = g;
        _current = g.GetRoot();
    }


    public void DrawTest()
    {
        var variations = Game.Variation.GetVariations();
        for(int x = 0;x < variations.Count;x++)
        {
            GameObject go = GameObject.Instantiate(lineHolder,this.transform);

            LineRenderer line = go.GetComponent<LineRenderer>();
            line.SetPosition(0,new Vector3(x*0.5f,0f));
            line.SetPosition(1,new Vector3(x*0.5f,0.5f*(float)variations[x].GetLength()));
        }
    }

    public class TreePoint
    {
        int _x;
        int _y;
        public TreePoint(int x, int y)
        {
            _x=x;
            _y = y;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
