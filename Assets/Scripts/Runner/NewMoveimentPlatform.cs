using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMoveimentPlatform : MonoBehaviour
{
    public GameObject Tile1, Tile2;
    private Vector3 Tile1OriginPosition, Tile2OriginPosition;
    private bool check = false;

    private void Start()
    {
        Tile1OriginPosition = Tile1.transform.position;
        Tile2OriginPosition = Tile2.transform.position;
    }

    private void Update()
    {
        if (Statistic.isGameStart)
        {
            if (!check)
                Movement(Tile1, Tile2, Tile1OriginPosition);
            if (check)
                Movement(Tile2, Tile1, Tile1OriginPosition);
            if (Tile2.transform.position == Tile1OriginPosition)
            {
                check = true;
                Tile1.transform.position = Tile2OriginPosition;
            }
            if (Tile1.transform.position == Tile1OriginPosition)
            {
                check = false;
                Tile2.transform.position = Tile2OriginPosition;
            }
        }
    }


    private void Movement(GameObject Tile1, GameObject Tile2, Vector3 Tile1OriginPosition)
    {
        Tile1.transform.position = Vector3.MoveTowards(Tile1.transform.position, Tile1.transform.position + new Vector3(0, 0, -1), Time.deltaTime * Statistic.Speed * 3);
        Tile2.transform.position = Vector3.MoveTowards(Tile2.transform.position, Tile1OriginPosition, Time.deltaTime * Statistic.Speed * 3);
    }

    public void ResetPosition()
    {
        Tile1.transform.position = Tile1OriginPosition;
        Tile2.transform.position = Tile2OriginPosition;
    }
}
