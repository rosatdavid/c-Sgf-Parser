﻿using System.Collections;
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
    public GameNode(GameNode p,string val)
    {
        parent = p;
        value = val;
        Match colorandpos = new Regex(@"(B|W)\[(.*?)\]").Match(val);
        color =colorandpos.Groups[1].Value;
        position =colorandpos.Groups[2].Value;
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
    public void addChildren(GameNode ch)
    {
        childrens.Add(ch);
    }
}