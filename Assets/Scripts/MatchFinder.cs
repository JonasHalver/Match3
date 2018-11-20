using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchFinder : MonoBehaviour {

    private List<GameObject> blocks = new List<GameObject>();
    public List<GameObject> matchBlocks = new List<GameObject>();

    private int count = 0;

    public GameObject resetter;

    public bool matched = false;
    public bool isChanging = false;

	void Start () {
        foreach (GameObject block in GameObject.FindGameObjectsWithTag("Block"))
            {
            blocks.Add(block);
            }
	}
	
	// Update is called once per frame
	void Update () {

        if (BlockScript.isSwapping == false)
            {
            if (isChanging == false)
                {
                if (matched == false)
                    {
                    MatchFunction();
                    }
                else
                    {
                    StartCoroutine(MatchColorChange());
                    isChanging = true;
                    }
                }
            }
	}

    public void MatchFunction()
        {
        foreach (GameObject block in blocks.ToArray())
            {
            CheckNeighbors(block);
            }
        count = matchBlocks.Count;


        if (count >= 3)
            {
            foreach (GameObject block in matchBlocks)
                {
                block.GetComponent<BlockScript>().isMatched = true;
                
                }
            matched = true;
            }
        }

    public void CheckNeighbors(GameObject current)
        {

        List<GameObject> tempList = new List<GameObject>();
        bool nDeadEnd = false;
        bool sDeadEnd = false;
        bool eDeadEnd = false;
        bool wDeadEnd = false;

        GameObject n = current.GetComponent<BlockScript>().nNeighbor;
        GameObject s = current.GetComponent<BlockScript>().sNeighbor;
        GameObject e = current.GetComponent<BlockScript>().eNeighbor;
        GameObject w = current.GetComponent<BlockScript>().wNeighbor;

        if (n != null)
            {
            if (n.GetComponent<BlockScript>().colorIndex == current.GetComponent<BlockScript>().colorIndex)
                {
                if (tempList.Contains(n) == false)
                    {
                    tempList.Add(n);
                    }
                else
                    {
                    nDeadEnd = true;
                    }
                }
            else
                {
                nDeadEnd = true;
                }
            }
        else
            {
            nDeadEnd = true;
            }

        if (s != null)
            {
            if (s.GetComponent<BlockScript>().colorIndex == current.GetComponent<BlockScript>().colorIndex)
                {
                if (tempList.Contains(s) == false)
                    {
                    tempList.Add(s);
                    }
                else
                    {
                    sDeadEnd = true;
                    }
                }

            else
                {
                sDeadEnd = true;
                }
            }
        else
            {
            sDeadEnd = true;
            }


        if (e != null)
            {
            if (e.GetComponent<BlockScript>().colorIndex == current.GetComponent<BlockScript>().colorIndex)
                {
                if (tempList.Contains(e) == false)
                    {
                    tempList.Add(e);
                    }
                else
                    {
                    eDeadEnd = true;
                    }
                }
            else
                {
                eDeadEnd = true;
                }
            }
        else
            {
            eDeadEnd = true;
            }

        if (w != null)
            {
            if (w.GetComponent<BlockScript>().colorIndex == current.GetComponent<BlockScript>().colorIndex)
                {
                if (tempList.Contains(w) == false)
                    {
                    tempList.Add(w);
                    }
                else
                    {
                    wDeadEnd = true;
                    }
                }
            else
                {
                wDeadEnd = true;
                }
            }
        else
            {
            wDeadEnd = true;
            }

        if (tempList.Count >= 2)
            {
            tempList.Add(current);

            foreach (GameObject block in tempList)
                {
                if (matchBlocks.Contains(block) == false)
                    {
                    matchBlocks.Add(block);
                    }
                }
            }
        }

    IEnumerator MatchColorChange()
        {
        yield return new WaitForSeconds(.5f);

        foreach (GameObject block in matchBlocks)
            {
            block.GetComponent<BlockScript>().currentColor = block.GetComponent<BlockScript>().matchColor;
            block.GetComponent<BlockScript>().needsColor = true;
            block.GetComponent<BlockScript>().poof.Play();
            }

        yield return new WaitForSeconds(1);
        resetter.SendMessage("ResetMatch");
        }

    public void ResetMatch()
        {
        matched = false;
        isChanging = false;
        matchBlocks = new List<GameObject>();
        }

    }
