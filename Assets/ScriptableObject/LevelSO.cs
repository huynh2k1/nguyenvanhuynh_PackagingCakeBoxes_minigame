using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ListLevel", menuName = "LevelSO/ListLevel")]
public class LevelSO : ScriptableObject
{
    public List<LevelData> listLevels;
}
[System.Serializable]
public class LevelData
{
    public int index;
    public int width;
    public int height;
    public Vector2 posGif;
    public List<Vector2> posCake;
    public List<Vector2> posObstacle;
}
