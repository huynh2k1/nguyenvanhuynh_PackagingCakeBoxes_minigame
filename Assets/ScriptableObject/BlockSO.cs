using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ListTypeBlocks", menuName = "ListBlockSO/ListTypeBlock")]
public class BlockSO : ScriptableObject
{
    public List<BlockType> blockTypes;

    public Sprite GetDataBlock(StateBlock newState)
    {
        for (int i = 0; i < blockTypes.Count; i++)
        {
            if ( blockTypes[i].state == newState)
            {
                return blockTypes[i].sprite;
            }
        }
        return null;
    }
}
[System.Serializable]
public class BlockType
{
    public Sprite sprite;
    public StateBlock state;
}