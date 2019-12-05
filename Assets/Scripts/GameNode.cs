using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;

public class GameNode
{
    private GameNode parent;
    private List<GameNode> childrens;
    private string value;
    private string color;
    private string position;
    private Game.Variation _variation;

    private static string elementOfNodePatern = @"(?<key>[A-Z]{1,2})(\[(?<value>.*?)\]){1,}(?=[A-Z]{1,2}|$)";   //@"(?<key>[A-Z]{0,2})\[(?<value>.*?)\]";
    private Dictionary<string,string> property = new Dictionary<string,string>();
    public GameNode(string  val)
    {
        value = val;
         property = Regex.Matches(value, elementOfNodePatern, RegexOptions.Singleline).Cast<Match>().ToDictionary(m => m.Groups["key"].Value,m => m.Groups["value"].Value);
        
        try
        {   
            position =property["B"];
            color = "B";
        }
        catch{}

        try
        {   
            position =property["W"];
            color = "W";
        }
        catch{}
        childrens = new List<GameNode>();
    }
    public void SetVariation(Game.Variation variation)
    {
        _variation =variation;
    }
    public Game.Variation GetVariation()
    {
        return _variation;
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
