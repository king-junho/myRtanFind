using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeTxt : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void timeLimit()
    {

        //Debug.Log("3�� �Ǳ� ��?");
        anim.SetBool("isLimit", true);
    }
}
