using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class UnitMovement : MonoBehaviour
{
    Camera myCam;
    public GameObject TileTestEdge;
    public GameObject TileTestCorner;

    List<Vector3> DestinationPoint = new List<Vector3>();
    NavMeshAgent myAgent;
    public LayerMask ground;

    public string shape;
    int numberOfIterations;
    int destinationIndex = 0;
    Vector3 newTemp;

    GameObject topLeftHorz;
    GameObject bottomLeftHorz;
    GameObject topRightHorz;
    GameObject bottomRightHorz;

    private void Awake()
    {
        shape = "Square";
    }
    void Start()
    {
        myCam = Camera.main;
        myAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            destinationIndex = 0;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                DestinationPoint.Add(hit.point);
                CreateWalkableArea(hit.point, shape);
                //for (int i = 0; i < UnitSelections.Instance.unitSelected.Count; i++)
                //{
                //    myAgent.SetDestination(DestinationPoint[i]);
                //}
                foreach (GameObject unit in UnitSelections.Instance.unitSelected)
                {
                    unit.GetComponent<UnitMovement>().myAgent.SetDestination(DestinationPoint[destinationIndex]);
                    //destinationIndex += (destinationIndex + 1) % UnitSelections.Instance.unitSelected.Count;
                    destinationIndex++;
                }
                if (destinationIndex >= UnitSelections.Instance.unitSelected.Count) DestinationPoint.Clear();
            }
        }
    }

    void CreateWalkableArea(Vector3 startPosition, string shape)
    {
        Vector3 topLeft = new Vector3();
        Vector3 topRight = new Vector3();
        Vector3 bottomLeft = new Vector3();
        Vector3 bottomRight = new Vector3();

        float cornerIncremeter = 1.5f;
        int sideIncremeter = 1;

        Vector3 tempTL = startPosition;
        Vector3 tempBL = startPosition;
        Vector3 tempTR = startPosition;
        Vector3 tempBR = startPosition;

        numberOfIterations = Mathf.CeilToInt(Mathf.Sqrt(UnitSelections.Instance.unitSelected.Count) / 2);


        for (int i = 0; i < numberOfIterations; i++)
        {
            switch (shape)
            {
                case "Square":
                    topLeft.x = (tempTL.x - cornerIncremeter);
                    topLeft.z = (tempTL.z + cornerIncremeter);
                    topLeft.y = transform.localScale.y / 2;

                    DestinationPoint.Add(topLeft);

                    EdgeMaker(sideIncremeter, topLeft, 1.5f, 0);
                    EdgeMaker(sideIncremeter, topLeft, 0, -1.5f);

                    bottomLeft.x = (tempBL.x - cornerIncremeter);
                    bottomLeft.z = (tempBL.z - cornerIncremeter);
                    bottomLeft.y = transform.localScale.y / 2;

                    DestinationPoint.Add(bottomLeft);

                    topRight.x = (tempTR.x + cornerIncremeter);
                    topRight.z = (tempTR.z + cornerIncremeter);
                    topRight.y = transform.localScale.y / 2;

                    DestinationPoint.Add(topRight);

                    bottomRight.x = (tempBR.x + cornerIncremeter);
                    bottomRight.z = (tempBR.z - cornerIncremeter);
                    bottomRight.y = transform.localScale.y / 2;

                    DestinationPoint.Add(bottomRight);

                    EdgeMaker(sideIncremeter, bottomRight, -1.5f, 0);
                    EdgeMaker(sideIncremeter, bottomRight, 0, 1.5f);

                    tempTL = topLeft;
                    tempBL = bottomLeft;
                    tempTR = topRight;
                    tempBR = bottomRight;

                    sideIncremeter += 2;
                    break;

                case "Rectangle":
                    if (i == 0)
                    {
                        topLeft.x = (tempTL.x - cornerIncremeter * 2);
                        topLeft.z = tempTL.z;
                        topLeft.y = transform.localScale.y / 2;

                        DestinationPoint.Add(topLeft);

                        EdgeMaker(1, topLeft, 1.5f, 0);

                        bottomRight.x = (tempBR.x + cornerIncremeter * 2);
                        bottomRight.z = tempBR.z;
                        bottomRight.y = transform.localScale.y / 2;

                        EdgeMaker(1, bottomRight, -1.5f, 0);

                        DestinationPoint.Add(bottomRight);

                        tempTL = topLeft;
                        tempBR = bottomRight;
                    }
                    else
                    {
                        topLeft.x = (tempTL.x - cornerIncremeter);
                        topLeft.z = (tempTL.z + cornerIncremeter);
                        topLeft.y = transform.localScale.y / 2;

                        DestinationPoint.Add(topLeft);

                        EdgeMaker(sideIncremeter + 4, topLeft, 1.5f, 0);
                        EdgeMaker(sideIncremeter, topLeft, 0, -1.5f);

                        if (i == 1)
                        {
                            bottomLeft.x = (tempTL.x - cornerIncremeter);
                            bottomLeft.z = (tempTL.z - cornerIncremeter);
                            bottomLeft.y = transform.localScale.y / 2;

                            DestinationPoint.Add(bottomLeft);

                            topRight.x = (tempBR.x + cornerIncremeter);
                            topRight.z = (tempBR.z + cornerIncremeter);
                            topRight.y = transform.localScale.y / 2;

                            DestinationPoint.Add(topRight);
                        }
                        else
                        {
                            bottomLeft.x = (tempBL.x - cornerIncremeter);
                            bottomLeft.z = (tempBL.z - cornerIncremeter);
                            bottomLeft.y = transform.localScale.y / 2;

                            DestinationPoint.Add(bottomLeft);

                            topRight.x = (tempTR.x + cornerIncremeter);
                            topRight.z = (tempTR.z + cornerIncremeter);
                            topRight.y = transform.localScale.y / 2;

                            DestinationPoint.Add(topRight);
                        }


                        bottomRight.x = (tempBR.x + cornerIncremeter);
                        bottomRight.z = (tempBR.z - cornerIncremeter);
                        bottomRight.y = transform.localScale.y / 2;

                        DestinationPoint.Add(bottomRight);

                        EdgeMaker(sideIncremeter + 4, bottomRight, -1.5f, 0);
                        EdgeMaker(sideIncremeter, bottomRight, 0, 1.5f);

                        tempTL = topLeft;
                        tempBL = bottomLeft;
                        tempTR = topRight;
                        tempBR = bottomRight;

                        sideIncremeter += 2;
                    }
                    break;
            }

            //bottomRightHorz = Instantiate(TileTestCorner);
            //bottomRightHorz.transform.position = bottomRight;
            //bottomLeftHorz = Instantiate(TileTestCorner);
            //bottomLeftHorz.transform.position = bottomLeft;
            //topLeftHorz = Instantiate(TileTestCorner);
            //topLeftHorz.transform.position = topLeft;
            //topRightHorz = Instantiate(TileTestCorner);
            //topRightHorz.transform.position = topRight;
        }
    }

    void EdgeMaker(int sideIncremeter, Vector3 corner, float x, float z)
    {
        for (int tl = 0; tl < sideIncremeter; tl++)
        {
            if (tl == 0)
            {
                Vector3 temp = corner;
                temp.x = corner.x + x;
                temp.z = corner.z + z;
                //GameObject topLeftHorz = Instantiate(TileTestEdge);
                //topLeftHorz.transform.position = temp;
                newTemp = temp;
                DestinationPoint.Add(newTemp);
            }
            else
            {
                Vector3 thirdTemp = newTemp;
                thirdTemp.x = newTemp.x + x;
                thirdTemp.z = newTemp.z + z;
                //GameObject topLeftHorz = Instantiate(TileTestEdge);
                //topLeftHorz.transform.position = thirdTemp;
                newTemp = thirdTemp;
                DestinationPoint.Add(newTemp);
            }
        }
    }
}
