using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sl : MonoBehaviour
{
    float FuerzaM = 0.1f;
    int FuerzaSalto = 6;
    public int saltosRestantes;


    public bool ensuelo = true;


    // Start is called before the first frame update
    void Start()
    {
        print("Se inició el código");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (saltosRestantes < 2 || ensuelo )
            {
                saltosRestantes++;
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, FuerzaSalto), ForceMode2D.Impulse);
                print("Osiris");
            }

        }

    }

    void FixedUpdate()
    {


        if (Input.GetKey(KeyCode.DownArrow))
        {
            
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = transform.position + new Vector3(-FuerzaM, 0, 0);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = transform.position + new Vector3(FuerzaM, 0, 0);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ensuelo = true;

        saltosRestantes = 0;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        ensuelo = false;
 


    }
}
