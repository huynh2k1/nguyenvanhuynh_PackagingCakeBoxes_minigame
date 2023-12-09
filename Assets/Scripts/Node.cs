using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Block OccupiedBlock;//khối bị chiếm đóng
    public Vector2 pos => transform.position;
}
