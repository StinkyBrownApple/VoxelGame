using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceCube : MonoBehaviour
{

    [SerializeField]
    GameObject GuideCube;
    [SerializeField]
    Camera playerCam;
    [SerializeField]
    GameObject Cube;

    Color[] colourArray = { Color.white, Color.red, Color.yellow, Color.blue, Color.green, Color.black };
    int selectedColour = 0;

    float xOffset = 0.5f;
    float yOffset = 0.5f;
    float zOffset = 0.5f;

    bool canPlace = true;


    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            selectedColour++;
            if(selectedColour == colourArray.Length)
            {
                selectedColour = 0;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            selectedColour--;
            if (selectedColour < 0)
            {
                selectedColour = colourArray.Length - 1;
            }
        }

        RaycastHit hitInfo;
        Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        if (Physics.Raycast(ray, out hitInfo, 5))
        {
            GuideCube.SetActive(true);
            if (hitInfo.collider.GetType() == typeof(BoxCollider))
            {
                Vector3 guideCubePos = Vector3.zero;
                Vector3 colliderPos = hitInfo.collider.transform.position;
                Vector3 rayVector = hitInfo.point - colliderPos;
                Vector2 rayVector2D = new Vector2(rayVector.x, rayVector.z);
                float xDot = Vector3.Dot(rayVector2D.normalized, Vector2.right);
                float zDot = Vector3.Dot(rayVector2D.normalized, Vector2.up);
                float yDot = Vector3.Dot(rayVector, Vector3.up);

                if (xDot > 0.7)
                {
                    guideCubePos = colliderPos + new Vector3(1, 0, 0);
                }

                if (xDot < -0.7)
                {
                    guideCubePos = colliderPos + new Vector3(-1, 0, 0);
                }

                if (zDot > 0.7)
                {
                    guideCubePos = colliderPos + new Vector3(0, 0, 1);
                }

                if (zDot < -0.7)
                {
                    guideCubePos = colliderPos + new Vector3(0, 0, -1);
                }

                if (yDot == 0.5)
                {
                    guideCubePos = colliderPos + new Vector3(0, 1, 0);
                }

                if (yDot == -0.5)
                {
                    guideCubePos = colliderPos + new Vector3(0, -1, 0);
                }

                GuideCube.transform.position = guideCubePos;

                if(Input.GetMouseButtonDown(0))
                {
                    Destroy(hitInfo.transform.gameObject);
                }

            }

            else
            {

                if (hitInfo.point.x < 0)
                    xOffset = -0.5f;
                else
                    xOffset = 0.5f;

                if (hitInfo.point.z < 0)
                    zOffset = -0.5f;
                else
                    zOffset = 0.5f;

                GuideCube.transform.position = new Vector3((int)hitInfo.point.x + xOffset, (int)hitInfo.point.y + yOffset, (int)hitInfo.point.z + zOffset);
            }

            if(Input.GetMouseButtonDown(1) && canPlace)
            {
                GameObject spawnedCube = Instantiate(Cube, GuideCube.transform.position, Quaternion.identity);
                spawnedCube.GetComponent<Renderer>().material.color = colourArray[selectedColour];
            }
        }

        else
        {
            GuideCube.SetActive(false);
        }
    }

    private void OnGUI()
    {
        Texture2D boxTexture = new Texture2D(1, 1);
        boxTexture.SetPixel(0, 0, colourArray[selectedColour]);
        boxTexture.wrapMode = TextureWrapMode.Repeat;
        boxTexture.Apply();

        GUI.DrawTexture(new Rect(0, 0, 50, 50), boxTexture);
    }

    public void PlayerCollision(bool isColliding)
    {
        if (isColliding)
        {
            canPlace = false;
        }
        else
            canPlace = true;
    }
}
