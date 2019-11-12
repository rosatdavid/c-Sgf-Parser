using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
public class Game
{
    private string blackName;
    private string whiteName;
    private string blackRank;
    private string whiteRank;
    private string blackTeam;
    private string whiteTeam;
    private string result;
    private string komi;
    private string handicap;
    private string time;
    private string date;
    private string eventPlayed;
    private string round;
    private string place;
    private string rules;
    private string gameName;
    private string opening;
    private string gameComment;
    private string source;
    private string user;
    private string annotation;
    private string copyright;
    private string m_sgf;
    private string m_baseSgfCleaned;
    
    private GameNode m_root;
    private List<GameNode> m_listGameNode;
private List<GameNode> CleanSgfForParser()
{
    char charToremplaceNode ='n';
    List<GameNode> listNode = new List<GameNode>();
    string nodePatern = @"(;[A-Z]{0,2}\[.*?\](?=[\r\n]*;|[\r\n]*\)|[\r\n]*\(|$))";
    
    MatchCollection matches = Regex.Matches(m_sgf, nodePatern, RegexOptions.Singleline);
    foreach(Match m in matches)
    {
        listNode.Add(new GameNode(m.Groups[1].Value));
    }
    Regex node = new Regex(nodePatern);
    m_sgf = Regex.Replace(m_sgf,nodePatern,charToremplaceNode.ToString(),RegexOptions.Singleline);
    Regex returnLine = new Regex(@"\r\n|;");
    m_sgf = returnLine.Replace(m_sgf,"");
     return listNode;
}


    public Game(string sgf)
    {
        m_sgf = sgf;
        m_listGameNode =CleanSgfForParser();
        ParseSgf();
        
    }

    public GameNode GetRoot()
    {
        return m_root;
    }

private void createTree( ref int index,ref int nodeIndex,GameNode parent = null)
{

while(nodeIndex < m_listGameNode.Count)
{
    char c = m_sgf[index];
    if(c == 'n')
    {
        if(parent != null)
        {
            parent.AddChildren(m_listGameNode[nodeIndex]);
        }
        m_listGameNode[nodeIndex].SetParent(parent);
        parent =m_listGameNode[nodeIndex];
        nodeIndex =nodeIndex+1;
        index = index+1;
        
    }else if(c == '(')
    {
        index = index+1;
        createTree(ref index,ref nodeIndex,parent);
    }else if(c == ')')
    {
        index = index+1;
        return;
    }else
    {
        Debug.LogError("createTreeSimple Error not n or ( or )");
        return;
    }


        
}

return;
}  


    public string parcourTree(GameNode node)
    {
        string retour;
        List<GameNode> childrens = node.GetChidrens();
        Debug.Log("Node :"+node.GetValue() +"has "+childrens.Count+" childs");
        retour =node.GetValue();
        if(childrens.Count > 1)
        {
            foreach (var ch in childrens)
            {

            retour = retour + "(" +parcourTree(ch) +")";
            }
        }else if(childrens.Count == 1)
        {
            retour = retour  +parcourTree(childrens[0]);
        }
        return retour;
    }
    public string MakeSgf(GameNode node)
    {
        string retour;
        List<GameNode> childrens = node.GetChidrens();
       // Debug.Log("Node :"+node.GetValue() +"has "+childrens.Count+" childs");
        retour =node.GetValue();
        if(childrens.Count > 1)
        {
            foreach (var ch in childrens)
            {

            retour = retour + "(" +MakeSgf(ch) +")";
            }
        }else if(childrens.Count == 1)
        {
            retour = retour  +MakeSgf(childrens[0]);
        }
        return retour;
    }
    public string exportSgf()
    {
        return "("+MakeSgf(m_root)+")";
    } 
    public bool CompareWithSgf(string sgf)
    {
        string finalsgf =exportSgf();
       // Debug.Log(finalsgf);
        if (finalsgf == sgf)
        {
             Debug.Log("input and output match");
             return true;
        }
        return false;
    }
    private bool ParseSgf()
    {
        
  
  /* 
        Regex game = new Regex(@"GM\[(\d)\]");
  
        Match match = game.Match(sgf);
        if (match.Success) {
            Debug.Log(match.Groups[1].Value);
            if(match.Groups[1].Value != "1")
                return false;
        }
        
        blackName = new Regex(@"PB\[(.*?)\]").Match(sgf).Groups[1].Value;
        whiteName = new Regex(@"PW\[(.*?)\]").Match(sgf).Groups[1].Value;
        blackRank = new Regex(@"BR\[(.*?)\]").Match(sgf).Groups[1].Value;
        whiteRank = new Regex(@"WR\[(.*?)\]").Match(sgf).Groups[1].Value;
        blackTeam = new Regex(@"BT\[(.*?)\]").Match(sgf).Groups[1].Value;
        whiteTeam = new Regex(@"WT\[(.*?)\]").Match(sgf).Groups[1].Value;
        result = new Regex(@"RE\[(.*?)\]").Match(sgf).Groups[1].Value;
        komi= new Regex(@"KM\[(.*?)\]").Match(sgf).Groups[1].Value;
        handicap = new Regex(@"HA\[(.*?)\]").Match(sgf).Groups[1].Value;
        time = new Regex(@"TM\[(.*?)\]").Match(sgf).Groups[1].Value;
        date = new Regex(@"DT\[(.*?)\]").Match(sgf).Groups[1].Value;
        eventPlayed = new Regex(@"EV\[(.*?)\]").Match(sgf).Groups[1].Value;
        round = new Regex(@"RO\[(.*?)\]").Match(sgf).Groups[1].Value;
        place = new Regex(@"RE\[(.*?)\]").Match(sgf).Groups[1].Value;
        rules = new Regex(@"RU\[(.*?)\]").Match(sgf).Groups[1].Value;
        gameName = new Regex(@"GN\[(.*?)\]").Match(sgf).Groups[1].Value;
        opening = new Regex(@"ON\[(.*?)\]").Match(sgf).Groups[1].Value;
        gameComment= new Regex(@"GC\[(.*?)\]").Match(sgf).Groups[1].Value;
        source = new Regex(@"SO\[(.*?)\]").Match(sgf).Groups[1].Value;
        user = new Regex(@"US\[(.*?)\]").Match(sgf).Groups[1].Value;
        annotation = new Regex(@"AN\[(.*?)\]").Match(sgf).Groups[1].Value;  
        copyright =  new Regex(@"CP\[(.*?)\]").Match(sgf).Groups[1].Value;
         
        



        Regex node = new Regex(@"(\b[A-Z]{1,}\[.*?\]){1,}");
        */
        m_root =  m_listGameNode[0];
        //int firstNodeIndex =node.Match(sgf).Index;
        int startNode =0;
        int index =0;
        createTree(ref index,ref startNode);

        //CompareWithSgf(sgf,root);
        //Debug.Log(MakeSgf(root));

        return true;

    }
    
}
