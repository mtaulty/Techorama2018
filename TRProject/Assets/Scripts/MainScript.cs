using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.InputModule.Utilities.Interactions;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    public void CreateAnimal(string type)
    {
        var newAnimal = this.gameObject.GetComponent<AnimalFactory>().MakeObjectForType(type);

        newAnimal.transform.position =
            Camera.main.transform.position +
            (Camera.main.transform.forward * 3.0f);

        newAnimal.transform.forward = Camera.main.transform.forward;
    }
}
