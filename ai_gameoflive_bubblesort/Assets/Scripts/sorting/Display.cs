using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display : MonoBehaviour {

    public int rows;
    public GameObject row;

    private GameObject[] graph;
    private ArrayList numList = new ArrayList();


	void Start () {
        graph = new GameObject[rows];
        for (int num = 0; num < rows; num++){
            numList.Add(num + 1);
        }

        numList = shuffelList(numList);

        foreach (int i in numList) {
            print(i);
        }
        drawGraph();
	}

    float time = Time.time;
	void Update () {
        if (Time.time >= time) {
            time = Time.time;
            drawGraph();
            bubbleSort();
        }
	}

    // sorting START
    void bubbleSort() {
        int lenght = numList.Count;
        bool swapped = true;
        while (swapped) {
            swapped = false;
            for (int i = 0; i < lenght; i++){
                if (i-1 != -1 && (int)numList[i-1] > (int)numList[i]) {
                    int a = (int)numList[i - 1];
                    int b = (int)numList[i];
                    numList[i] = a;
                    numList[i - 1] = b;
                    swapped = true;
                    graph[i].GetComponent<Renderer>().material.color = Color.green;
                    return;
                }
            }
        }
    }

    int current = 1;
    void cocktailShakerSort(){
        bool swapped = true;
        while (swapped) {
            for (int i = 0; i < numList.Count; i++){
                if (i - 1 != -1 && (int)numList[i] == current) {
                    int a = (int)numList[i - 1];
                    int b = (int)numList[i];
                    numList[i] = a;
                    numList[i - 1] = b;
                    swapped = true;
                    if (current == (int)numList[i]){
                        current++;
                    }
                    graph[i].GetComponent<Renderer>().material.color = Color.green;
                    return;
                }
                print(current + "|" + (int)numList[i]);
            }
            return;
        }
    }
    // sorting END

    void drawGraph() {
        for (int i = 0; i < graph.Length; i++){
            Destroy(graph[i]);
        }
        graph = new GameObject[rows];
        
        for (int r = 0; r < numList.Count; r++){
            int num = (int)numList[r];
            Vector3 pos = new Vector3(0.2f * r, (float)num * 0.1f / 2f, 0f);
            GameObject obj = Instantiate(row, pos, Quaternion.identity) as GameObject;
            obj.transform.localScale = new Vector3(0.15f, (float)num * 0.1f, 0.15f);
            graph[r] = obj;
        }
    }

    ArrayList shuffelList(ArrayList list) {
        for (int t = 0; t < list.Count; t++){
            int tmp = (int)list[t];
            int r = Random.Range(t, list.Count);
            list[t] = list[r];
            list[r] = tmp;
        }
        return list;
    }
}
