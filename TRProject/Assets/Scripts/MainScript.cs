using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.InputModule.Utilities.Interactions;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    public void OnFix()
    {
        var focusedObject = this.GetFocusedObject();

        if (focusedObject != null)
        {
            var manipulatable = focusedObject.GetComponent<TwoHandManipulatable>();

            if (manipulatable != null)
            {
                // Don't want to move it any more
                Destroy(manipulatable);

                // And store where it was
                this.gameObject.GetComponent<AnimalPersistence>().StoreAnimal(focusedObject);
            }
        }
    }
    GameObject GetFocusedObject()
    {
        GameObject focusedObject = null;
        IPointingSource pointingSource;

        FocusManager.Instance.TryGetSinglePointer(out pointingSource);

        if (pointingSource != null)
        {
            focusedObject = FocusManager.Instance.GetFocusedObject(pointingSource);
        }
        return (focusedObject);
    }
    public void CreateAnimal(string type)
    {
        var newAnimal = this.gameObject.GetComponent<AnimalFactory>().MakeObjectForType(type);

        newAnimal.transform.position =
            Camera.main.transform.position +
            (Camera.main.transform.forward * 3.0f);

        newAnimal.transform.forward = Camera.main.transform.forward;
    }
}
