using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

public class HighScoreSystem : MonoBehaviour{
    public string fileName = "HighScore";
    public int nameCharacterLength = 3;
    public int maximumNames = 10;
    public List<NameToScore> highScoreList = new List<NameToScore>();
    [System.Serializable]
    public class NameToScore : IComparable<NameToScore>
    {
        private HighScoreSystem myParent;
        //public to expose to editor
        public string nameValue = "";
        public float scoreValue =0;
        public string Name
        {
            get
            {
                return nameValue;
            }
            set
            {
                if (Name.Length > myParent.nameCharacterLength)
                {
                    nameValue = Name.Substring(0, myParent.nameCharacterLength);
                }
                else
                {
                    Debug.Log(Name);
                    nameValue = Name;
                }
            }
        }
        private NameToScore()
        {

        }
        public NameToScore(HighScoreSystem parent)
        {
            myParent = parent;
            scoreValue = 0;
        }


        int IComparable<NameToScore>.CompareTo(NameToScore other)
        {
            return this.scoreValue.CompareTo(other.scoreValue) * -1;
        }

    }

    public void Load()
    {
        string jsonString = PlayerPrefs.GetString(fileName);
        if(jsonString != "")
        {
            JsonUtility.FromJsonOverwrite(jsonString, highScoreList);
        }
        
    }
    public void Save()
    {
        highScoreList.Sort();
        string jsonValue = JsonUtility.ToJson(highScoreList);
        PlayerPrefs.SetString(fileName, jsonValue);
    }
    public void AddValue(string name, float score)
    {
        //at this point we assume the list is still sorted
        //and pop the back value if we detect we should be inserted
        //iterating backwards should let us early out if we can
        for (int i =highScoreList.Count -1; i >=0; --i)
        {
            if(score > highScoreList[i].scoreValue)
            {
                NameToScore item = new NameToScore(this);
                item.Name = name;
                item.scoreValue = score;
                highScoreList.Insert(i,  item);

                if(highScoreList.Count > maximumNames)
                {
                    highScoreList.RemoveAt(highScoreList.Count - 1);
                }
                break;
            }
        }
    }

    private void OnEnable()
    {
        Debug.Log("called load");
        //This could be improved on to try and keep a static instance
        //or use dont destroy on load
        Load();
    }
}
