using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMan : MonoBehaviour
{

    public float speed = 4.0f;

    private Vector2 direction = Vector2.zero;

    private Node currentNode;

    // Start is called before the first frame update
    void Start()
    {
        Node node = GetNodeAtPosition(transform.localPosition);

        if (node != null)
        {
            currentNode = node;
            Debug.Log (currentNode);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();

      //  Move ();

        UpdateOrientation();
    }

    void CheckInput ()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector2.left;
            MoveToNode(direction);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector2.right;
            MoveToNode(direction);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            direction = Vector2.up;
            MoveToNode(direction);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector2.down;
            MoveToNode(direction);
        }
    }

    void Move ()
    {
        transform.localPosition += (Vector3)(direction * speed) * Time.deltaTime;
    }

    void MoveToNode (Vector2 d)
    {
        Node moveToNode = CanMove (d);

        if (moveToNode != null)
        {
            transform.localPosition = moveToNode.transform.position;
            currentNode = moveToNode;
        }
    }

    void UpdateOrientation()
    {
        if (direction == Vector2.left)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direction == Vector2.right)
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direction == Vector2.up)
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
        else if (direction == Vector2.down)
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.localRotation = Quaternion.Euler(0, 0, 270);
        }
    }

     Node CanMove (Vector2 d)
    {
        Node moveToNode = null;

        for (int i = 0; i < currentNode.neighbors.Length; i++)
        {
            if (currentNode.validDirections [i] == d)
            {
                moveToNode = currentNode.neighbors[i];
                break;
            }
        }

        return moveToNode;
    } 

    Node GetNodeAtPosition (Vector2 pos)
    {
        GameObject tile = GameObject.Find("game").GetComponent<GameBoard>().board[(int)pos.x, (int)pos.y];
        
        if (tile != null)
        {
            return tile.GetComponent<Node>();
        }

        return null;
    }
}
