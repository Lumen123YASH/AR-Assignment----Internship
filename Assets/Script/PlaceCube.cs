using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceCube : MonoBehaviour
{
    public GameObject targetObj;
    private ARRaycastManager raycastManager; //manages all raycasts in System
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>(); //creates a list of all detections

    // Start is called before the first frame update
    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>(); //calling it from the components
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Debug.Log("touch is being Recorded");

            //Getting Touch input
            Touch touch = Input.GetTouch(0);
            
            //Checking Touch is being Phased i.e. state the user is touching the Screen during it is being recorded or not
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("Touch Phase has Began");

                //Raycasting for Detecting a Collision with plane Detection
                if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;

                    //Generating a prefab where the touch is being recorded
                    GameObject cube = Instantiate(targetObj, hitPose.position, Quaternion.identity);
                    cube.GetComponent<Renderer>().material.color = Random.ColorHSV();

                    //Creating an ARScreenShot after 10 seconds
                    StartCoroutine(CaptureARView(10));
                }
            }
        }
    }

    IEnumerator CaptureARView(int frameDelay)
    {
        //Coundown for an ARScreenShot
        for (int i = 0; i < frameDelay; i++)
            yield return new WaitForEndOfFrame();

        //Getting an Image
        ARCameraManager cameraManager = Camera.main.GetComponent<ARCameraManager>();
        if (cameraManager != null)
        {
            Debug.Log("There is an Camera in Scene");

            //Getting the Image
            cameraManager.TryAcquireLatestCpuImage(out XRCpuImage image);

            //validating the Image
            if (image.valid)
            {
                Debug.Log("The Image Is Valid");

                //Converting parameters of the Raw Image Data for Apply texture formating for texture size and Dimensions
                var conversionParams = new XRCpuImage.ConversionParams
                {
                    inputRect = new RectInt(0, 0, image.width, image.height),
                    outputDimensions = new Vector2Int(image.width, image.height),
                    outputFormat = TextureFormat.RGBA32,
                    transformation = XRCpuImage.Transformation.MirrorY
                };

                //Converting Texture from parameters
                Texture2D texture = new Texture2D(image.width, image.height, TextureFormat.RGBA32, false);
                image.Convert(conversionParams, texture.GetRawTextureData<byte>());
                texture.Apply();

                // Save the texture
                byte[] bytes = texture.EncodeToPNG();
                string path = Application.persistentDataPath + "/ARCapture.png";
                System.IO.File.WriteAllBytes(path, bytes);
                Debug.Log("Saved Image at: " + path);

                image.Dispose();
            }
        }
    }
}
