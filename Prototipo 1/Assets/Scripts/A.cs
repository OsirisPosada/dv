using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sl : MonoBehaviour
{
    float FuerzaM = 0.1f;
    int FuerzaSalto = 5;
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
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (saltosRestantes < 2 || ensuelo )
            {
                saltosRestantes++;
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, FuerzaSalto), ForceMode2D.Impulse);
                print("Presiono tecla arriba");
            }

        }

    }

    void FixedUpdate()
    {


        if (Input.GetKey(KeyCode.S))
        {
            transform.localScale = transform.localScale + new Vector3(0, 0.1f, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position = transform.position + new Vector3(-FuerzaM, 0, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position = transform.position + new Vector3(FuerzaM, 0, 0);
        }

    }


}
