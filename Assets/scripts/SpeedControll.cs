using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
public class SpeedControll : MonoBehaviour
{
    public Move player;
    private Vector2 startPosition;
    [SerializeField]
    private List<Transform> pivotPoints = new List<Transform>();
    private int pivotIndex = 0;
    enum StateMachineType
    {
        Slow
    }
    private StateMachineType state = StateMachineType.Slow;

    public findplayer()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Move>();
    }

}