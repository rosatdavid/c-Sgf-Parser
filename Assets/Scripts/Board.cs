using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LibFever;
using System.IO;
using DataStructures;
using Grid = LibFever.Grid;

[RequireComponent(typeof(SpriteRenderer))]

public class Board : MonoBehaviour {

   
    Vector2 _size_of_texture = new Vector2(512, 512);
    //public Vector2 _plan_dim = new Vector2(512, 512);
    public Color _couleur_paire = Color.white;
    public Color _couleur_impaire = Color.black;
    [Range(1, 10)]
   
    public float _pos_z = 0;
    //player
    public Transform _moveTest;
    Point _player_start_pos = new Point(0, 0);

    //Tiles Instantiate
    public List<ObjectBiblio> _prefabs_bibliotheque;
    private Dictionary<TypeOfMarker, List<GameObject>> dic_Prefab_object;
    public List<GridObject> _obstacles;
    private Dictionary<Point, int> _dic_obstacles;
    public int _nb_tiles_largeur = 9;
    public int _nb_tiles_hauteur = 9;
    List<LineRenderer> listLinesBoard;
    private LibFever.Plane _plane_texture;
    private LibFever.Plane _plane_collider;

    public Material _gride_material;
    SpriteRenderer _spriteRender;
    Texture2D _textu;
    //MeshCollider _collider;
    public float lineBordureWidth = 0.03f;
    public float lineInsideWidth = 0.01f;
    public float linedistanceFromBoard = 0.01f;
    [HideInInspector]
    public LibFever.Grid _grille;
    public GameObject lineHolder;
    LibFever.Node[,] _graph;

    GameManager gameManager;
    Camera gameCamera;
    void InitGrid()
    {

        _plane_collider = new LibFever.Plane(_spriteRender);

        float largeur = _plane_collider.lenght.x;
        float hauteur = _plane_collider.lenght.y;



        _grille = new LibFever.Grid(_plane_collider,_nb_tiles_largeur,_nb_tiles_hauteur);
        Debug.Log("grille exactSeting min " + _grille.plane.min + " max " + _grille.plane.max + "_nb_tiles_hauteur " +_nb_tiles_hauteur + "nb_tiles_largeur" +_nb_tiles_largeur);

    }

    void DrawBoard()
    {
        listLinesBoard = new List<LineRenderer>();
        GameObject go = GameObject.Instantiate(lineHolder,this.transform);
        go.transform.position = go.transform.position + new Vector3(0f,0f,linedistanceFromBoard);
        LineRenderer line = go.GetComponent<LineRenderer>();
        //go.transform.parent = this.transform;
        line.loop = true;
        line.startWidth = lineBordureWidth;
        line.SetVertexCount(4);
        line.SetPosition(0,Grid.GetPosInWorld(_grille,new Point(0,0)));
        line.SetPosition(1,Grid.GetPosInWorld(_grille,new Point(0,_nb_tiles_hauteur-1)));
        line.SetPosition(2,Grid.GetPosInWorld(_grille,new Point(_nb_tiles_largeur-1,_nb_tiles_hauteur-1)));
        line.SetPosition(3,Grid.GetPosInWorld(_grille,new Point(_nb_tiles_largeur-1,0)));
      
        listLinesBoard.Add(line);
        for(int y = 1;y < _nb_tiles_hauteur;y++)
        {
        LineRenderer l = new LineRenderer();
            go = GameObject.Instantiate(lineHolder,this.transform);
            go.transform.position = go.transform.position + new Vector3(0f,0f,linedistanceFromBoard);
            line = go.GetComponent<LineRenderer>();
            listLinesBoard.Add(line);
            line.startWidth = lineInsideWidth;
            line.SetPosition(0,Grid.GetPosInWorld(_grille,new Point(0,y)));
            line.SetPosition(1,Grid.GetPosInWorld(_grille,new Point(_nb_tiles_largeur-1,y)));
        }
        for(int x = 1;x< _nb_tiles_largeur;x++)
        {
            LineRenderer l = new LineRenderer();
            go = GameObject.Instantiate(lineHolder,this.transform);
            go.transform.position = go.transform.position + new Vector3(0f,0f,linedistanceFromBoard);
            line = go.GetComponent<LineRenderer>();
            line.startWidth = lineInsideWidth;
            listLinesBoard.Add(line);
            line.SetPosition(0,Grid.GetPosInWorld(_grille,new Point(x,0)));
            line.SetPosition(1,Grid.GetPosInWorld(_grille,new Point(x,_nb_tiles_hauteur-1)));

        }
    }

    void DebugGrid(LibFever.Grid g)
    {

        for (int y = 1; y < g.nb_tiles.y; y++)
        {
            for (int x = 1; x < g.nb_tiles.x; x++)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Vector3 posCube = LibFever.Grid.GetPosInWorld(g, new Point(x, y), _pos_z);
                cube.transform.position = posCube;
                cube.transform.localScale = new Vector3(g.tile_size.x, g.tile_size.y,0.001f);
            }
        }
    }
    void MakeBoardTexture()
    {


        _plane_texture = new LibFever.Plane(0, 0, _textu.width, _textu.height);
        for (int y = 1; y <= _textu.height; y++)
        {
            for (int x = 1; x <= _textu.width; x++)
            {
                Point current_case = Grid.getCaseIndex(new LibFever.Grid(_plane_collider,_nb_tiles_largeur,_nb_tiles_hauteur), LibFever.Plane.posPlanInOther(_plane_texture, new Vector2(x, y), _plane_collider));             //Mathf.Floor(Mathf.Abs(textu.width - x) / tile_largeur);

                bool case_paire_x = ((current_case.x + 1) % 2) == 0;
                bool case_paire_y = ((current_case.y + 1) % 2) == 0;

                if (case_paire_x ^ case_paire_y)
                {
                    _textu.SetPixel(x - 1, y - 1, _couleur_paire);
                }
                else
                {
                    _textu.SetPixel(x - 1, y - 1, _couleur_impaire);
                }
            }


        }

        _textu.Apply();
        byte[] bytes = _textu.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/" + "texture_grid.png", bytes);

        _gride_material.mainTexture = _textu;
        _spriteRender.material = _gride_material;
    }
    public void Teleport(Transform tran, Point p, float z)
    {
            Vector3 center = Grid.GetPosInWorld(_grille, new Point(p.x, p.y), z);
            tran.position = center;
        
    }
    // Use this for initialization
    void Awake()
    {
        //Transform parent = this.GetComponentInParent<Transform>();
        //pos_z = parent.position.y;


        gameManager = Object.FindObjectOfType<GameManager>();
        gameCamera = gameManager.gameCamera;
        //_collider = this.GetComponent<MeshCollider>();
        _spriteRender = this.GetComponent<SpriteRenderer>();
        _textu = new Texture2D((int)_size_of_texture.x, (int)_size_of_texture.y, TextureFormat.ARGB32, false);
        InitGrid();
        //MakeBoardTexture();
     



        dic_Prefab_object = new Dictionary<TypeOfMarker, List<GameObject>>();
        foreach (ObjectBiblio ob in _prefabs_bibliotheque)
        {
            dic_Prefab_object[ob.marker_type] = ob.prefabs;
        }

        _dic_obstacles = new Dictionary<Point, int>();
        foreach (GridObject go in _obstacles)
        {
            //Debug.Log(go);
            Point p = new Point(go.x, go.y);
            int value;
            if (!_dic_obstacles.TryGetValue(p, out value))
            {


                _dic_obstacles[p] = (int)go.obstacle_type;
     


            }

        }
        //_graph = LibFever.Node.GenerateGraph(_grille, _dic_obstacles);
        //DebugGrid(_grille);
        DrawBoard();
    }

    // Update is called once per frame





    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            LayerMask mask = InputManager.GetMaskForGrid();
            Debug.Log(Input.mousePosition);
            Point index;
            if (InputManager.ScreenToBoard(gameCamera, _grille, Input.mousePosition, mask, out index))
            {

                Debug.Log("Case : " + index);




            }
        }
    }
}
