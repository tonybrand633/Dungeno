using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosSignal : MonoBehaviour
{
    public KeepInRoom kim;
    public float GridMult;
    public bool Draging;
    public GameObject orignalPoint;
    float distanceStart;
    float distanceCurrent;

    // Start is called before the first frame update
    void Start()
    {
        kim = GetComponent<KeepInRoom>();
        orignalPoint = Instantiate(orignalPoint,this.transform.position, Quaternion.identity);
        //orignalPoint.transform.parent = this.transform;
        distanceStart = (this.transform.position - orignalPoint.transform.localPosition).magnitude;
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 rPos = kim.GetRoomPosOnGrid(GridMult);

        distanceCurrent = (this.transform.position - orignalPoint.transform.localPosition).magnitude;
        //Debug.Log(transform.position);
        //Debug.Log(orignalPoint.transform.localPosition);
        //Debug.Log(distanceCurrent);
        
        
        if (distanceCurrent!=distanceStart) 
        {
            Draging = true;
            Debug.Log("RoomPosOnGrid: " + rPos + "RoomPos: " + kim.roomPos + "RoomNum: " + kim.roomNum);
            orignalPoint.transform.position = this.transform.position;
            distanceStart = (this.transform.position - orignalPoint.transform.localPosition).magnitude;
        }


        
    }
}
