using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class CharacterSelect : MonoBehaviour
{
    private int index;
    [SerializeField] private GameObject[] characters; 
    [SerializeField] TextMeshProUGUI characterName; 
    [SerializeField] GameObject[] characterPrefabs; 
    public static GameObject selectedCharacter; 
    public Button backButton; 
    private const string SelectedCharacterIndexKey = "SelectedCharacterIndex";

    void Start()
    {
        
        index = PlayerPrefs.GetInt(SelectedCharacterIndexKey, 0);

        if (index >= characters.Length || index < 0)
        {
            index = 0;
        }
        SelectCharacter(); 

        if (backButton != null)
        {
            backButton.onClick.RemoveAllListeners(); 
            backButton.onClick.AddListener(OnBackButtonClick); 
        }
        else
        {
            Debug.LogError("Back Button is not assigned in CharacterSelect!");
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnNextButtonClick();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnPreviousButtonClick(); 
        }
        else if (Input.GetKeyDown(KeyCode.Return)) 
        {
            OnPlayButtonClick(); 
        }
    }
    public void OnPlayButtonClick()
    {
        
        PlayerPrefs.SetInt(SelectedCharacterIndexKey, index);
        PlayerPrefs.Save(); 

        
        SceneManager.LoadScene("GamePlay");
    }
    public void OnPreviousButtonClick()
    {
        index--; 

        if (index < 0)
        {
            index = characters.Length - 1;
        }
        SelectCharacter();
        Debug.Log("Selected character index: " + index);
    }
    public void OnNextButtonClick()
    {
        index++; 
    
        if (index >= characters.Length) 
        {
            index = 0;
        }
        SelectCharacter();
        Debug.Log("Selected character index: " + index);
    }
    private void SelectCharacter()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            SpriteRenderer characterSprite = characters[i].GetComponent<SpriteRenderer>();

            if (i == index)
            {
                characterSprite.color = Color.white;
                selectedCharacter = characterPrefabs[i]; 
                characterName.text = characterPrefabs[i].name;  
            }
            else
            {              
                characterSprite.color = Color.grey; 
            }
        }
    }
    public void OnBackButtonClick()
    {
        SceneManager.LoadScene("MainMenu"); 
    }
}