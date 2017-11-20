using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetTitels : MonoBehaviour {

    public string titlename = "";
    public int max;
    private string titlename_;


    public Text uiText;
    public RawImage img;
    public Texture[] imgTexture;

    DatabaseAPI databaseAPI;

	void Start () {
        databaseAPI = GetComponent<DatabaseAPI>();

        titlename = titlename.Replace(" ", "_");
        titlename_ = titlename;
        databaseAPI.getAllDataByName(titlename);
    }
	
	void Update () {
        //getTitle();
        getAllData();

        if (titlename_ != titlename){
            titlename = titlename.Replace(" ", "_");
            titlename_ = titlename;
            //databaseAPI.getTitleByName(titlename);
            databaseAPI.getAllDataByName(titlename);
        }

        
    }
    void getAllData() {
        if (databaseAPI.isDone()){
            string[] arr = databaseAPI.getContent();
            string titletext = "";
            int max_;
            if (max >= arr.Length - 1){
                max_ = arr.Length;
            }else{
                max_ = max;
            }
            for (int str = 0; str < max_; str++){
                string[] strsplit = arr[str].Split('|');
                if (str == 0) {
                    // get img
                    foreach (Texture tex in imgTexture) {
                        if (tex.name == strsplit[0] + "_VRK") {
                            img.texture = tex;
                            break;
                        }
                    }
                }
                if (strsplit.Length > 1){
                    titletext += strsplit[0] + " - " + strsplit[2] + " [" + strsplit[1] + "]\n";
                }
            }
            uiText.text = titletext;
        }
        else{
            uiText.text = "loading...";
        }
    }

    void getTitle() {
        if (databaseAPI.isDone()){
            string[] arr = databaseAPI.getContent();
            string titletext = "";
            int max_;
            if (max >= arr.Length - 1){
                max_ = arr.Length;
            }else{
                max_ = max;
            }
            for (int str = 0; str < max_; str++){
                titletext += arr[str] + "\n";
            }
            uiText.text = titletext;
        }
        else{
            uiText.text = "loading...";
        }
    }
}
