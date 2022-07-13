using UnityEngine;
using SplineMesh;
using System.Collections.Generic;
using System.Collections;

public class TubeGenerator : MonoBehaviour
{
    [SerializeField] private Spline spline;
    [SerializeField] private float scaler = 0.02f;//Needed for pipe crack control.
    [SerializeField] private Transform gravityModifier;
    [SerializeField] private Transform generatedMeshParent;

    public void generateTube(string svgName)
    {
        string pathStr = SVGConverter.getPathString(svgName);
        generateBezierByUsingPath(SVGConverter.getCoordinates(pathStr));
    }


    private void generateBezierByUsingPath(string[] path)
    {
        for (int i = 0; i < path.Length; i++)
        {
            string node = path[i];
            Vector3 lastNodePos = spline.nodes[spline.nodes.Count - 1].Position;
            if (!string.IsNullOrEmpty(node))
            {
                float[] coordinates = getPoints(node);
                switch (node[0])
                {
                    case 'M':
                        //Zero point.
                        break;
                    case 'L':
                        spline.AddNode(new SplineNode(new Vector3(lastNodePos.x + coordinates[0] *scaler, lastNodePos.y + coordinates[1] * scaler, 0f), new Vector3(0f, 1f, 0f)));
                        spline.nodes[spline.nodes.Count - 1].Up = Vector3.one;
                        break;
                    case 'V':
                        if (coordinates[0] > 0)
                        {
                            spline.AddNode(new SplineNode(new Vector3(lastNodePos.x, lastNodePos.y + coordinates[0] * scaler * 0.3f, 0f), new Vector3(0f, 1f, 0f)));
                            spline.nodes[spline.nodes.Count - 1].Up = Vector3.one;
                        }
                        break;
                    case 'C'://Had problems when converting curve's to SplineMesh's curve calculation.
                        break;
                    case 'H':
                        if (coordinates[0] > 0)
                        {
                            spline.AddNode(new SplineNode(new Vector3(lastNodePos.x + coordinates[0] * scaler, lastNodePos.y, 0f), new Vector3(0f, 1f, 0f)));
                            spline.nodes[spline.nodes.Count - 1].Up = Vector3.one;
                        }
                        break;
                }
            }
        }
        StartCoroutine(moveGravityModifier());
    }

    IEnumerator moveGravityModifier()//Should be work after spline has generated successfully.
    {
        yield return new WaitForEndOfFrame();

        Transform lastMeshTransform = generatedMeshParent.GetChild(generatedMeshParent.childCount - 1);
        Debug.Log(lastMeshTransform.name);
        Mesh mesh = lastMeshTransform.GetComponent<MeshFilter>().mesh;
        Vector3 lastVertexPos = mesh.vertices[mesh.vertexCount - 1];
        gravityModifier.position = lastMeshTransform.TransformPoint(lastVertexPos);

    }

    private float[] getPoints(string pointsString)
    {
        List<float> result = new List<float>();
        pointsString = pointsString.Substring(1);
        string[] pointArr = pointsString.Split(' ');
        foreach (var point in pointArr)
        {
            if(float.TryParse(point, out float value))
            {
                result.Add(value);
            }
        }
        return result.ToArray();
    }
}
