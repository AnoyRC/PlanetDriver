using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityHandler : MonoBehaviour {

	private PlanetHandler attractor;
	private Rigidbody rb;

	public bool placeOnSurface = false;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		attractor = PlanetHandler.instance;
	}
	
	void FixedUpdate ()
	{
		if (placeOnSurface)
			attractor.PlaceOnSurface(rb);
		else
			attractor.Attract(rb);
	}

}
