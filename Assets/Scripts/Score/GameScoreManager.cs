using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScoreManager : MonoBehaviour
{

    public int Score;
    public int FirstBoxValue = 10;
    public int GatesValue = 1;

    [Header("Box Values")]
    public int EmptyBox = 10;
    public int FillBox = 11;
    public int CloseBox = 12;
    public int PackedBox = 13;


}
