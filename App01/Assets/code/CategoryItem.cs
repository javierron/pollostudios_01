using UnityEngine;
using UnityEngine.UI;

public class CategoryItem : MonoBehaviour {

    public int id;
    public Transform trans;
    public Text category_name;

    public void show_this_category() {
        main.Instace.show_items(id);
    }
}
