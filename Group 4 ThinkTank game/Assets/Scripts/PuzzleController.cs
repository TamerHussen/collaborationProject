using UnityEngine;
using System.Collections.Generic;

public class PuzzleController : MonoBehaviour
{
    private List<Transform> originalPieces = new List<Transform>();
    private List<Vector3> originalPositions = new List<Vector3>();
    private List<Quaternion> originalRotations = new List<Quaternion>();
    private bool isSeparated = false;

    void Start()
    {
        // Store the original state of the model pieces
        foreach (Transform piece in transform)
        {
            originalPieces.Add(piece);
            originalPositions.Add(piece.localPosition);
            originalRotations.Add(piece.localRotation);
        }
    }

    void Update()
    {
        // Separate the pieces when moving
        if (Input.GetMouseButtonDown(0))
        {
            Separate();
        }

        // Reassemble when movement stops
        if (Input.GetMouseButtonUp(0))
        {
            Invoke(nameof(Reassemble), 1.0f); // Delay to allow physics to settle
        }
    }

    void Separate()
    {
        if (isSeparated) return;

        foreach (Transform piece in originalPieces)
        {
            if (piece.GetComponent<Rigidbody>() == null)
            {
                Rigidbody rb = piece.gameObject.AddComponent<Rigidbody>();
                rb.mass = 1;
                rb.AddExplosionForce(100f, transform.position, 5f);
            }
        }

        isSeparated = true;
    }

    void Reassemble()
    {
        if (!isSeparated) return;

        for (int i = 0; i < originalPieces.Count; i++)
        {
            Transform piece = originalPieces[i];
            Rigidbody rb = piece.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Destroy(rb); // Remove Rigidbody to "snap" the piece into place
            }

            // Smooth reassembly using Lerp
            StartCoroutine(SmoothReassemble(piece, originalPositions[i], originalRotations[i]));
        }

        isSeparated = false;
    }

    System.Collections.IEnumerator SmoothReassemble(Transform piece, Vector3 targetPosition, Quaternion targetRotation)
    {
        float duration = 1f;
        float elapsed = 0f;

        Vector3 startPos = piece.localPosition;
        Quaternion startRot = piece.localRotation;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / duration);

            piece.localPosition = Vector3.Lerp(startPos, targetPosition, t);
            piece.localRotation = Quaternion.Slerp(startRot, targetRotation, t);

            yield return null;
        }

        piece.localPosition = targetPosition;
        piece.localRotation = targetRotation;
    }
}
