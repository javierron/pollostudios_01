using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {

    public int id;
    public Transform trans;
    public Text item_name;

    public void show_this_item() {
        main.Instace.show_preview(id);
    }
}
