using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Vector2 pos => transform.position;
    public BlockSO dataBlock;
    public Node Node;
    public int value;
    public StateBlock state;
    public Block mergingBlock; // Block để merge
    public bool merging; // kiểm tra có đang merge hay không

    public SpriteRenderer spriteRenderer;

    //public void Init(BlockType type)
    //{
    //    state = type.state;
    //    spriteRenderer.sprite = type.sprite;
    //}
    ////
    public void SetBlock(Node node)
    {
        if (node != null)
        {
            Node.OccupiedBlock = null;
            Node = node;
            node.OccupiedBlock = this; //gán block bị chiếm đóng của node mới = this

        }
    }
    public void UpdateBlock(StateBlock state, Node node)
    {
        if (node != null)
            node.OccupiedBlock = null;
        Node = node;
        node.OccupiedBlock = this; //gán block bị chiếm đóng của node mới = this
        UpdateState(state);
        UpdateSprite(state);
    }
    public void UpdateSprite(StateBlock state)
    {
        spriteRenderer.sprite = dataBlock.GetDataBlock(state);
    }
    public void UpdateState(StateBlock newState)
    {
        state = newState;
    }
    public void MergeBlock(Block blockToMergeWith)
    {
        mergingBlock = blockToMergeWith; //gán block hiện tại thành block target
        Node.OccupiedBlock = null; //cập nhật ô hiện tại thành không có block chiếm đóng
        blockToMergeWith.merging = true;
    }    
    public bool CanMerge(int value) => this.value == value && !merging && mergingBlock == null;
}


public enum StateBlock
{
    gift,
    cake,
    obstacle,
}
