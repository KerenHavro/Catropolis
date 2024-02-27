using UnityEngine;

public class DestroyThis : MonoBehaviour
{
    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    public void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }

    public void ActivateGameObject()
    {
        gameObject.SetActive(true);
    }
}



