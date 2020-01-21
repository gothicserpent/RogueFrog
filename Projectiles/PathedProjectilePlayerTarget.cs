using UnityEngine;
using System.Collections;
using MoreMountains.Tools;

namespace MoreMountains.TopDownEngine
{
/// <summary>
/// This class handles the movement of a pathed projectile
/// </summary>
[AddComponentMenu("TopDown Engine/Character/AI/Pathed Projectile Player Target")]
public class PathedProjectilePlayerTarget : MonoBehaviour
{
[Information("A GameObject with this component will move towards its target and get destroyed when it reaches it. Here you can define what object to instantiate on impact. Use the Initialize method to set its destination and speed.",MoreMountains.Tools.InformationAttribute.InformationType.Info,false)]
/// The effect to instantiate when the object gets destroyed
public GameObject DestroyEffect;
/// the destination of the projectile
//protected Transform _destination;
//protected GameObject _player;
protected Vector3 _playerPosition;
/// the movement speed
protected float _speed;

/// <summary>
/// Initializes the specified destination and speed.
/// </summary>
/// <param name="destination">Destination.</param>
/// <param name="speed">Speed.</param>
public virtual void Initialize(Vector3 destination, float speed)
{
	//DESTINATION SHOULD BE VECTOR3 GameObject.FindWithTag("Player").transform.position
	//_destination=destination;
	//_player = GameObject.FindWithTag("Player");
	//_destination = _player.transform;

	//_playerPosition = GameObject.FindWithTag("Player").transform.position; //WAS UNCOMMENTED!

	//this.GetComponent<Projectile>().Direction = Vector3.right;


//destination - origin
//Vector3 dir = (this.transform.position - camera.transform.position).normalized


	this.GetComponent<Projectile>().Direction = (destination - this.transform.position).normalized;                 // set direction of the projectile in Vector3 space

/*
                        var dir = pointB - pointA; //a vector pointing from pointA to pointB
                        var rot = Quaternion.LookRotation(dir, Vector3.up); //calc a rotation that
                        transform.rotation = rot;
 */

//					transform.rotation = newRotation;

	//transform.rotation = Quaternion.LookRotation(this.GetComponent<Projectile>().Direction, Vector3.up);
	//transform.rotation = Quaternion.FromToRotation(this.transform.position, _playerPosition);
	//transform.rotation = Quaternion.Euler(Quaternion.LookRotation(_playerPosition).eulerAngles - Quaternion.LookRotation(this.transform.position).eulerAngles);
	transform.rotation = Quaternion.LookRotation(this.GetComponent<Projectile>().Direction);
	transform.rotation = Quaternion.FromToRotation(Vector3.up, transform.forward);                 // rotate the projectile to face the player


	//Debug.Log("Dest init");
	_speed=speed;                 //this should be zero from this class, only set speed using Projectile class

	//this.GetComponent<Projectile>().GetComponent<Rigidbody2D>().AddForce(this.GetComponent<Projectile>().transform.up * speed);
	this.GetComponent<Projectile>().Speed=speed;

/*
   var projectile : Transform;
   var bulletSpeed : float = 20;

   function Update () {
                // Put this in your update function
                if (Input.GetButtonDown("Fire1")) {

                // Instantiate the projectile at the position and rotation of this transform
                var clone : Transform;
                clone = Instantiate(projectile, transform.position, transform.rotation);

                // Add force to the cloned object in the object's forward direction
                clone.rigidbody.AddForce(clone.transform.forward * shootForce);
                }
   }
 */




	Invoke("DestroyProjectile", this.GetComponent<MMPoolableObject>().LifeTime);                  //forces object to be destroyed within mmpoolable lifetime
}


protected void DestroyProjectile()
{
	Destroy(gameObject);
}



/// <summary>
/// Every frame, me move the projectile's position to its destination
/// </summary>
protected virtual void Update ()                 //don't do anything here, leave it all to the projectile itself
{

	//Debug.Log(this.GetComponent<MMPoolableObject>().LifeTime);
	//if((this.GetComponent<MMPoolableObject>().LifeTime -= Time.deltaTime)>0.0f) //spawn projectile based on fire rate
	//	return;
	//if(this.GetComponent<Projectile>()._health>0)
	//return;

	//transform.position=Vector3.MoveTowards(transform.position,_playerPosition,Time.deltaTime * _speed);
	//var distanceSquared = (_playerPosition - transform.position).sqrMagnitude;
	//if(distanceSquared > .01f * .01f)
	//	return;

/*
                        if (DestroyEffect!=null)
                        {
                                Instantiate(DestroyEffect,transform.position,transform.rotation);
                        }


 */
	//Destroy(gameObject);
}
}
}
