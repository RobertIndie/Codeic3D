using UnityEngine;
using Codeic;
using System.Threading;
using CDK;
using System;
using System.Collections.Generic;

public class C3D : MonoBehaviour {
    // Use this for initialization
    void Start () {
        Runner runner = new Runner("localhost", 8001);
        runner.AddLib(new Codeic3D());
        Thread t = new Thread(new ThreadStart(runner.Start));
        t.Start();
	}
    public static List<GameObject> objectPool = new List<GameObject>();
    public GameObject testObject;
    public static List<Vector3> messagePool = new List<Vector3>();
    // Update is called once per frame
    void Update () {
        if (messagePool.Count != 0)
        {
            foreach(Vector3 v in messagePool)
            {
                GameObject go = Instantiate(testObject);
                go.transform.position = v;
                objectPool.Add(go);
            }
            messagePool.Clear();
        }
	}
    public class Codeic3D : ILib
    {
        public string name
        {
            get
            {
                return "Essential";
            }
        }

        public string author
        {
            get
            {
                return "Aaron Robert";
            }
        }

        public string version
        {
            get
            {
                return "v1.0";
            }
        }

        public void Awake(IRunner runner)
        {

        }

        public void Receive(string command, string[] parameters)
        {
            switch (command)
            {
                case "CreateObject": Debug.Log(string.Format("在X={0}，Y={1}，Z={2}的位置创建了个物体", parameters[0], parameters[1], parameters[2]));
                    C3D.messagePool.Add(new Vector3(
                            float.Parse(parameters[0]),
                            float.Parse(parameters[1]),
                            float.Parse(parameters[2])
                        ));
                    break;
            }
        }

        public void Reset()
        {

        }

        public void Start()
        {

        }
    }
}
