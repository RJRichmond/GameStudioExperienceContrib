using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    LineRenderer line;
    LineRenderer ProjectileLine;
    public float BasePower;
    float powerUpTime;
    public Slider powerSlider;
    public int ShotCount;

    Vector3 startPosition;
    Vector3 endPosition;
    Vector3 power;
    Vector3 minimumPower;
    Vector3 maximumPower;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        line = this.GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (ShotCount > 0) 
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 direction = Vector3.zero;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 ballPosition = new Vector3(this.transform.position.x, 0.1f, this.transform.position.z);
                Vector3 mousePosition = new Vector3(hit.point.x, 0.1f, hit.point.z);
                Vector3 LinePosition = new Vector3(ballPosition.x-mousePosition.x,0.1f,ballPosition.z-mousePosition.z);

                line.SetPosition(0, ballPosition);
                line.SetPosition(1, ballPosition + LinePosition);
                
                //direction = (mousePosition - ballPosition).normalized;
                //Everything which is commented is to do with the old movement of the ball and the old power system.
            }


            if (Input.GetMouseButtonDown(0))
            {
                startPosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                line.enabled = true;

                /*
                 powerUpTime += Time.deltaTime;
                BasePower = Mathf.PingPong(powerUpTime, 1);
                powerSlider.value = BasePower;
                BasePower *= 10;
                 */


                Debug.Log("Click");
            }

            if (Input.GetMouseButtonUp(0))
            {
                line.enabled = false;
                endPosition = Input.mousePosition;
                Shoot(endPosition - startPosition);
                /*
                rb.AddForce(direction*BasePower, ForceMode.Impulse);
                BasePower = 0;
                powerSlider.value = BasePower;
                powerUpTime = 0f;
                 */
                ShotCount--;
                Debug.Log("released");
            }
        }
    }

    void Shoot(Vector3 Force) 
    {
        rb.AddForce(new Vector3(Force.y, 0, -Force.x) * 3);
    }
}
