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
            bool t = test1.CompareWithSgf(testSgf,test1.GetRoot());
            if(!t){
               Debug.LogError("test failed :"+testSgf);
                ret = false;
            }

        }

        return ret;
    }
public string OpenSgfFile()
{
    string fileName = @"kogominsanscom.sgf";
    string content = "";
    try
    {   
        content = System.IO.File.ReadAllText(fileName);
    }
    catch (Exception Ex)
    {
        Debug.Log(Ex.ToString());
    }
    
       content = Regex.Replace(content, @"\t\n\r", "");
       Debug.Log(content);
       return content;
}
    void Start()
    {
       testsSgf = new List<string>();
       testsSgf.Add("(;GM[1]FF[4]CA[UTF-8]AP[Sabaki:0.43.3]KM[7.5]SZ[19]DT[2019-11-06];B[as](;W[ar])(;W[br])(;W[cr])(;W[dr])(;W[er]))");
       testsSgf.Add("(;GM[1]FF[4]CA[UTF-8]AP[Sabaki:0.43.3]KM[7.5]SZ[19]DT[2019-11-06];B[as];W[ar];B[aq](;W[ap])(;W[bq](;B[bp];W[bo];B[bn])(;B[cq];W[cp](;B[co];W[cn])(;B[dq](;W[dp];B[do];W[dn];B[dm])(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn]))))))");
       testsSgf.Add("(;GM[1]FF[4]CA[UTF-8]AP[Sabaki:0.43.3]KM[7.5]SZ[19]DT[2019-11-06];B[as];W[ar];B[aq](;W[ap])(;W[bq](;B[bp];W[bo];B[bn])(;B[cq];W[cp](;B[co];W[cn])(;B[dq](;W[dp];B[do];W[dn];B[dm])(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn]))(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn])))(;B[dq](;W[dp];B[do];W[dn];B[dm])(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn]))(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn]))))(;B[cq];W[cp](;B[co];W[cn])(;B[dq](;W[dp];B[do];W[dn];B[dm])(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn]))(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn])))(;B[dq](;W[dp];B[do];W[dn];B[dm])(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn]))(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn])))))(;W[bq](;B[bp];W[bo];B[bn])(;B[cq];W[cp](;B[co];W[cn])(;B[dq](;W[dp];B[do];W[dn];B[dm])(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn]))(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn])))(;B[dq](;W[dp];B[do];W[dn];B[dm])(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn]))(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn]))))(;B[cq];W[cp](;B[co];W[cn])(;B[dq](;W[dp];B[do];W[dn];B[dm])(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn]))(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn])))(;B[dq](;W[dp];B[do];W[dn];B[dm])(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn]))(;W[ep];B[eo](;W[en];B[em])(;W[fp];B[fo];W[fn]))))))");


        //OpenSgfFile();


       if(Tests())
        {
            Debug.Log("tests pass");
        }else
        {
            Debug.LogError("tests Dosent pass");
            
        }
      
                   
     // string sgf = "(;GM[1]FF[4]SZ[19]GN[]DT[2019-05-16]PB[vidada]PW[gg0010203]BR[6çº§]WR[6çº§]KM[0]HA[0]RU[Japanese]AP[Sabaki:0.43.3]RE[B+R]TM[1200]TC[3]TT[30]RL[0]CA[UTF-8];B[pd]C[bla]N[blou];W[dd];B[pp];W[dp];B[fq];W[hq];B[cq];W[dq];B[cp];W[dr];B[do];W[eo];B[cr];W[dn];B[co](;W[fp];B[cm];W[kp];B[cc];W[dc](;B[cd];W[ce];B[be];W[bf];B[cf];W[de];B[bg];W[bd];B[af];W[bc];B[eg];W[he];B[nc];W[qq];B[pq];W[qp];B[qo];W[ro];B[rn];W[sp];B[qn];W[qr];B[pr];W[qs];B[qj];W[qf];B[of];W[rd];B[qc];W[rc];B[rb];W[sb];B[qb];W[qh];B[oh];W[pi];B[ic];W[hc];B[id];W[hd];B[jf];W[le];B[je];W[lg];B[li];W[kh];B[ok];W[lc];B[kd];W[ld];B[kc];W[ki];B[kj];W[ii];B[jj];W[ji];B[jb];W[lb];B[hb];W[gb];B[ia];W[ga];B[ha];W[kb];B[hf];W[gf];B[hg];W[gg];B[hh];W[ij];B[gh];W[lj];B[lk];W[mj];B[mk];W[mi];B[oi];W[pj];B[pk];W[oj];B[nj];W[ni];B[nk];W[ph];B[mh];W[lh];B[nh];W[li];B[mf];W[lf];B[jl];W[il];B[jm];W[im];B[jn];W[ch];B[df];W[fh];B[fg];W[fe];B[ei];W[fi];B[fj];W[gi];B[hi];W[gj];B[hj];W[gk];B[ik];W[hk];B[jk];W[ie];B[if];W[ke];B[jd];W[ej];B[dj];W[eh];B[bi];W[di];B[cj];W[bh];B[ah];W[fk];B[nb];W[ne];B[oe];W[nf];B[nd];W[me];B[og];W[mg];B[qe];W[re];B[pf];W[qg];B[qd];W[mb];B[ja];W[no];B[mp];W[mo];B[lq];W[lp];B[kq];W[ln];B[jp];W[jq];B[ko];W[lo];B[nq];W[np];B[mq];W[po];B[pn];W[oo];B[iq];W[jr];B[ir];W[ip];B[jo];W[hr];B[kr];W[is];B[op];W[or];B[nr];W[ps];B[oq];W[os];B[ks];W[ns];B[on];W[ms];B[nn];W[js];B[lm];W[rj];B[rk];W[ri];B[sk];W[sj];B[em];W[en];B[dm];W[fm];B[cg];W[cn];B[bn];W[el];B[bl];W[io];B[in];W[hn];B[ds];W[es];B[cs];W[fr];B[ee];W[ed];B[ff];W[ge];B[gc];W[fc];B[ad];W[cb];B[dh];W[dg];B[ef];W[dh];B[dl];W[dk];B[ck];W[ek];B[ra];W[ci];B[bj];W[ac];B[ae];W[rq];B[sn];W[so];B[qk];W[jg];B[ih];W[kf];B[jh];W[na];B[oa];W[ma];B[ka];W[ob];B[pa];W[pb];B[qa];W[pg];B[oc];W[lr];B[mn];W[ls];B[kn];W[sc];B[qi];W[rg];B[mr];W[gd];B[sr])(;B[mi];W[lj];B[ff];W[me](;B[oi](;W[sl])(;W[im])(;W[jb])(;W[hk])(;W[pm];B[ql];W[rn](;B[lm])(;B[nn];W[jm];B[hl])))(;B[ih])))(;W[hl];B[hi];W[qm];B[ke](;W[je];B[jq];W[jr])(;W[di];B[mk];W[lh];B[qh];W[rl];B[lp];W[ns];B[mn];W[om](;B[so])(;B[qk];W[ro];B[rp];W[qq];B[qr]))))";
     //   Game game = new Game(sgf);
    }

    // Update is called once per frame
    void Update()
    {
        //string sgf = "(;GM[1]FF[4]SZ[19]GN[]DT[2019-05-16]PB[vidada]PW[gg0010203]BR[6çº§]WR[6çº§]KM[0]HA[0]RU[Japanese]AP[Sabaki:0.43.3]RE[B+R]TM[1200]TC[3]TT[30]RL[0]CA[UTF-8];B[pd]C[bla]N[blou];W[dd];B[pp];W[dp];B[fq];W[hq];B[cq];W[dq];B[cp];W[dr];B[do];W[eo];B[cr];W[dn];B[co](;W[fp];B[cm];W[kp];B[cc];W[dc](;B[cd];W[ce];B[be];W[bf];B[cf];W[de];B[bg];W[bd];B[af];W[bc];B[eg];W[he];B[nc];W[qq];B[pq];W[qp];B[qo];W[ro];B[rn];W[sp];B[qn];W[qr];B[pr];W[qs];B[qj];W[qf];B[of];W[rd];B[qc];W[rc];B[rb];W[sb];B[qb];W[qh];B[oh];W[pi];B[ic];W[hc];B[id];W[hd];B[jf];W[le];B[je];W[lg];B[li];W[kh];B[ok];W[lc];B[kd];W[ld];B[kc];W[ki];B[kj];W[ii];B[jj];W[ji];B[jb];W[lb];B[hb];W[gb];B[ia];W[ga];B[ha];W[kb];B[hf];W[gf];B[hg];W[gg];B[hh];W[ij];B[gh];W[lj];B[lk];W[mj];B[mk];W[mi];B[oi];W[pj];B[pk];W[oj];B[nj];W[ni];B[nk];W[ph];B[mh];W[lh];B[nh];W[li];B[mf];W[lf];B[jl];W[il];B[jm];W[im];B[jn];W[ch];B[df];W[fh];B[fg];W[fe];B[ei];W[fi];B[fj];W[gi];B[hi];W[gj];B[hj];W[gk];B[ik];W[hk];B[jk];W[ie];B[if];W[ke];B[jd];W[ej];B[dj];W[eh];B[bi];W[di];B[cj];W[bh];B[ah];W[fk];B[nb];W[ne];B[oe];W[nf];B[nd];W[me];B[og];W[mg];B[qe];W[re];B[pf];W[qg];B[qd];W[mb];B[ja];W[no];B[mp];W[mo];B[lq];W[lp];B[kq];W[ln];B[jp];W[jq];B[ko];W[lo];B[nq];W[np];B[mq];W[po];B[pn];W[oo];B[iq];W[jr];B[ir];W[ip];B[jo];W[hr];B[kr];W[is];B[op];W[or];B[nr];W[ps];B[oq];W[os];B[ks];W[ns];B[on];W[ms];B[nn];W[js];B[lm];W[rj];B[rk];W[ri];B[sk];W[sj];B[em];W[en];B[dm];W[fm];B[cg];W[cn];B[bn];W[el];B[bl];W[io];B[in];W[hn];B[ds];W[es];B[cs];W[fr];B[ee];W[ed];B[ff];W[ge];B[gc];W[fc];B[ad];W[cb];B[dh];W[dg];B[ef];W[dh];B[dl];W[dk];B[ck];W[ek];B[ra];W[ci];B[bj];W[ac];B[ae];W[rq];B[sn];W[so];B[qk];W[jg];B[ih];W[kf];B[jh];W[na];B[oa];W[ma];B[ka];W[ob];B[pa];W[pb];B[qa];W[pg];B[oc];W[lr];B[mn];W[ls];B[kn];W[sc];B[qi];W[rg];B[mr];W[gd];B[sr])(;B[mi];W[lj];B[ff];W[me](;B[oi](;W[sl])(;W[im])(;W[jb])(;W[hk])(;W[pm];B[ql];W[rn](;B[lm])(;B[nn];W[jm];B[hl])))(;B[ih])))(;W[hl];B[hi];W[qm];B[ke](;W[je];B[jq];W[jr])(;W[di];B[mk];W[lh];B[qh];W[rl];B[lp];W[ns];B[mn];W[om](;B[so])(;B[qk];W[ro];B[rp];W[qq];B[qr]))))";
        //Game game = new Game(sgf);
    }
}
