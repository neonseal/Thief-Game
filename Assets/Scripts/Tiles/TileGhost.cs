public class TileGhost : BaseTile {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnMouseDown() {
        GetComponentInParent<Board>().AddTile(this);
    }
}
