using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour
{
    [SerializeField]
    private Sprite wallSprite;

    [SerializeField]
    private float width;

    [SerializeField]
    private float height;

    [SerializeField]
    private int rows;

    [SerializeField]
    private int cols;

    [SerializeField]
    private float breakForce;

    [SerializeField]
    private float edgeBreakForce;

    private GameObject[][] walls;


    void Awake()
    {
        walls = new GameObject[cols][];
        for (int i = 0; i < cols; i++)
        {
            walls[i] = new GameObject[rows];
        }

        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                GameObject o = new GameObject("Wall" + x + y);
                o.transform.parent = transform;
                o.AddComponent<SpriteRenderer>();
                SpriteRenderer r = o.GetComponent<SpriteRenderer>();
                r.sprite = wallSprite;
                o.transform.localScale = new Vector3(width, height, 1.0f);
                o.transform.position = transform.position;
                o.transform.Translate(new Vector3(x * width, y * height, 0));

                o.AddComponent<BoxCollider2D>();
                o.AddComponent<Rigidbody2D>();
                Rigidbody2D rigidbodyComponent = o.GetComponent<Rigidbody2D>();
                rigidbodyComponent.useAutoMass = true;

                if (x > 0)
                {
                    o.AddComponent<FixedJoint2D>();

                    Component[] components = o.GetComponents(typeof(FixedJoint2D));
                    FixedJoint2D fixedJoint = (FixedJoint2D)components[0];
                    fixedJoint.connectedBody = walls[x - 1][y].GetComponent<Rigidbody2D>();
                    if(y > rows / 2)
                    {
                        fixedJoint.breakForce = breakForce * (y - rows/2 + 1);
                    }
                    else
                    {
                        fixedJoint.breakForce = breakForce * (rows/2 - y + 1);
                    }
                }

                if (y > 0)
                {
                    o.AddComponent<FixedJoint2D>();

                    Component[] components = o.GetComponents(typeof(FixedJoint2D));
                    FixedJoint2D fixedJoint;
                    if (x > 0)
                    {
                        fixedJoint = (FixedJoint2D)components[1];
                    }
                    else
                    {
                        fixedJoint = (FixedJoint2D)components[0];
                    }
                    fixedJoint.connectedBody = walls[x][y - 1].GetComponent<Rigidbody2D>();
                    if (y > rows / 2)
                    {
                        fixedJoint.breakForce = breakForce * (y - rows / 2 + 1);
                    }
                    else
                    {
                        fixedJoint.breakForce = breakForce * (rows / 2 - y + 1);
                    }

                }

                walls[x][y] = o;

            }
        }
        if (cols > rows)
        {
            GameObject o = new GameObject("LeftWall");
            o.transform.parent = transform;
            o.AddComponent<SpriteRenderer>();
            SpriteRenderer r = o.GetComponent<SpriteRenderer>();
            r.sprite = wallSprite;

            o.transform.localScale = new Vector3(width, height * rows, 1.0f);
            o.transform.position = transform.position;
            o.transform.Translate(new Vector3(-1 * width, 0.5f * rows * height, 0));

            o.AddComponent<BoxCollider2D>();
            r.enabled = false;
            o.AddComponent<Rigidbody2D>();
            Rigidbody2D rigidbodyComponent = o.GetComponent<Rigidbody2D>();
            rigidbodyComponent.useAutoMass = true;
            rigidbodyComponent.bodyType = RigidbodyType2D.Static;
            for (int i = 0; i < rows; i++)
            {
                o.AddComponent<FixedJoint2D>();

                Component[] components = o.GetComponents(typeof(FixedJoint2D));
                FixedJoint2D fixedJoint = (FixedJoint2D)components[i];
                fixedJoint.connectedBody = walls[0][i].GetComponent<Rigidbody2D>();
                fixedJoint.breakForce = edgeBreakForce;
            }

            o = new GameObject("RightWall");
            o.transform.parent = transform;
            o.AddComponent<SpriteRenderer>();
            r = o.GetComponent<SpriteRenderer>();
            r.sprite = wallSprite;
            o.transform.localScale = new Vector3(width, height * rows, 1.0f);
            o.transform.position = transform.position;
            o.transform.Translate(new Vector3(cols * width, 0.5f * rows * height, 0));

            o.AddComponent<BoxCollider2D>();
            r.enabled = false;
            o.AddComponent<Rigidbody2D>();
            rigidbodyComponent = o.GetComponent<Rigidbody2D>();
            rigidbodyComponent.useAutoMass = true;
            rigidbodyComponent.bodyType = RigidbodyType2D.Static;
            for (int i = 0; i < rows; i++)
            {
                o.AddComponent<FixedJoint2D>();

                Component[] components = o.GetComponents(typeof(FixedJoint2D));
                FixedJoint2D fixedJoint = (FixedJoint2D)components[i];
                fixedJoint.connectedBody = walls[cols-1][i].GetComponent<Rigidbody2D>();
                fixedJoint.breakForce = edgeBreakForce;
            }


        }
        else
        {
            GameObject o = new GameObject("BottomWall");
            o.transform.parent = transform;
            o.AddComponent<SpriteRenderer>();
            SpriteRenderer r = o.GetComponent<SpriteRenderer>();
            r.sprite = wallSprite;

            o.transform.localScale = new Vector3(width * cols, height, 1.0f);
            o.transform.position = transform.position;
            o.transform.Translate(new Vector3(0.5f * width * cols, -1f * height, 0));

            o.AddComponent<BoxCollider2D>();
            r.enabled = false;
            o.AddComponent<Rigidbody2D>();
            Rigidbody2D rigidbodyComponent = o.GetComponent<Rigidbody2D>();
            rigidbodyComponent.useAutoMass = true;
            rigidbodyComponent.bodyType = RigidbodyType2D.Static;
            for (int i = 0; i < cols; i++)
            {
                o.AddComponent<FixedJoint2D>();

                Component[] components = o.GetComponents(typeof(FixedJoint2D));
                FixedJoint2D fixedJoint = (FixedJoint2D)components[i];
                fixedJoint.connectedBody = walls[i][0].GetComponent<Rigidbody2D>();
                fixedJoint.breakForce = edgeBreakForce;
            }

            o = new GameObject("TopWall");
            o.transform.parent = transform;
            o.AddComponent<SpriteRenderer>();
            r = o.GetComponent<SpriteRenderer>();
            r.sprite = wallSprite;
            o.transform.localScale = new Vector3(width * cols, height, 1.0f);
            o.transform.position = transform.position;
            o.transform.Translate(new Vector3(0.5f * cols * width, rows * height, 0));

            o.AddComponent<BoxCollider2D>();
            r.enabled = false;
            o.AddComponent<Rigidbody2D>();
            rigidbodyComponent = o.GetComponent<Rigidbody2D>();
            rigidbodyComponent.useAutoMass = true;
            rigidbodyComponent.bodyType = RigidbodyType2D.Static;
            for (int i = 0; i < cols; i++)
            {
                o.AddComponent<FixedJoint2D>();

                Component[] components = o.GetComponents(typeof(FixedJoint2D));
                FixedJoint2D fixedJoint = (FixedJoint2D)components[i];
                fixedJoint.connectedBody = walls[i][rows-1].GetComponent<Rigidbody2D>();
                fixedJoint.breakForce = edgeBreakForce;
            }
        }


    }
}