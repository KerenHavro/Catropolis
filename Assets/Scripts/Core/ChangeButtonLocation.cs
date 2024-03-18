using UnityEngine;

public class RandomLocationButton : MonoBehaviour
{
    public Transform[] targetLocations; // Array to hold the target locations
    private int currentIndex = 0; // Index to keep track of the current target location
    private float currentSize = 1;
    // Function to move the button to a random location and resize it
    public void MoveToRandomLocation()
    {
        int randomIndex = currentIndex; // Initialize with current index to start loop
        while (randomIndex == currentIndex) // Repeat until a different index is generated
        {
            randomIndex = Random.Range(0, targetLocations.Length); // Get a random index
        }

        Transform randomLocation = targetLocations[randomIndex]; // Get the random target location

        transform.position = randomLocation.position; // Move the button to the random location
        currentIndex = randomIndex; // Update the current index

        // Resize the button
        currentSize = currentSize - 0.1f;
        float newSize = currentSize; // Random size between 0.5 and 1.5
        
        transform.localScale = new Vector3(newSize, newSize, 1f); // Apply the new size
    }
}
