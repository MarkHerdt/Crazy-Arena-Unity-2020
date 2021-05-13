using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BarricadeTrigger : MonoBehaviour
{
    [SerializeField] private Barricade barricade;
    public Barricade Barricade { get { return barricade; } }
    private Astronaut astronaut;

    private void Awake()
    {
        barricade = barricade.GetComponent<Barricade>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Gives the Astronaut inside the Trigger a reference for this Object
        if ((astronaut = other.GetComponent<Astronaut>()) != null)
        {
            astronaut.trigger = this;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((astronaut = other.GetComponent<Astronaut>()) != null)
        {
            astronaut.trigger = this;
            barricade.ShowButtonButtonIcon();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Removes the reference for this Object from the Astronaut
        if ((astronaut = other.GetComponent<Astronaut>()) != null)
        {
            astronaut.trigger = null;

            barricade.ShowBarricadeIcon();
        }
    }

    /// <summary>
    /// Returns "true" when the Barricade is activated and "false" when its already active
    /// </summary>
    /// <returns></returns>
    public bool PlaceBarricade(float duration)
    {
        // When the Barricade is currently not active
        if (!barricade.barricadeIsActive)
        {
            barricade.ActivateBarricade(duration);

            return true;
        }
        // When the Barricade is currently active
        else
        {
            //// Lets the Astronaut reset the Barricade
            //if (astronaut.Config.ResetBarricades)
            //{

            //}
            barricade.DeactivateBarricade();

            return false;
        }
    }
}
