using UnityEngine;

public class AnimalHit : MonoBehaviour, IHittable
{
    public void GetHit()
    {
        Debug.Log(gameObject.name + " wurde getroffen");
    }
}