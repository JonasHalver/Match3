using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour {

    public Sprite color1;
    public Sprite color2;
    public Sprite color3;
    public Sprite color4;
    public Sprite color5;
    public Sprite currentColor;
    public Sprite matchColor;

    public SpriteRenderer sprite;
    public GameObject image;

    public int colorIndex;

    public GameObject nNeighbor;
    public GameObject sNeighbor;
    public GameObject eNeighbor;
    public GameObject wNeighbor;

    public GameObject resetter;

    public bool isMatched = false;
    public bool needsColor = false;
    public static bool waitForReset = false;
    public static bool stillCounting = false;

    public int newCount = 0;
    private int previousCount = 0;

    public List<GameObject> matchBlocks = new List<GameObject>();
    public List<GameObject> blocks = new List<GameObject>();
    public List<int> counts = new List<int>();

    public Vector3 blockPos;
    private Vector3 screenPoint;
    private Vector3 offset;
    private Rigidbody rb;
    public float maxDrag = 1.5f;
    public static bool isSwapping = false;
    public bool isPicked = false;
    public GameObject swapTarget = null;
    public direction swapDirection;

    public enum direction {Up, Down, Left, Right};
    

    void Start () {
        //sprite = gameObject.GetComponent<SpriteRenderer>();
        colorIndex = Random.Range(0, 5);

        foreach (GameObject block in GameObject.FindGameObjectsWithTag("Block"))
            {
            blocks.Add(block);
            }

        SetNewColor();
        blockPos = transform.position;
        rb = gameObject.GetComponent<Rigidbody>();

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
        sprite.sprite = currentColor;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other = resetter.GetComponent<Collider>())
            {
                GetNewColor(gameObject);
                matchBlocks = new List<GameObject>();
                counts = new List<int>();
                isMatched = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
        {
        if (other != null)
            {
            if (isPicked)
                {
                if (other.CompareTag("Block"))
                    {
                    SwapHover(other.GetComponent<BlockScript>().image);
                    isSwapping = true;
                    }
                }
            }
        }

    private void OnTriggerExit(Collider other)
        {
        if (other != null)
            {
            if (other.CompareTag("Block"))
                {
                isSwapping = false;
                other.GetComponent<BlockScript>().image.transform.localPosition = new Vector3(0f, 0f);
                }
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
        if (colorIndex == 3)
            {
            currentColor = color4;
            }
        if (colorIndex == 4)
            {
            currentColor = color5;
            }
        needsColor = false;
        }

    public void CountCheck()
        {
        Debug.Log("Checking Count");
        int highestCount;
        foreach (GameObject block in blocks)
            {
            counts.Add(block.GetComponent<BlockScript>().newCount);
            }
        counts.Sort();
        counts.Reverse();

        highestCount = counts[0];
        Debug.Log(highestCount.ToString());

        if (highestCount == newCount)
            {
            stillCounting = false;
            }
        }

    public bool CheckNeighbors(GameObject current)
    {
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
            if (n.GetComponent<BlockScript>().colorIndex == colorIndex)
            {
                if (matchBlocks.Contains(n) == false)
                {
                    matchBlocks.Add(n);
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
            if (s.GetComponent<BlockScript>().colorIndex == colorIndex)
            {
                if (matchBlocks.Contains(s) == false)
                {
                    matchBlocks.Add(s);
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
            if (e.GetComponent<BlockScript>().colorIndex == colorIndex)
            {
                if (matchBlocks.Contains(e) == false)
                {
                    matchBlocks.Add(e);
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
            if (w.GetComponent<BlockScript>().colorIndex == colorIndex)
            {
                if (matchBlocks.Contains(w) == false)
                {
                    matchBlocks.Add(w);
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

        if (nDeadEnd && sDeadEnd && eDeadEnd && wDeadEnd)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GetNewColor(GameObject current)
    {
        if (current.GetComponent<BlockScript>().needsColor)
        {
            if (current.GetComponent<BlockScript>().nNeighbor != null)
            {
                GetNewColor(current.GetComponent<BlockScript>().nNeighbor);
            }
            else
            {
                colorIndex = Random.Range(0, 5);
                SetNewColor();
            }
        }
        else if (current.GetComponent<BlockScript>().needsColor == false)
        {
            colorIndex = current.GetComponent<BlockScript>().colorIndex;
            current.GetComponent<BlockScript>().needsColor = true;
            current.GetComponent<BlockScript>().currentColor = matchColor;
            SetNewColor();
        }
    }

    public void ResetMatch()
    {
        
        waitForReset = false;
    }

    public void OnMouseDown()
        {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }

    private void OnMouseDrag()
        {
        Vector3 newPos = new Vector3();
        Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset;

        gameObject.tag = "PickedBlock";
        isPicked = true;

        float xOffset = cursorPos.x - blockPos.x;
        float yOffset = cursorPos.y - blockPos.y;

        if (Mathf.Abs(xOffset) < maxDrag && Mathf.Abs(yOffset) < maxDrag)
            {
            if (Mathf.Abs(xOffset) > Mathf.Abs(yOffset))
                {
                //rb.constraints = RigidbodyConstraints.FreezePositionY;
                newPos = new Vector3(cursorPos.x, blockPos.y, blockPos.z);
                }
            else
                {
                //rb.constraints = RigidbodyConstraints.FreezePositionX;
                newPos = new Vector3(blockPos.x, cursorPos.y, blockPos.z);
                }
            }
        else
            {
            if (xOffset <= maxDrag * -1f)
                {
                newPos = new Vector3(blockPos.x - maxDrag, blockPos.y, blockPos.z);
                }
            else if (xOffset >= maxDrag)
                {
                newPos = new Vector3(blockPos.x + maxDrag, blockPos.y, blockPos.z);
                }
            else if (yOffset <= maxDrag * -1f)
                {
                newPos = new Vector3(blockPos.x, blockPos.y - maxDrag, blockPos.z);
                }
            else if (yOffset >= maxDrag)
                {
                newPos = new Vector3(blockPos.x, blockPos.y + maxDrag, blockPos.z);
                }

            }

        if (Mathf.Abs(xOffset) > Mathf.Abs(yOffset) && xOffset > 0)
            {
            swapDirection = direction.Right;
            }
        else if (Mathf.Abs(xOffset) > Mathf.Abs(yOffset) && xOffset < 0)
            {
            swapDirection = direction.Left;
            }
        else if (Mathf.Abs(xOffset) < Mathf.Abs(yOffset) && yOffset > 0)
            {
            swapDirection = direction.Up;
            }
        else if (Mathf.Abs(xOffset) < Mathf.Abs(yOffset) && yOffset < 0)
            {
            swapDirection = direction.Down;
            }

        transform.position = newPos;
        }

    private void OnMouseUp()
        {
        transform.position = blockPos;

        gameObject.tag = "Block";
        isPicked = false;
        swapTarget.transform.localPosition = new Vector3(0f, 0f, 0f);

        if (isSwapping)
            {
            Swap(swapTarget);            
            }

        
        }

    public void SwapHover(GameObject other)
        {
        swapTarget = other;
        //Vector3 relPos =  transform.InverseTransformVector(blockPos);

        //float xOffset = transform.position.x - other.transform.parent.transform.position.x;
        //float yOffset = transform.position.y - other.transform.parent.transform.position.y;

        if (swapDirection == direction.Up)
            {
            other.transform.localPosition = new Vector3(0f, -0.138f);
            }
        else if (swapDirection == direction.Down)
            {
            other.transform.localPosition = new Vector3(0f, 0.138f);
            }
        else if (swapDirection == direction.Left)
            {
            other.transform.localPosition = new Vector3(0.138f, 0f);
            }
        else if (swapDirection == direction.Right)
            {
            other.transform.localPosition = new Vector3(-0.138f, 0f);
            }
        }

    public void Swap(GameObject other)
        {
        int index = colorIndex;
        int targetIndex = other.transform.parent.GetComponent<BlockScript>().colorIndex;
        Debug.Log(index.ToString() + " and " + targetIndex.ToString());

        colorIndex = targetIndex;
        other.transform.parent.GetComponent<BlockScript>().colorIndex = index;
        Debug.Log(other.transform.parent.GetComponent<BlockScript>().colorIndex.ToString() + " and " + colorIndex.ToString());

        SetNewColor();
        other.transform.parent.GetComponent<BlockScript>().SendMessage("SetNewColor");

        swapTarget = null;
        other.transform.parent.GetComponent<BlockScript>().swapTarget = null;
        }

    }



