using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collision : MonoBehaviour
{ 
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag.Equals("Cube"))
        {
            if (!CubeCollect.Instance.Cubes.Contains(other.gameObject))
            {
                other.tag = "Collected";
                CubeCollect.Instance.StackCube(other.gameObject, CubeCollect.Instance.Cubes.Count - 1);
                other.gameObject.AddComponent<Collision>();
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                other.gameObject.GetComponent<Collider>().isTrigger = false;
            }
        }
        
    }
    private void OnTriggerExit(Collider Other)
    {
        if (Other.gameObject.tag.Equals("Cube2"))
        {
            for (int i = CubeCollect.Instance.Cubes.Count - 1; i > CubeCollect.Instance.Cubes.IndexOf(Other.gameObject); i--)
            {
                if (gameObject.tag.Equals("Collected"))
                {
                    GameObject Cubei = CubeCollect.Instance.Cubes[i];
                    CubeCollect.Instance.Cubes[i].gameObject.tag = "Cube";
                    Destroy(CubeCollect.Instance.Cubes[i].GetComponent<Collision>());
                    Vector3 cube = CubeCollect.Instance.Cubes[i].transform.position;
                    
                    CubeCollect.Instance.Cubes[i].gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 3, 10), ForceMode.Impulse);

                    Cubei.gameObject.transform.parent = Other.transform;
                    CubeCollect.Instance.Cubes[i].transform.localPosition = new Vector3(0, 2, 0);

                    Debug.Log(CubeCollect.Instance.Cubes[i].transform.position);



                    CubeCollect.Instance.Cubes[i].gameObject.GetComponent<Rigidbody>().useGravity = true;
                    CubeCollect.Instance.Cubes[i].gameObject.GetComponent<Rigidbody>().isKinematic = false;



                    
                    //CubeCollect.Instance.Cubes[i].gameObject.GetComponent<Collider>().isTrigger = true;
                    CubeCollect.Instance.Cubes.RemoveAt(i);

                }



            }
        }
    }


}
