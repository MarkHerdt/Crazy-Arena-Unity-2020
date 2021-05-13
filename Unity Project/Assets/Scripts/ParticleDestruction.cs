using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleDestruction : MonoBehaviour
{
	// If true, deactivate the object instead of destroying it
	public bool OnlyDeactivate;
	
	void OnEnable()
	{
		StartCoroutine(CheckIfAlive());
	}
	
	IEnumerator CheckIfAlive()
	{
		ParticleSystem particleSystem = this.GetComponent<ParticleSystem>();
		
		while(true && particleSystem != null)
		{
			yield return new WaitForSeconds(.5f);

			if(!particleSystem.IsAlive(true))
			{
				if(OnlyDeactivate)
				{
					#if UNITY_3_5
						this.gameObject.SetActiveRecursively(false);
					#else
						this.gameObject.SetActive(false);
					#endif
				}
				else
					GameObject.Destroy(this.gameObject);
				break;
			}
		}
	}
}
