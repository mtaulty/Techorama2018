using HoloToolkit.Unity;
using UnityEngine;

public class AnimalPersistence : MonoBehaviour {

    public void Update()
    {
        if (!this.reloadedAnimals &&
            WorldAnchorManager.Instance != null &&
            WorldAnchorManager.Instance.AnchorStore != null)
        {
            this.reloadedAnimals = true;

            var anchorIds = WorldAnchorManager.Instance.AnchorStore.GetAllIds();

            foreach (var anchorId in anchorIds)
            {
                // Using the name of the anchor (e.g. 1.0_1.0_1.0_lion) turn it back
                // into the right animal object at the right scale.
                var animalInstance = this.InstantiateAnimalFromNameAndScale(anchorId);

                // And ask the device to put it back in the same location, orientation
                // in the real-world.
                WorldAnchorManager.Instance.AnchorStore.Load(anchorId, animalInstance);
            }            
        }
    }
    public void StoreAnimal(GameObject gameObject)
    {
        // Make a name that includes the type of animal (lion) and its
        // current scale (this is just us being lazy and not wanting
        // to store the scale seperately)
        var anchorName = GetAnchorNameForObjectWithScale(gameObject);

        // Ask the platform to add an 'anchor' to this object with this
        // name and to persist it over iterations of the app.
        WorldAnchorManager.Instance.AttachAnchor(gameObject, anchorName);
    }
    static string GetAnchorNameForObjectWithScale(GameObject gameObject)
    {
        // We take the object's current name which should be something like
        // lion_GUID
        // and put the object's current scale on the front making
        // 0.75_0.75_0.75_lion_GUID
        // this is purely so we can be lazy and use the name to store everything
        // we need about the object rather than create a separate data store.
        var name = string.Format("{0}_{1}_{2}_{3}",
            gameObject.transform.localScale.x,
            gameObject.transform.localScale.y,
            gameObject.transform.localScale.z,
            gameObject.name);

        return (name);
    }
    public GameObject InstantiateAnimalFromNameAndScale(string anchorName)
    {
        var idPieces = anchorName.Split('_');

        // The animal type is the 4th thing encoded in the name
        var animalType = idPieces[3];

        var animalInstance = 
            this.gameObject.GetComponent<AnimalFactory>().MakeObjectForType(animalType);

        // The local scale is encoded in the first 3 parts of the name
        animalInstance.transform.localScale =
            new Vector3(
                float.Parse(idPieces[0]),
                float.Parse(idPieces[1]),
                float.Parse(idPieces[2]));

        return (animalInstance);
    }
    bool reloadedAnimals;
}
