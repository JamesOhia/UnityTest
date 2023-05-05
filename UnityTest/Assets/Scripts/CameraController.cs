using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform anchor;

    [Tooltip("how far the camera should be away from the anchor on the z axis from start")]
    public int zDistanceFromAnchor = 10;//how far the camera should be away from the anchor on the z axis from start

    float angle = 0;
    Vector3 mousePos0 = Vector3.zero;
    Vector3 mousePos1 = Vector3.zero;

    void Start()
    {
        setAnchor(anchor);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos0 = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))//rotate the camera around the anchor point as the user is dragging his/her finger accross the screen
        {
            mousePos1 = Input.mousePosition;

            Vector3 mDelta = mousePos1 - mousePos0;
            angle = Vector3.Dot(mDelta, Vector3.right);//base rotation on the change in position

            transform.RotateAround(anchor.position, Vector3.up, -angle * Time.deltaTime * 20);

            mousePos0 = mousePos1;
        }
    }

    public void setAnchor(Transform newAnchor)//call this function to change the camer's anchor to another transform. This will also move the camera towards the desired anchor
    {
        anchor = newAnchor;

        Vector3 newCamPosition = anchor.position + Vector3.back * zDistanceFromAnchor;
        newCamPosition.y = transform.position.y;//maintain the existing y position of the camera
        transform.position = newCamPosition;

        //make the camera look towards the anchor point
        Vector3 dirToAnchor = anchor.position - transform.position;
        Quaternion lookDir = Quaternion.LookRotation(dirToAnchor);
        transform.rotation = lookDir;
    }
}
