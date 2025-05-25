using UnityEngine;

public class Resource : MonoBehaviour
{
    public bool IsAvailable = true;

    public void GetResource()
    {
        IsAvailable = false;
    }
    public void CollectResource()
    {
        gameObject.SetActive(false);
    }
}
