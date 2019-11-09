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

    private GameNode root;
    public Game(string sgf)
    {
        ParseSgf(sgf);
    }

    public GameNode GetRoot()
    {
        return root;
    }
private int createTree(ref string tree,int index  = 0,GameNode parent = null)
{
    //Regex insideOfParentesis  = new Regex(@"\((.*)\)"); 

  
    //Regex node = new Regex(@"(;[A-Z].*?\]((?=;)|(?=\()|(?=\))|($)))");
    Regex node = new Regex(@"(;[A-Z]{1,2}\[.*?\]((?=;)|(?=\()|(?=\))|$))");

    Regex firstParentesis =  new Regex(@"(\()|(\))");
    //Regex firstParentesis =  new Regex(@"^.*?(\(|\))");
    
    bool loop = true;
    
    int maxNodeIndex =0;
    //int childIndex = 0;
    while(loop)
    {
        
       // string tree = tree2.Substring(index+childIndex);
       //index = index + childIndex;
        //childIndex =0;
        maxNodeIndex = tree.Length;
        Match matchFirstParentesis = firstParentesis.Match(tree,index);
        if(matchFirstParentesis.Success)
        {
            maxNodeIndex = matchFirstParentesis.Index;
        }
        //string partTreeForNodes = tree.Substring(0,maxNodeIndex);
        
        //string partOfStringToWorkWith = tree.Substring(0, maxNodeIndex);
            // Debug.Log("tree lenght: "+ tree.Length +" : "+index + " : "+(maxNodeIndex-index));
            
            MatchCollection matches = node.Matches(tree,index);
            foreach(Match match in matches)
            {
                if(match.Index+match.Length > maxNodeIndex)
                    break;
                        string value = match.Value;    
                        GameNode gn = new GameNode(parent,value);
                        if(parent != null)
                            parent.addChildren(gn);

                        parent =gn;
                        
            }        
                
                        
        if(matchFirstParentesis.Success)
        {


                
                string comp = matchFirstParentesis.Value;
                int childIndex  = maxNodeIndex+1;
                maxNodeIndex = 0;
                if(comp == "(")
                {
                    
                    index =  createTree(ref tree,childIndex, parent);
                    
                }else if(comp == ")")
                {
                    return childIndex;
                }else
                {
                    Debug.LogError("Not ( ) found");
                    return -1;
                }

            }
        else
        { 
            loop = false;
        }

    }
    return index;
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
        return "("+MakeSgf(root.GetChidrens()[0])+")";
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
    private bool ParseSgf(string sgf)
    {
        


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
        root =  new GameNode(null,"root");
        int firstNodeIndex =node.Match(sgf).Index;
         float start =Time.realtimeSinceStartup;
        createTree(ref sgf,0,root);
        float now = Time.realtimeSinceStartup;
        float finish =now-start;
        Debug.Log("createTree take"+finish+"secondes");
        //CompareWithSgf(sgf,root);
        //Debug.Log(MakeSgf(root));

        return true;

    }
    
}
