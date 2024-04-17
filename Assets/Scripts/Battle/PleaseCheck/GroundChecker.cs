using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private string groundTag = "Ground";
    private bool isGrounded = false;
    private bool isGroundEnter, isGroundStay, isGroundExit;

    public bool IsGrounded()
    {
        if (isGroundEnter || isGroundStay)
        {
            isGrounded = true;
        }
        else if (isGroundExit)
        {
            isGrounded = false;
        }

        isGroundEnter = false;
        isGroundStay = false;
        isGroundExit = false;

        return isGrounded;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == groundTag)
        {
            isGroundEnter = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == groundTag)
        {
            isGroundStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == groundTag)
        {
            isGroundExit = true;
        }
    }
    /*[SerializeField] bool checkBuilderWagon = true;

    #region
    private string groundTag = "Ground";
    //private string builderTag = "Builder";
    private string wagonTag = "Wagon";
    private bool isGrounded = false;
    private bool isGroundEnter, isGroundStay, isGroundExit;
    #endregion

    public bool IsGrounded()
    {
        if (isGroundEnter || isGroundStay)
        {
            isGrounded = true;
        }
        else if (isGroundExit)
        {
            isGrounded = false;
        }

        isGroundEnter = false;
        isGroundStay = false;
        isGroundExit = false;

        return isGrounded;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == groundTag)
        {
            isGroundEnter = true;
        }
        else if (checkBuilderWagon && collision.tag == wagonTag)
        {
            isGroundEnter = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == groundTag)
        {
            isGroundStay = true;
        }
        else if (checkBuilderWagon && collision.tag == wagonTag)
        {
            isGroundStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == groundTag)
        {
            isGroundExit = true;
        }
        else if (checkBuilderWagon && collision.tag == wagonTag)
        {
          //  isGroundStay = true;
            isGroundExit = true;
        }
    }*/
}
