using UnityEngine;

public class FixedCamera : MonoBehaviour
{
    private bool hasPositioned = false;

    void Start()
    {
        if (Application.isPlaying == true)
        {
            Invoke(nameof(AutoDetectAndPosition), 0.1f);
        }
        else
        {
            TryEditorPreview();
        }
    }

    void AutoDetectAndPosition()
    {
         if (hasPositioned == true)
        {
            return;
        }

         RandomGenerator generator = FindAnyObjectByType<RandomGenerator>();
        if (generator != null)
        {
            int totalWidth = generator.width + (2 * generator.offset);

            PositionCamera(totalWidth, generator.height, generator.offset);

            hasPositioned = true;

            Debug.Log("Posicionou");
        }
        else
        {
            Debug.Log("generator null");
        }
    }

    void TryEditorPreview()
    {
        var generate = FindAnyObjectByType<RandomGenerator>();

        if (generate)
        {
            PositionCamera(generate.width + (2 * generate.offset), generate.height, generate.offset);
        }
        else
        {
            PositionCamera(12, 8, 0);
        }
    }

    void PositionCamera(int roomWidth, int roomHeight, int offset)
    {
        Camera camera = Camera.main;

        if (camera == null || !camera.orthographic)
        {
            return;
        }

        float aspectRatio = (float)Screen.width / Screen.height;
        float halfHeight = roomHeight / 2;
        float padding = 0.5f;
        float requiredHeight = halfHeight + padding;
        float halfWidth = roomWidth / 2;
        float requiredWidth = (halfWidth / aspectRatio) + padding;

        camera.orthographicSize = Mathf.Max(requiredHeight, requiredWidth);

        camera.transform.position = new Vector3((roomWidth / 2f) - offset, (roomHeight / 2) - 0.5f, - 10f);
    }
}
