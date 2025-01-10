using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    //sample blocks from where to create new blocks
    public List<LevelBlock> legoBlocks = new List<LevelBlock>();
    // blocks added to the game
    List<LevelBlock> currentBlocks = new List<LevelBlock>();
    // Start is called before the first frame update

    public Transform initialPoint;
    // public Transform exitPoint;
    private static LevelGenerator _sharedInstance;
    public static LevelGenerator sharedInstance
    {
        get
        {
            return _sharedInstance;
        }
        // set
        // {
        //     _sharedInstance = value;
        // }
    }

    public byte initialBlockNumber = 2;
    private void Awake()
    {
        _sharedInstance = this;
        createInitialBlocks();
    }
    public void createInitialBlocks()
    {
        if (currentBlocks.Count > 0) return;
        for (byte i = 0; i < initialBlockNumber; i++)
            AddNewBlock(true);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddNewBlock(bool initialBlocks = false)
    {
        int randNumber = initialBlocks ? 0 : Random.Range(0, legoBlocks.Count); // legoBlocks.Count-1
        var block = Instantiate(legoBlocks[randNumber]);
        block.transform.SetParent(this.transform, false); //added this
        Vector3 blockPosition = Vector3.zero;
        if (currentBlocks.Count == 0)
        {
            blockPosition = initialPoint.position;
        }
        else
        {
            int lastBlockPos = currentBlocks.Count - 1;
            blockPosition = currentBlocks[lastBlockPos].exit.position;
        }
        block.transform.position = blockPosition;
        currentBlocks.Add(block);
    }
    public void RemoveOldBlock()
    {
        var oldblock = currentBlocks[0];
        currentBlocks.Remove(oldblock);
        Destroy(oldblock.gameObject);
    }
    public void RemoveAllBlocks()
    {
        while (currentBlocks.Count > 0)
        {
            RemoveOldBlock();
        }
    }
}
