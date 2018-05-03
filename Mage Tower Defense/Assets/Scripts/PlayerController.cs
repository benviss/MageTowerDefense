using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

  Vector3 velocity;
  Rigidbody myRigidbody;

  void Start()
  {
    myRigidbody = GetComponent<Rigidbody>();
  }

  public void Move(Vector3 _velocity)
  {
        //Vector3 heightCorrectedPoint = transform.TransformDirection( _velocity);//Camera.main.transform.TransformDirection(new Vector3(_velocity.x, _velocity.y, _velocity.z));
    //heightCorrectedPoint.y = 1;
    velocity = _velocity;
  }

  public void LookAt(Vector3 lookPoint)
  {
    Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
    transform.LookAt(heightCorrectedPoint);
  }

  void FixedUpdate()
  {
    //transform.position += transform.forward * velocity.z * Time.deltaTime;
    //transform.position += transform.right * velocity.x * Time.deltaTime;

    myRigidbody.MovePosition(myRigidbody.position + velocity  * Time.fixedDeltaTime);
  }
}