using UnityEngine;
using System.Collections;
using MoreMountains.Tools;

using Pathfinding;

namespace MoreMountains.TopDownEngine
{
	/// <summary>
	/// Spawns pathed projectiles
	/// </summary>
	[AddComponentMenu("TopDown Engine/Character/AI/Pathed Projectile Spawner Player Target")]
	public class PathedProjectileSpawnerPlayerTarget : MonoBehaviour
	{
		[Header("Pathed Projectile Spawner")]
		[Information("A GameObject with this component will spawn projectiles at the specified fire rate.",MoreMountains.Tools.InformationAttribute.InformationType.Info,false)]
		/// the pathed projectile's destination
		protected Transform Destination; //this is set in the pathedprojectileplayertarget.cs file
		/// the projectiles to spawn
		public PathedProjectilePlayerTarget Projectile;
		/// the effect to instantiate at each spawn
		public GameObject SpawnEffect;
		/// the speed of the projectiles
		public float Speed = 100.0f; //only set this from the projectile script on the projectile.
		/// the frequency of the spawns
		public float FireRate;

		protected float _nextShotInSeconds;
		public float SpawnRange=0.0f;
		public float DestinationRange=0.0f;

		//protected bool Instantiated; //already pulled projectile

	    /// <summary>
	    /// Initialization
	    /// </summary>
	    protected virtual void Start ()
		{
			//Invoke("SetDestination", 0.5f);  // SHOULD DO THIS PER PROJECTILE IN THE ACTUAL PROJECTILE SCRIPT
			//Destination = GameObject.FindWithTag("Player").transform;
			_nextShotInSeconds=FireRate;
			//Instantiated = false;
		}

		protected void SetDestination()
    {
		//	Destination = GameObject.FindWithTag("Player").transform;
      //Debug.Log("Setting Destination");
    }

	    /// <summary>
	    /// Every frame, we check if we need to instantiate a new projectile
	    /// </summary>
	    protected virtual void Update ()
		{
			//Destination = GameObject.FindWithTag("Player").transform;
			if((_nextShotInSeconds -= Time.deltaTime)>0)// && Instantiated && Time.frameCount < 10) //spawn projectile based on fire rate
				return;

			if(!GetComponent<IAstarAI>().canMove) //only spawn the projectile if the AI is able to move (the ai brain has detected the player)
				return;

			_nextShotInSeconds = FireRate;
			var projectile = (PathedProjectilePlayerTarget) Instantiate(Projectile, transform.position + new Vector3(Random.Range(-SpawnRange, SpawnRange),Random.Range(-SpawnRange, SpawnRange),Random.Range(-SpawnRange, SpawnRange)),transform.rotation);
			projectile.Initialize(GameObject.FindWithTag("Player").transform.position + new Vector3(Random.Range(-DestinationRange, DestinationRange),Random.Range(-DestinationRange, DestinationRange),Random.Range(-DestinationRange, DestinationRange)),Speed);

			//Debug.Log("Instantiating");

			if (SpawnEffect!=null)
			{
				Instantiate(SpawnEffect,transform.position,transform.rotation);
			}

			//Instantiated = true;
		}

		/// <summary>
		/// Debug mode
		/// </summary>
		public virtual void OnDrawGizmos()
		{
			if (Destination==null)
				return;

			Gizmos.color=Color.gray;
			Gizmos.DrawLine(transform.position,Destination.position);
		}
	}
}
