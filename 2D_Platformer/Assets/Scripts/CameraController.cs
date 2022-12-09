using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    //[SerializeField]
    //float timeOffSet;

    //[SerializeField]
    //Vector2 posOffSet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 startPos = transform.position;
        //Vector3 endPos = player.transform.position;

        //endPos.x += posOffSet.x;
        //endPos.y += posOffSet.y + 2;
        //endPos.x = -10;

        //transform.position = Vector3.Lerp(startPos, endPos, timeOffSet * Time.deltaTime);

        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 2, -10);
    }
}
