﻿using UnityEngine;

public class data : MonoBehaviour {

    [System.Serializable]
    public struct items_t {
        public int id;
        public string name;
        public string description;
    }

    [System.Serializable]
    public struct categories_t {
        public int id;
        public string name;
        public items_t[] items;
    }

    public categories_t[] categories;

    public categories_t get_category_data(int id) {

        for (int i = 0; i < categories.Length; i++) {
            if (categories[i].id == id) {
                return categories[i];
            }
        }

        throw new System.ArgumentException("Invalid Category ID: ", id.ToString());

    }

    public items_t get_item_data(int id) {

        for (int i = 0; i < categories.Length; i++) {
            for (int j = 0; j < categories[i].items.Length; j++) {
                if (categories[i].items[j].id == id) {
                    return categories[i].items[j];
                }
            }
            
        }

        throw new System.ArgumentException("Invalid Category ID: ", id.ToString());

    }

}
