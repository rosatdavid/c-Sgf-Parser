using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using System.Text.RegularExpressions;
public class startgame : MonoBehaviour
{
    // Start is called before the first frame update
    public List<string> testsSgf;    
    public bool Tests(){
        bool ret =true; 
        foreach(string testSgf in testsSgf)
        {
            
            Game test1 = new Game(testSgf);
            bool t = test1.CompareWithSgf(testSgf);
            if(!t){
               Debug.LogError("test failed :"+testSgf);
                ret = false;
            }
        }
        return ret;
    }

private string OpenSgfFile(string fileName)
{
    string content = "";
    try
    {   
        content = System.IO.File.ReadAllText(fileName);
    }
    catch (Exception Ex)
    {
        Debug.Log(Ex.ToString());
    }

       Debug.Log(content);
       return content;
}
    void Start()
    {
       testsSgf = new List<string>();
       testsSgf.Add("(;GM[1]FF[4]CA[UTF-8]AP[Sabaki:0.43.3]KM[7.5]SZ[19]DT[2019-11-06];B[as](;W[ar])(;W[br])(;W[cr])(;W[dr])(;W[er]))");
       testsSgf.Add("(;GM[1]FF[4]CA[UTF-8]AP[Sabaki:0.43.3]KM[7.5]SZ[19]DT[2019-11-06];B[as];W[ar];B[aq](;W[ap])(;W[bq](;B[bp];W[bo];B[bn])(;B[cq];W[cp](;B[co];W[cn])(;B[dq](;W[dp];B[do];W[dn];B[dm])(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn]))))))");
       testsSgf.Add("(;GM[1]FF[4]CA[UTF-8]AP[Sabaki:0.43.3]KM[7.5]SZ[19]DT[2019-11-06];B[as];W[ar];B[aq](;W[ap])(;W[bq](;B[bp];W[bo];B[bn])(;B[cq];W[cp](;B[co];W[cn])(;B[dq](;W[dp];B[do];W[dn];B[dm])(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn]))(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn])))(;B[dq](;W[dp];B[do];W[dn];B[dm])(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn]))(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn]))))(;B[cq];W[cp](;B[co];W[cn])(;B[dq](;W[dp];B[do];W[dn];B[dm])(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn]))(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn])))(;B[dq](;W[dp];B[do];W[dn];B[dm])(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn]))(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn])))))(;W[bq](;B[bp];W[bo];B[bn])(;B[cq];W[cp](;B[co];W[cn])(;B[dq](;W[dp];B[do];W[dn];B[dm])(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn]))(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn])))(;B[dq](;W[dp];B[do];W[dn];B[dm])(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn]))(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn]))))(;B[cq];W[cp](;B[co];W[cn])(;B[dq](;W[dp];B[do];W[dn];B[dm])(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn]))(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn])))(;B[dq](;W[dp];B[do];W[dn];B[dm])(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn]))(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn]))))))");
       testsSgf.Add(OpenSgfFile(@"KogoJosekiDictionary.sgf"));


        bool passingTests = Tests();
       if(passingTests)
        {
            Debug.Log("tests pass");
        }else
        {
            Debug.LogError("tests Dosent pass");
        }
      
        float start =Time.realtimeSinceStartup;
        string sgf = OpenSgfFile(@"Kogo's Joseki Dictionary.sgf");
        Game game = new Game(sgf);
        float now = Time.realtimeSinceStartup;
        float finish =now-start;
        Debug.Log("create gametree take : "+finish+" secondes");

        string finalSgf =game.exportSgf();

        try
        {   
            System.IO.File.WriteAllText(@"export.sgf",finalSgf);
        }
        catch (Exception Ex)
        {
            Debug.Log(Ex.ToString());
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
