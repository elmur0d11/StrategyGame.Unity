using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SellectController : MonoBehaviour
{
    public GameObject cube;
    public LayerMask layer, layerMask;
    private Camera _camera;
    private GameObject _cubeSellection;
    private RaycastHit hit;
    public List<GameObject> players;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(1) && players.Count > 0)
        {
            Ray Ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(Ray, out RaycastHit agentTarget, 1000f, layer))
                foreach (var el in players)
                    el.GetComponent<NavMeshAgent>().SetDestination(agentTarget.point);
        }
        
        if (Input.GetMouseButtonDown(0)) 
        {
            foreach (var el in players)
                if (el != null)
                    el.transform.GetChild(0).gameObject.SetActive(false);

            players.Clear();

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000f, layer))
                _cubeSellection = Instantiate(cube, new Vector3(hit.point.x, 18.67f, hit.point.z), Quaternion.identity);
        }

        if (_cubeSellection)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitDrag, 1000f, layer))
            {
                float xScale = (hit.point.x - hitDrag.point.x) * -1;
                float zScale = hit.point.z - hitDrag.point.z;

                if(xScale < 0.0f && zScale < 0.0f)
                    _cubeSellection.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
                else if(xScale < 0.0f)
                    _cubeSellection.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
                else if(zScale < 0.0f)
                    _cubeSellection.transform.localRotation = Quaternion.Euler(new Vector3(180, 0, 0));
                else
                    _cubeSellection.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));


                _cubeSellection.transform.localScale = new Vector3(Mathf.Abs(xScale), 1f, Mathf.Abs(zScale));
            }
        }

        if(Input.GetMouseButtonUp(0) && _cubeSellection)
        {
           RaycastHit[] hits = Physics.BoxCastAll(
                _cubeSellection.transform.position,
                _cubeSellection.transform.localScale,
                Vector3.down,
                Quaternion.identity,
                0,
                layerMask);

            foreach(var el in hits)
            {
                if (el.collider.CompareTag("Enemy")) continue;

                players.Add(el.transform.gameObject);
                el.transform.GetChild(0).gameObject.SetActive(true);
            }

            Destroy(_cubeSellection);
        }
    }
}
