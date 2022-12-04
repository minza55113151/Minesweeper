using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlockManager : MonoBehaviour
{
    [SerializeField] private Block[] blocks;
    public Block currentBlock;
   
    public bool isMine;
    public int mineCount;

    public int x;
    public int y;


    [SerializeField] private GameObject explosiveShardPrefab;

    
    Ray ray;
    RaycastHit hit;

    public void SetXY(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public void SetMine(int mineCount)
    {
        this.mineCount = mineCount;
        if (mineCount == 0)
        {
            return;
        }
        if(mineCount == -1)
        {
            isMine = true;
            return;
        }
        for (int i = 0; i < blocks.Length; i++)
        {
            if (blocks[i].name == "WhiteBlock")
            {
                TextMeshProUGUI text = blocks[i].gameObject.GetComponentInChildren<TextMeshProUGUI>(true);
                text.text = mineCount.ToString();

                if(mineCount == 1)
                {
                    text.color = Color.blue;
                }
                if (mineCount == 2)
                {
                    text.color = Color.green;
                }
                if (mineCount == 3)
                {
                    text.color = Color.red;
                }
                if (mineCount == 4)
                {
                    text.color = Color.magenta;
                }
                if(mineCount == 5)
                {
                    text.color = Color.yellow;
                }
                if (mineCount == 6)
                {
                    text.color = Color.cyan;
                }
                if (mineCount == 7)
                {
                    text.color = Color.black;
                }
                if (mineCount == 8)
                {
                    text.color = Color.gray;
                }
            }
        }
    }
    private void Start()
    {
        Open("BlackBlock");
        transform.localScale *= 0.9f;
    }

    private void Update()
    {
        if (GameManager.instance.isGameEnd)
        {
            return;
        }
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    LeftClick();
                }
                if (Input.GetMouseButtonDown(1))
                {
                    RightClick();
                }
                if (MapManager.instance.CheckWin())
                {
                    GameManager.instance.GameWin();
                }
            }
        }
    }
    public void LeftClick()
    {
        switch (currentBlock.name)
        {
            case "WhiteBlock":
                // check num
                if (mineCount != 0)
                {
                    MapManager.instance.OpenNumber(this);
                }
                break;
            case "BombBlock":
                break;
            case "BlackBlock":
                if (MapManager.instance.isGenerateMine == false)
                {
                    MapManager.instance.GenerateMine(x, y);
                }
                //check for bomb
                if (isMine == true)
                {
                    //booommmm!!!
                    Open("BombBlock");
                    GameManager.instance.GameOver();
                }
                else
                {
                    Open("WhiteBlock");
                }
                break;
            case "FlagBlock":
                break;
            case "QuestionMarkBlock":
                break;
            default:
                break;
        }
    }
    public void RightClick()
    {
        switch (currentBlock.name)
        {
            case "WhiteBlock":
                break;
            case "BombBlock":
                break;
            case "BlackBlock":
                //change to flag block
                if (MapManager.instance.flagRemain > 0)
                {
                    Open("FlagBlock");
                    MapManager.instance.flagRemain--;
                }
                break;
            case "FlagBlock":
                //change to question mark block
                MapManager.instance.flagRemain++;
                Open("QuestionMarkBlock");
                break;
            case "QuestionMarkBlock":
                //change to black block
                Open("BlackBlock");
                break;
            default:
                break;
        }
    }
    public void Open(string name)
    {
        if(name == "WhiteBlock" && currentBlock.name == "WhiteBlock")
        {
            return;
        }
        foreach (Block block in blocks)
        {
            if (block.name == name)
            {
                block.Open();
                currentBlock = block;
            }
            else
            {
                block.Close();
            }
        }
        if (name == "WhiteBlock" && mineCount == 0)
        {
            MapManager.instance.Open(x, y);
        }
    }
    public void Explode()
    {
        StartCoroutine(CoroutineExplode());
    }
    private IEnumerator CoroutineExplode()
    {
        //yield return new WaitForSeconds(Random.Range(0.5f, 2f));
        yield return null;
        Vector3 center = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Collider[] colliders = Physics.OverlapSphere(center, 2f);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.tag == "Block")
            {
                Vector3 direction = collider.transform.position - center;
                direction = direction.normalized;
                collider.gameObject.GetComponent<Rigidbody>().AddForce(direction * 1000f);
            }
        }
        Destroy(gameObject);
        Instantiate(explosiveShardPrefab, transform.position, Quaternion.identity);
    }
}
