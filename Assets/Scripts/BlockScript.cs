using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour {

    public Color color1;
    public Color color2;
    public Color color3;
    private Color currentColor;
    public Color matchColor;

    private SpriteRenderer sprite;

    public int colorIndex;

    public bool isEdge;
    public bool isCorner;
    public bool isCenter;

    public GameObject nNeighbor;
    public GameObject sNeighbor;
    public GameObject eNeighbor;
    public GameObject wNeighbor;

    public bool isMatched = false;
    private bool needsColor = false;
    private static bool waitForFall = false;

    void Start () {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        colorIndex = Random.Range(0, 3);

        SetNewColor();

        Vector3 down = new Vector3(0f, -1f);
        Vector3 left = new Vector3(-1f, 0f);

        Ray nRay = new Ray(transform.position, transform.up);
        Ray sRay = new Ray(transform.position, down);
        Ray eRay = new Ray(transform.position, transform.right);
        Ray wRay = new Ray(transform.position, left);

        RaycastHit nHit;
        RaycastHit sHit;
        RaycastHit eHit;
        RaycastHit wHit;

        if (Physics.Raycast(nRay, out nHit))
            {
            if (nHit.collider != null)
                {
                if (nHit.collider.gameObject.CompareTag("Block"))
                    {
                    nNeighbor = nHit.collider.gameObject;
                    }
                else
                    {
                    nNeighbor = null;
                    }
                }
            else
                {
                nNeighbor = null;
                }
            }

        if (Physics.Raycast(sRay, out sHit))
            {
            if (sHit.collider != null)
                {
                if (sHit.collider.gameObject.CompareTag("Block"))
                    {
                    sNeighbor = sHit.collider.gameObject;
                    }
                else
                    {
                    sNeighbor = null;
                    }
                }
            else
                {
                sNeighbor = null;
                }
            }

        if (Physics.Raycast(eRay, out eHit))
            {
            if (eHit.collider != null)
                {
                if (eHit.collider.gameObject.CompareTag("Block"))
                    {
                    eNeighbor = eHit.collider.gameObject;
                    }
                else
                    {
                    eNeighbor = null;
                    }
                }
            else
                {
                eNeighbor = null;
                }
            }

        if (Physics.Raycast(wRay, out wHit))
            {
            if (wHit.collider != null)
                {
                if (wHit.collider.gameObject.CompareTag("Block"))
                    {
                    wNeighbor = wHit.collider.gameObject;
                    }
                else
                    {
                    wNeighbor = null;
                    }
                }
            else
                {
                wNeighbor = null;
                }
            }
        }
	
	
	void Update () {
        sprite.color = currentColor;

        if (waitForFall == false)
            {
            MatchCheck();
            }
        

        if (needsColor)
            {
            StartCoroutine (GetNewColor(gameObject));
            needsColor = !needsColor;
            }
    }

    public void MatchCheck()
        {
        if (isMatched == false)
            {

            if (nNeighbor != null)
                {
                if (nNeighbor.GetComponent<BlockScript>().colorIndex == colorIndex)
                    {
                    if (sNeighbor != null)
                        {
                        if (sNeighbor.GetComponent<BlockScript>().colorIndex == colorIndex)
                            {
                            StartCoroutine (Match(gameObject, nNeighbor, sNeighbor));
                            }
                        }
                    if (eNeighbor != null)
                        {
                        if (eNeighbor.GetComponent<BlockScript>().colorIndex == colorIndex)
                            {
                            StartCoroutine (Match(gameObject, nNeighbor, eNeighbor));
                            }
                        }
                    if (wNeighbor != null)
                        {
                        if (wNeighbor.GetComponent<BlockScript>().colorIndex == colorIndex)
                            {
                            StartCoroutine (Match(gameObject, nNeighbor, wNeighbor));
                            }
                        }
                    }
                }
            if (sNeighbor != null)
                {
                if (sNeighbor.GetComponent<BlockScript>().colorIndex == colorIndex)
                    {
                    if (nNeighbor != null)
                        {
                        if (nNeighbor.GetComponent<BlockScript>().colorIndex == colorIndex)
                            {
                            StartCoroutine (Match(gameObject, sNeighbor, nNeighbor));
                            }
                        }
                    if (eNeighbor != null)
                        {
                        if (eNeighbor.GetComponent<BlockScript>().colorIndex == colorIndex)
                            {
                            StartCoroutine (Match(gameObject, sNeighbor, eNeighbor));
                            }
                        }
                    if (wNeighbor != null)
                        {
                        if (wNeighbor.GetComponent<BlockScript>().colorIndex == colorIndex)
                            {
                            StartCoroutine (Match(gameObject, sNeighbor, wNeighbor));
                            }
                        }
                    }
                }
            if (eNeighbor != null)
                {
                if (eNeighbor.GetComponent<BlockScript>().colorIndex == colorIndex)
                    {
                    if (sNeighbor != null)
                        {
                        if (sNeighbor.GetComponent<BlockScript>().colorIndex == colorIndex)
                            {
                            StartCoroutine (Match(gameObject, eNeighbor, sNeighbor));
                            }
                        }
                    if (nNeighbor != null)
                        {
                        if (nNeighbor.GetComponent<BlockScript>().colorIndex == colorIndex)
                            {
                            StartCoroutine (Match(gameObject, eNeighbor, nNeighbor));
                            }
                        }
                    if (wNeighbor != null)
                        {
                        if (wNeighbor.GetComponent<BlockScript>().colorIndex == colorIndex)
                            {
                            StartCoroutine (Match(gameObject, eNeighbor, wNeighbor));
                            }
                        }
                    }
                }
            if (wNeighbor != null)
                {
                if (wNeighbor.GetComponent<BlockScript>().colorIndex == colorIndex)
                    {
                    if (sNeighbor != null)
                        {
                        if (sNeighbor.GetComponent<BlockScript>().colorIndex == colorIndex)
                            {
                            StartCoroutine (Match(gameObject, wNeighbor, sNeighbor));
                            }
                        }
                    if (eNeighbor != null)
                        {
                        if (eNeighbor.GetComponent<BlockScript>().colorIndex == colorIndex)
                            {
                            StartCoroutine (Match(gameObject, wNeighbor, eNeighbor));
                            }
                        }
                    if (nNeighbor != null)
                        {
                        if (nNeighbor.GetComponent<BlockScript>().colorIndex == colorIndex)
                            {
                            StartCoroutine (Match(gameObject, wNeighbor, nNeighbor));
                            }
                        }
                    }
                }
            }
        }

    IEnumerator Match(GameObject block1, GameObject block2, GameObject block3)
        {
        waitForFall = true;
        yield return new WaitForSeconds(1f);
        block1.GetComponent<BlockScript>().currentColor = matchColor;
        block2.GetComponent<BlockScript>().currentColor = matchColor;
        block3.GetComponent<BlockScript>().currentColor = matchColor;

        block1.GetComponent<BlockScript>().isMatched = true;
        block2.GetComponent<BlockScript>().isMatched = true;
        block3.GetComponent<BlockScript>().isMatched = true;

        block1.GetComponent<BlockScript>().needsColor = true;
        block2.GetComponent<BlockScript>().needsColor = true;
        block3.GetComponent<BlockScript>().needsColor = true;
        }

    IEnumerator GetNewColor(GameObject current)
        {
        if (current.GetComponent<BlockScript>().isMatched)
            {
            if (current.GetComponent<BlockScript>().nNeighbor != null)
                {
                StartCoroutine(GetNewColor(current.GetComponent<BlockScript>().nNeighbor));
                Debug.Log("Checking north");
                yield break;
                }
            else
                {
                yield return new WaitForSeconds(.5f);
                colorIndex = Random.Range(0, 3);
                SetNewColor();
                Debug.Log("Random");
                }
            }
        else if (current.GetComponent<BlockScript>().isMatched == false)
            {
            yield return new WaitForSeconds(.5f);
            colorIndex = current.GetComponent<BlockScript>().colorIndex;
            SetNewColor();
            Debug.Log("Setting");
            }
        }

    public void SetNewColor()
        {
        if (colorIndex == 0)
            {
            currentColor = color1;
            }
        if (colorIndex == 1)
            {
            currentColor = color2;
            }
        if (colorIndex == 2)
            {
            currentColor = color3;
            }
        isMatched = false;
        waitForFall = false;
        }
}

