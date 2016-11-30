using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct category_panel_settings_t {
    public GameObject prefab;
    public LayoutGroup panel;
}

[System.Serializable]
public struct item_panel_settings_t {
    public GameObject prefab;
    public LayoutGroup panel;
}

public class main : MonoBehaviour {

    public enum State { Categories, Items, }

    public static main Instace { get; private set; }

    public data data_object;
    public category_panel_settings_t category_panel_settings;
    public item_panel_settings_t item_panel_settings;
    public State state;

    public void Awake() {

        Instace = this;

        show_categories();
        state = State.Categories;
    }

    public void set_category(int id) {
        clear_categories();

        item_panel_settings.panel.GetComponent<Image>().enabled = true;


        var category = data_object.get_category_data(id);

        for (int i = 0; i < category.items.Length; i++) {
            var item = Instantiate(item_panel_settings.prefab);
            var item_data = item.GetComponent<Item>();
            item_data.item_name.text = category.items[i].name;
            item_data.trans.SetParent(item_panel_settings.panel.transform, true);
        }

        state = State.Items;
    }

    public void back() {

        switch (state) {
            case State.Categories:
                break;
            case State.Items:
                clear_items();
                show_categories();
                state = State.Categories;
                break;
            default:
                break;
        }

        
    }

    void clear_items() {
        item_panel_settings.panel.GetComponent<Image>().enabled = false;
        while (item_panel_settings.panel.transform.childCount > 0) {
            DestroyImmediate(item_panel_settings.panel.transform.GetChild(0).gameObject);
        }
    }

    void clear_categories() {
        category_panel_settings.panel.GetComponent<Image>().enabled = false;
        while (category_panel_settings.panel.transform.childCount > 0) {
            DestroyImmediate(category_panel_settings.panel.transform.GetChild(0).gameObject);
        }
    }

    void show_categories() {
        category_panel_settings.panel.GetComponent<Image>().enabled = true;

        for (int i = 0; i < data_object.categories.Length; i++) {
            var category = Instantiate(category_panel_settings.prefab);
            var category_data = category.GetComponent<CategoryItem>();
            category_data.category_name.text = data_object.categories[i].name;
            category_data.id = data_object.categories[i].id;
            category_data.trans.SetParent(category_panel_settings.panel.transform, true);
        }
    }



}
