using UnityEngine;

public class MyFirstScript : MonoBehaviour
{

    //this is still in the class..
    public GameObject ourGameObject; //<-- since this is public it can be assigned and read from any script.. 

    private GameObject thisGameObjectIsPrivate; // it cannot..

    void Start()
    {
        //we could call it on start
        MyFunction(); // <-- this is how we could call that method..
        // now we could use that gameobject for w/e we wish..
        ourGameObject.SetActive(false);
    }

    void Update()
    {
            // this is running every frame..
            // we could do things like listen for input..
    }

    public void MyFunction()
    {
        // this is my custom function
        // it can do whatever it wants..
        Debug.Log("HelloWorld");
        // perhaps debug ^
    }
}
