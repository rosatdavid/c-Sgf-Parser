using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
public class GameNode
{
    private GameNode parent;
    private List<GameNode> childrens;
    private string value;
    private string color;
    public string position;
    public GameNode(string val)
    {
        
        value = val;
//        Match colorandpos = new Regex(@"(B|W)\[(.*?)\]").Match(val);

        color =  "PLACE HOLDER"; //colorandpos.Groups[1].Value;
        position ="PLACE HOLDER";//colorandpos.Groups[2].Value;
        childrens = new List<GameNode>();
    }
    public GameNode GetParent()
    {
        return parent;
    }
    public List<GameNode> GetChidrens()
    {
        return childrens;
    }
    public string GetPosition()
    {
        return position;
    }
    public string GetValue()
    {
        return value;
    }
    public void AddChildren(GameNode ch)
    {
        childrens.Add(ch);
    }
    public void SetParent(GameNode p)
    {
        parent = p;
    }
}
