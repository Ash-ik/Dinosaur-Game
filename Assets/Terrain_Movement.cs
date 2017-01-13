using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.ObjectModel;

public class Terrain_Movement : MonoBehaviour {

    public Sprite[] Tile_Sprites;
    public GameObject Tile_Prefab;
    public Transform Parent_Transform;

    private GameObject[] Tile_Game_Objects;

    public float Between_Distance;
    public float Movement_Speed;
    public float Tree_Movement_Speed;
    public float Tile_Width;
    public float Landscape_Level;

    public bool Move_Terrain = false;
    public bool Create_Objects = true;

    public Sprite[] Tree_Sprites;
    public float Tree_Generation_Tick;
    private float Time_Passed = 0;
    private Collection<GameObject> Trees;
    public GameObject Tree_Prefab;
    public Transform Tree_Parent;
    public float Tree_Level;

    public GameObject Bird_Prefab;
    public float Bird_Generation_Tick;
    private float Bird_Generation_Time_Passed;
    private Collection<GameObject> Birds;
    public Transform Bird_Parent;
    public float Bird_Level;
    public float Birds_Movement_Speed;
    private bool Add_Tree = true;

    public GameObject Cloud_Prefab;
    public float Cloud_Generation_Tick;
    private float Cloud_Generation_Time_Passed;
    private Collection<GameObject> Clouds;
    public Transform Cloud_Parent;
    public float Cloud_Level;
    public float Clouds_Movement_Speed;
    public Sprite[] Cloud_Sprites;

    public Game_System Game_Sys;

    // Use this for initialization
    void Start () {
	    if (Tile_Prefab != null)
        {
            Tile_Game_Objects = new GameObject[11];

            int I;
            for (I = 0; I < 11; I++)
            {
                Tile_Game_Objects[I] = (GameObject)Instantiate(Tile_Prefab, Parent_Transform);
                Vector3 Pos = Tile_Game_Objects[I].transform.localPosition;
                Pos.x = Pos.x + I * Tile_Width;
                Pos.y = Landscape_Level;
                Tile_Game_Objects[I].transform.localPosition = Pos;
                Tile_Game_Objects[I].transform.localScale = new Vector3(1, 1, 1);

				float z = Random.value;
				if (z >= 0.0 && z <= .5)
					Tile_Game_Objects [I].GetComponent<Image> ().sprite = Tile_Sprites [4];
				else if (z > .5 && z <= .95)
					Tile_Game_Objects [I].GetComponent<Image> ().sprite = Tile_Sprites [(int)(Random.Range (1, 3))];
				else
                Tile_Game_Objects[I].GetComponent<Image>().sprite = Tile_Sprites[0];
                Tile_Game_Objects[I].SetActive(false);
            }
        }

        Trees = new Collection<GameObject>();
        Birds = new Collection<GameObject>();
        Clouds = new Collection<GameObject>();
    }

    public void Visible_All()
    {
        if (Tile_Game_Objects != null)
        {
            int I;
            for (I = 0; I < Tile_Game_Objects.Length; I++)
                Tile_Game_Objects[I].SetActive(true);
        }
    }
	
    void FixedUpdate()
    {
        if (Game_Sys != null)
        {
            if (Game_Sys.Current_State == 2)
            {
                Game_Sys.Added_Speed = Game_Sys.Added_Speed + 0.5f;
                if (Game_Sys.Added_Speed > 300)
                {
                    Game_Sys.Added_Speed = 300;
                }
            }
        }
    }

	// Update is called once per frame
	void Update () {

	    if (Move_Terrain == true)
        {
            int I;
            for (I = 0; I < 11; I++)
            {
                Vector3 Pos = Tile_Game_Objects[I].transform.localPosition;

                if (Pos.x < -400 - 96)
                {
                    int J;
                    float Max_X = -1000;
                    int ID = I;
                    for (J = 0; J < 11; J++)
                    {
                        Vector3 P = Tile_Game_Objects[J].transform.localPosition;
                        if (P.x > Max_X)
                        {
                            Max_X = P.x;
                            ID = J;
                        }
                    }

                    Vector3 P_X = Tile_Game_Objects[ID].transform.localPosition;
                    Pos.x = P_X.x + 96;
                }

                Pos.x = Pos.x - (Movement_Speed + Game_Sys.Added_Speed) * Time.deltaTime;
                Tile_Game_Objects[I].transform.localPosition = Pos;

             
            }
        }

        if (Create_Objects == false)
            return;

        Time_Passed = Time_Passed + Time.deltaTime;
        Bird_Generation_Time_Passed = Bird_Generation_Time_Passed + Time.deltaTime;
        Cloud_Generation_Time_Passed = Cloud_Generation_Time_Passed + Time.deltaTime;

        if (Add_Tree == true)
        {
            if (Time_Passed > Tree_Generation_Tick)
            {
                Tree_Generation_Tick = Random.Range(2, 4);
                Time_Passed = 0;

                int Rand_Trees = (int)(Random.Range(0, 3));
                if (Rand_Trees == 0)
                {
                    GameObject T1 = (GameObject)Instantiate(Tree_Prefab, Tree_Parent);
                    T1.GetComponent<Image>().sprite = Tree_Sprites[(int)(Random.Range(0, Tree_Sprites.Length - 1))];
					T1.transform.localScale = new Vector3(1, 1, 1);
                    Vector3 Tree_Pos = T1.transform.localPosition;
                    Tree_Pos.x = 400;
					Tree_Pos.y = Tree_Level;
                    T1.transform.localPosition = Tree_Pos;
                    Trees.Add(T1);
                }
                else if (Rand_Trees == 1)
                {
                    GameObject T1 = (GameObject)Instantiate(Tree_Prefab, Tree_Parent);
                    T1.GetComponent<Image>().sprite = Tree_Sprites[(int)(Random.Range(0, Tree_Sprites.Length - 1))];
					T1.transform.localScale = new Vector3(1, 1, 1);
                    Vector3 Tree_Pos = T1.transform.localPosition;
                    Tree_Pos.x = 400;
					Tree_Pos.y = Tree_Level;
                    T1.transform.localPosition = Tree_Pos;
                    Trees.Add(T1);

                    GameObject T2 = (GameObject)Instantiate(Tree_Prefab, Tree_Parent);
                    T2.GetComponent<Image>().sprite = Tree_Sprites[(int)(Random.Range(0, Tree_Sprites.Length - 1))];
					T2.transform.localScale = new Vector3(1, 1, 1);
                    Tree_Pos = T2.transform.localPosition;
                    Tree_Pos.x = 400 + 30;
					Tree_Pos.y = Tree_Level;
                    T2.transform.localPosition = Tree_Pos;
                    Trees.Add(T2);
                }
                else if (Rand_Trees == 2)
                {
                    GameObject T1 = (GameObject)Instantiate(Tree_Prefab, Tree_Parent);
					T1.GetComponent<Image>().sprite = Tree_Sprites[(int)(Random.Range(0, Tree_Sprites.Length - 1))];
					T1.transform.localScale = new Vector3(1, 1, 1);
                    Vector3 Tree_Pos = T1.transform.localPosition;
                    Tree_Pos.x = 410;
					Tree_Pos.y = Tree_Level;
                    T1.transform.localPosition = Tree_Pos;
                    Trees.Add(T1);

                    GameObject T2 = (GameObject)Instantiate(Tree_Prefab, Tree_Parent);
                    T2.GetComponent<Image>().sprite = Tree_Sprites[(int)(Random.Range(0, Tree_Sprites.Length - 1))];
                    T2.transform.localScale = new Vector3(1, 1, 1);
                    Tree_Pos = T2.transform.localPosition;
                    Tree_Pos.x = 410 + 30;
					Tree_Pos.y = Tree_Level;
                    T2.transform.localPosition = Tree_Pos;
                    Trees.Add(T2);

                    GameObject T3 = (GameObject)Instantiate(Tree_Prefab, Tree_Parent);
                    T3.GetComponent<Image>().sprite = Tree_Sprites[(int)(Random.Range(0, Tree_Sprites.Length - 1))];
					T3.transform.localScale = new Vector3(1, 1, 1);
                    Tree_Pos = T3.transform.localPosition;
                    Tree_Pos.x = 410 - 30;
					Tree_Pos.y = Tree_Level;
                    T3.transform.localPosition = Tree_Pos;
                    Trees.Add(T3);
                }

                Add_Tree = false;
                Bird_Generation_Time_Passed = 0;
            }
        }
        else if (Add_Tree == false)
        {
            if (Bird_Generation_Time_Passed > Bird_Generation_Tick)
            {
                Bird_Generation_Tick = Random.Range(2, 4);
                Bird_Generation_Time_Passed = 0;

                int Rand_Birds = (int)(Random.Range(0, 2));
                if (Rand_Birds == 0)
                {
                    GameObject T1 = (GameObject)Instantiate(Bird_Prefab, Bird_Parent);
                    T1.transform.localScale = new Vector3(1, 1, 1);
                    Vector3 Bird_Pos = T1.transform.localPosition;
                    Bird_Pos.x = 400;
					switch ((int)(Random.Range (0, 2))) 
					{
					case 0:
						Bird_Pos.y = Bird_Level;
						break;
					case 1:
						Bird_Pos.y = Bird_Level - 60;
						break;
					}
                    T1.transform.localPosition = Bird_Pos;
                    Birds.Add(T1);
                }
                else if (Rand_Birds == 1)
                {
                    GameObject T1 = (GameObject)Instantiate(Bird_Prefab, Bird_Parent);
                    T1.transform.localScale = new Vector3(1, 1, 1);
                    Vector3 Bird_Pos = T1.transform.localPosition;
                    Bird_Pos.x = 400;
                    Bird_Pos.y = Bird_Level;
                    T1.transform.localPosition = Bird_Pos;
                    Birds.Add(T1);

                    GameObject T2 = (GameObject)Instantiate(Bird_Prefab, Bird_Parent);
                    T2.transform.localScale = new Vector3(1, 1, 1);
                    Bird_Pos = T2.transform.localPosition;
					Bird_Pos.x = 400 + (int)(Random.Range(150, 250));
                    Bird_Pos.y = Bird_Level - 60;
                    T2.transform.localPosition = Bird_Pos;
                    Birds.Add(T2);
                }

                Add_Tree = true;
                Time_Passed = 0;
            }
            
        }

        if (Cloud_Generation_Time_Passed > Cloud_Generation_Tick)
        {
            Cloud_Generation_Tick = Random.Range(2, 4);
            Cloud_Generation_Time_Passed = 0;

            int Rand_Clouds = (int)(Random.Range(0, 2));
            if (Rand_Clouds == 0)
            {
                GameObject T1 = (GameObject)Instantiate(Cloud_Prefab, Cloud_Parent);
                T1.GetComponent<Image>().sprite = Cloud_Sprites[(int)(Random.Range(0, Cloud_Sprites.Length - 1))];
                T1.transform.localScale = new Vector3(1, 1, 1);
                Vector3 Cloud_Pos = T1.transform.localPosition;
                Cloud_Pos.x = 400;
                Cloud_Pos.y = Cloud_Level;
                T1.transform.localPosition = Cloud_Pos;
                Clouds.Add(T1);
            }
            else if (Rand_Clouds == 1)
            {
                GameObject T1 = (GameObject)Instantiate(Cloud_Prefab, Cloud_Parent);
                T1.GetComponent<Image>().sprite = Cloud_Sprites[(int)(Random.Range(0, Cloud_Sprites.Length - 1))];
                T1.transform.localScale = new Vector3(1, 1, 1);
                Vector3 Cloud_Pos = T1.transform.localPosition;
                Cloud_Pos.x = 400;
                Cloud_Pos.y = Cloud_Level;
                T1.transform.localPosition = Cloud_Pos;
                Clouds.Add(T1);

                GameObject T2 = (GameObject)Instantiate(Cloud_Prefab, Cloud_Parent);
                T2.GetComponent<Image>().sprite = Cloud_Sprites[(int)(Random.Range(0, Cloud_Sprites.Length - 1))];
                T2.transform.localScale = new Vector3(1, 1, 1);
                Cloud_Pos = T2.transform.localPosition;
                Cloud_Pos.x = 400 + 40;
                Cloud_Pos.y = Cloud_Level - 40;
                T2.transform.localPosition = Cloud_Pos;
                Clouds.Add(T2);
            }
            
        }

        int Ind;


        for (Ind = 0; Ind < Trees.Count; Ind++)
        {

            Vector3 P = Trees[Ind].transform.localPosition;
            P.x = P.x - ((Tree_Movement_Speed + Game_Sys.Added_Speed) * Time.deltaTime);
            Trees[Ind].transform.localPosition = P;

        }

        for (Ind = 0; Ind < Birds.Count; Ind++)
        {

            Vector3 P = Birds[Ind].transform.localPosition;
            P.x = P.x - ((Birds_Movement_Speed + Game_Sys.Added_Speed) * Time.deltaTime);
            Birds[Ind].transform.localPosition = P;

        }

        for (Ind = 0; Ind < Clouds.Count; Ind++)
        {

            Vector3 P = Clouds[Ind].transform.localPosition;
            P.x = P.x - ((Clouds_Movement_Speed + Game_Sys.Added_Speed) * Time.deltaTime);
            Clouds[Ind].transform.localPosition = P;

        }


        for (Ind = 0; Ind < Trees.Count; Ind++)
        {
            Vector3 P = Trees[Ind].transform.localPosition;
            if (P.x < -496)
            {
                Destroy(Trees[Ind]);
                if (Ind != Trees.Count - 1)
                {
                    int Jnd;
                    for (Jnd = Ind + 1; Jnd < Trees.Count; Jnd++)
                    {
                        Trees[Jnd - 1] = Trees[Jnd];
                    }
                }


                Trees.RemoveAt(Trees.Count - 1);
            }
        }


        for (Ind = 0; Ind < Birds.Count; Ind++)
        {
            Vector3 P = Birds[Ind].transform.localPosition;
            if (P.x < -496)
            {
                Destroy(Birds[Ind]);
                if (Ind != Birds.Count - 1)
                {
                    int Jnd;
                    for (Jnd = Ind + 1; Jnd < Birds.Count; Jnd++)
                    {
                        Birds[Jnd - 1] = Birds[Jnd];
                    }
                }


                Birds.RemoveAt(Birds.Count - 1);
            }
        }

        for (Ind = 0; Ind < Clouds.Count; Ind++)
        {
            Vector3 P = Clouds[Ind].transform.localPosition;
            if (P.x < -496)
            {
                Destroy(Clouds[Ind]);
                if (Ind != Clouds.Count - 1)
                {
                    int Jnd;
                    for (Jnd = Ind + 1; Jnd < Clouds.Count; Jnd++)
                    {
                        Clouds[Jnd - 1] = Clouds[Jnd];
                    }
                }


                Clouds.RemoveAt(Clouds.Count - 1);
            }
        }
    }

    public void Clear_All()
    {
        int Ind;
        for (Ind = Clouds.Count - 1; Ind >= 0; Ind--)
        {
            Destroy(Clouds[Ind]);
        }

        Clouds.Clear();

        for (Ind = Trees.Count - 1; Ind >= 0; Ind--)
        {
            Destroy(Trees[Ind]);
        }

        Trees.Clear();


        for (Ind = Birds.Count - 1; Ind >= 0; Ind--)
        {
            Destroy(Birds[Ind]);
        }

        Birds.Clear();

    }

   
}


