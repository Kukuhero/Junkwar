using UnityEngine;
using System.Collections;

public class Wegpunkt
{
	private Vector3 position;
	private float totaldistance;
	private bool wegfrei;
	private float Entfernung;
	private float distancetotarget;

	public void Wegpunkte_in_Array(Vector3 position2, float totaldistance2, bool wegfrei2, float Entfernung2, float distancetotarget2)
	{
		position = position2;
		totaldistance = totaldistance2;
		wegfrei = wegfrei2;
		Entfernung = Entfernung2;
		distancetotarget = distancetotarget2;

	}
		

	public Vector3 Position_aus_Array ()
	{
		return position;

	}

	public float totaldistance_aus_Array ()
	{
		return totaldistance;

	}

	public bool wegfrei_aus_Array ()
	{
		return wegfrei;

	}

	public float Entfernung_aus_Array ()
	{
		return Entfernung;

	}

	public float distancetotarget_aus_Array ()
	{
		return distancetotarget;

	}
}
