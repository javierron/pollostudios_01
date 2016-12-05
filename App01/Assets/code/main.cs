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

[System.Serializable]
public struct preview_panel_settings_t {
    public CanvasGroup group;
    public Text title;
    public Text description;
}

public class main : MonoBehaviour {

    public enum State { Categories, Items, Preview, }

    public static main Instace { get; private set; }

    public data data_object;
    public category_panel_settings_t category_panel_settings;
    public item_panel_settings_t item_panel_settings;
    public preview_panel_settings_t preview_panel_settings;
    public State state;

    public void Awake() {

        Instace = this;

        show_categories();
        state = State.Categories;
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
            case State.Preview:
                clear_preview();
                show_items(last_category);
                break;
            default:
                break;
        }

        
    }

    public void show_items(int category_id) {
        clear_categories();

        item_panel_settings.panel.GetComponent<Image>().enabled = true;


        var category = data_object.get_category_data(category_id);

        for (int i = 0; i < category.items.Length; i++) {
            var item = Instantiate(item_panel_settings.prefab);
            var item_data = item.GetComponent<Item>();
            item_data.item_name.text = category.items[i].name;
            item_data.id = category.items[i].id;
            item_data.trans.SetParent(item_panel_settings.panel.transform);
        }
        last_category = category_id;
        state = State.Items;
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
            category_data.trans.SetParent(category_panel_settings.panel.transform);
        }
    }

    void clear_preview() {
        preview_panel_settings.group.alpha = 0f;
    }

    public void show_preview(int item_id) {

        clear_items();

        state = State.Preview;

        var item_data = data_object.get_item_data(item_id);

        preview_panel_settings.title.text = item_data.name;
       // preview_panel_settings.description.text = item_data.description;
        preview_panel_settings.group.alpha = 1f;
    }

    int last_category;

}
