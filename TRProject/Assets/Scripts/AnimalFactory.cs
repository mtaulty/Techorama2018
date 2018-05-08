using System;
using UnityEngine;

public class AnimalFactory : MonoBehaviour
{
    public GameObject lionPrefab;
    public GameObject owlPrefab;
    public GameObject pigPrefab;

    public GameObject MakeObjectForType(string type)
    {
        GameObject gameObject = null;

        switch (type)
        {
            case "lion":
                gameObject = GameObject.Instantiate(this.lionPrefab);
                break;
            case "pig":
                gameObject = GameObject.Instantiate(this.pigPrefab);
                break;
            case "owl":
                gameObject = GameObject.Instantiate(this.owlPrefab);
                break;
            default:
                break;
        }
        // Give the game object a unique name which reflects the type it is
        gameObject.name = string.Format("{0}_{1}", type, Guid.NewGuid());

        return (gameObject);
    }
}
