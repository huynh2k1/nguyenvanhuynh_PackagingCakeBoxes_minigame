using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using System.Threading;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState stateGame;
    public LevelSO levelSO;

    [Header("-------Prefab-------")]
    public Block blockPrefab;
    public Node nodePrefab;

    [Header("------List Blocks-------")]
    public List<Block> _obstacles; // list block idle
    public List<Block> _blocks; // list block can move
    public List<Node> _nodes; //list node

    [Header("------Size Board------")]
    private int width;
    private int height;

    public int numTarget;
    public int score = 0;
    public float _travelTime;
    float timeLimit = 46f;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        ChangeState(GameState.Home);
    }

    private void Update()
    {
        if (stateGame != GameState.Playing)
        {
            timeLimit = 46;
            return;
        }

        if(timeLimit <= 0)
        {
            ChangeState(GameState.Lose);
            return;
        }
        if(timeLimit > 0)
        {
            timeLimit -= Time.deltaTime;
            CanvasManager.instance.canvasGame.UpdateTextTime((int)timeLimit);

        }
    }

    public void ChangeState(GameState newState, int idLevel = 0)
    {
        stateGame = newState;
        switch (newState)
        {
            case GameState.Home:
                CanvasManager.instance.ShowHome(true);
                CanvasManager.instance.ShowUIGame(false);
                break;

            case GameState.GenerateLevel:
                InitGrid(idLevel);
                break;

            case GameState.SpawningBlocks:
                LoadDataLevel(idLevel);
                break;

            case GameState.Playing:
                CanvasManager.instance.ShowUIGame(true);
                break;

            case GameState.Win:
                PrefData.LevelUnlocked = PrefData.CurLevel + 1;
                CheckTimeToSetStar();
                if(PrefData.LevelUnlocked > 8)
                {
                    PrefData.LevelUnlocked = 8;
                }
                DOVirtual.DelayedCall(1f, () =>
                {
                    PopupController.instance.ShowPopupWin(true);
                });
                break;

            case GameState.Lose:
                DOVirtual.DelayedCall(1f, () =>
                {
                    PopupController.instance.ShowPopupLose(true);
                });
                break;
        }
    }

    private void InitGrid(int id)
    {
        ClearBlocks();
        ClearObstacles();
        ClearNodes();

        width = levelSO.listLevels[id].width;
        height = levelSO.listLevels[id].height;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 pos = new Vector2(x, y);
                Node node = Instantiate(nodePrefab, pos, Quaternion.identity);
                _nodes.Add(node);
                node.name = "(" + x + "," + y + ")";    
            }
        }

        Vector2 center = new Vector2((float)width / 2 - 0.5f, (float)height / 2 - 0.5f);
        Camera.main.transform.position = new Vector3(center.x, center.y, -10f);

        ChangeState(GameState.SpawningBlocks, id);
    }

    private void ClearNodes()
    {
        if(_nodes.Count != 0)
        {
            foreach (Node node in _nodes)
            {
                Destroy(node.gameObject);
            }
            _nodes.Clear();
        }
        _nodes = new List<Node>();
    }

    private void ClearBlocks()
    {
        if(_blocks.Count != 0)
        {
            foreach (Block block in _blocks)
            {
                Destroy(block.gameObject);
            }
            _blocks.Clear();
        }
        _blocks = new List<Block>();
    }

    private void ClearObstacles()
    {
        if(_obstacles.Count != 0)
        {
            foreach(Block block in _obstacles)
            {
                Destroy(block.gameObject);
            }
            _obstacles.Clear();
        }
    }

    private void LoadDataLevel(int id)
    {
        numTarget = levelSO.listLevels[id].posCake.Count;
        foreach (Node node in _nodes)
        {
            for (int i = 0; i < levelSO.listLevels[id].posCake.Count; i++)
            {
                if (node.pos == levelSO.listLevels[id].posCake[i])
                {
                    InstantiateBlock(StateBlock.cake, node, node.pos);
                }
            }
            if (node.pos == levelSO.listLevels[id].posGif)
            {
                InstantiateBlock(StateBlock.gift, node, node.pos);
            }
            for (int j = 0; j < levelSO.listLevels[id].posObstacle.Count; j++)
            {
                if (node.pos == levelSO.listLevels[id].posObstacle[j])
                {
                    InstantiateBlock(StateBlock.obstacle, node, node.pos);
                }
            }
        }
        DOVirtual.DelayedCall(1f, () =>
        {
            ChangeState(GameState.Playing);
        });
    }

    void InstantiateBlock(StateBlock state, Node node, Vector2 position)
    {
        Block newBlock = Instantiate(blockPrefab, position, Quaternion.identity);
        newBlock.UpdateBlock(state, node);

        if(state == StateBlock.obstacle)
        {
            _obstacles.Add(newBlock);
        }
        else
        {
            _blocks.Add(newBlock);
        }
    }

    private void CheckNumTarget()
    {
        if(numTarget > 0)
        {
            numTarget--;
            if(numTarget == 0)
            {
                ChangeState(GameState.Win);
            }
        }
    }
    private void CheckTimeToSetStar()
    {
        if(timeLimit > 30f)
            score = 3;
        else if(timeLimit > 20f)
            score = 2;
        else 
            score = 1;
        
        if(score > PrefData.GetStarActive(PrefData.CurLevel))
            PrefData.SetStarActive(PrefData.CurLevel, score);

    }
    public void Shift(Vector2 dir)
    {
        var orderedBlocks = _blocks.OrderBy(b => b.pos.x).ThenBy(b => b.pos.y).ToList();//sắp xếp list block theo x tăng dần, sau đó đến y tăng dần
        if(dir == Vector2.right || dir == Vector2.up) orderedBlocks.Reverse(); // đảo ngược list
        

        foreach(Block block in orderedBlocks)
        {
            Node curNode = block.Node;
            do
            {
                block.SetBlock(curNode);

                Node possibleNode = GetNodeAtPosition(curNode.pos + dir);

                if (possibleNode != null)
                {
                    if (possibleNode.OccupiedBlock != null)
                    {
                        if(curNode.pos.y > possibleNode.pos.y && possibleNode.OccupiedBlock.state == StateBlock.gift)
                        {
                            CheckNumTarget();
                            block.MergeBlock(possibleNode.OccupiedBlock);
                        }
                    }
                    //Nêu mà ô tiếp theo không có gì thì gán ô target = ô tiếp theo
                    if (possibleNode.OccupiedBlock == null)
                    {
                        curNode = possibleNode;
                    }
                }
            } while (curNode != block.Node);
        }

        Sequence sequence = DOTween.Sequence();

        foreach (var block in orderedBlocks)
        {
            var movePoint = block.mergingBlock != null ? block.mergingBlock.Node.pos : block.Node.pos;
            sequence.Insert(0, block.transform.DOMove(movePoint, _travelTime));
        }

        sequence.OnComplete(() =>
        {
            foreach (var block in orderedBlocks.Where(b => b.mergingBlock != null))
            {
                MergeBlocks(block);
            }
        });

    }

    void MergeBlocks(Block baseBlock)
    {
        RemoveBlock(baseBlock);
    }

    public void RemoveBlock(Block block)
    {
        _blocks.Remove(block);
        Destroy(block.gameObject);
    }

    private Node GetNodeAtPosition(Vector2 pos) => _nodes.FirstOrDefault(n => n.pos == pos);
}
public enum GameState
{
    Home,
    GenerateLevel,
    SpawningBlocks,
    Playing,
    Win,
    Lose,
}
