using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.InputModule.Utilities.Interactions;
using UnityEngine;

public class InteractionHandler : MonoBehaviour, IFocusable, IInputClickHandler
{
    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (!this.tapped)
        {
            this.tapped = true;

            // switch on gravity and let it fall.
            var rigidBody = this.gameObject.AddComponent<Rigidbody>();
            rigidBody.freezeRotation = true;
            this.waitingToLand = true;
        }
    }
    void OnCollisionStay(Collision collision)
    {
        if (this.waitingToLand && (collision.relativeVelocity.magnitude < 0.01f))
        {
            this.waitingToLand = false;
            Destroy(this.gameObject.GetComponent<Rigidbody>());

            this.gameObject.GetComponent<TwoHandManipulatable>().enabled = true;
        }
    }
    public void OnFocusEnter()
    {
        if (!this.tapped)
        {
            this.gameObject.transform.localScale *= 1.2f;
        }
    }
    public void OnFocusExit()
    {
        if (!this.tapped)
        {
            this.gameObject.transform.localScale = Vector3.one;
        }
    }
    bool waitingToLand;
    bool tapped = false;
}
