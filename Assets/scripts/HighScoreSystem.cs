using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

public class HighScoreSystem : MonoBehaviour
{
    public string scoreKey = "HighScore";
    public int nameCharacterLength = 3;
    public int maximumNames = 10;
    public List<NameToScore> highScoreList = new List<NameToScore>();
    [System.Serializable]
    public class NameToScore : IComparable<NameToScore>
    {
        //public to expose to editor
        public string nameValue = "";
        public float scoreValue = 0;
        public NameToScore()
        {

        }


        int IComparable<NameToScore>.CompareTo(NameToScore other)
        {
            return this.scoreValue.CompareTo(other.scoreValue) * -1;
        }

    }

    public void Load()
    {
        int count = 0;
        string jsonString = PlayerPrefs.GetString(scoreKey + count.ToString(), "");
        while (jsonString != "" && count < maximumNames)
        {
            NameToScore data = new NameToScore();
            JsonUtility.FromJsonOverwrite(jsonString, data);
            
            highScoreList[count].nameValue = data.nameValue;
            highScoreList[count].scoreValue = data.scoreValue;

            //can't add to the array, must override existing (editor exposed) variables
            ++count;
            jsonString = PlayerPrefs.GetString(scoreKey + count.ToString(), "");
        }

    }
    public void Save()
    {
        //should be save to delete here as we are about to save again
        PlayerPrefs.DeleteAll();
        highScoreList.Sort();
        int count = 0;
        foreach (NameToScore data in highScoreList)
        {
            string jsonValue = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(scoreKey + count.ToString(), jsonValue);
            ++count;
        }

    }
    public void AddValue(string name, float score)
    {
        //at this point we assume the list is still sorted
        //and pop the back value if we detect we should be inserted
        //iterating backwards should let us early out if we can
        for (int i = 0; i < highScoreList.Count; ++i)
        {
            if (score > highScoreList[i].scoreValue)
            {
                //save a copy of the exisitng data
                NameToScore item = new NameToScore();
                item.nameValue = highScoreList[i].nameValue;
                item.scoreValue = highScoreList[i].scoreValue;

                //ovewrite the exisitng etry
                highScoreList[i].nameValue = name;
                highScoreList[i].scoreValue = score;

                Debug.Log("Value at index " + i.ToString() + " is now " + highScoreList[i].nameValue + " " + highScoreList[i].scoreValue.ToString());

                Debug.Log("it was  " + item.nameValue + " " + item.scoreValue.ToString());
                //propogae down list for follow on changes

                for(int j =i +1; j < highScoreList.Count; ++j)
                {
                    //cache the 'next item' in list
                    NameToScore nextItem = new NameToScore();
                    nextItem.nameValue = highScoreList[j].nameValue;
                    nextItem.scoreValue = highScoreList[j].scoreValue;

                    //overwrite data
                    highScoreList[j].nameValue = item.nameValue;
                    highScoreList[j].scoreValue = item.scoreValue;

                    //update item
                    item.nameValue = nextItem.nameValue;
                    item.scoreValue = nextItem.scoreValue;

                    Debug.Log("Value at previous index " + (j-1).ToString() + " is now " + highScoreList[j-1].nameValue + " " + highScoreList[j-1].scoreValue.ToString());
                    Debug.Log("Value at index " + j.ToString() + " is now " + highScoreList[j].nameValue + " " + highScoreList[j].scoreValue.ToString());
                }

                break;
            }
        }
    }

    private void OnEnable()
    {
        //This could be improved on to try and keep a static instance
        //or use dont destroy on load
        Load();
    }
}
