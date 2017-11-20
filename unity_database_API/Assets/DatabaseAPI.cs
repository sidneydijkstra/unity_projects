using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DatabaseAPI : MonoBehaviour
{
    public string[] returning = new string[1];

    WWW content = null;

    // get al isbn
    public string[] getAllISBN()
    {
        returning = null;
        StartCoroutine(getAllISBN_());
        return returning;
    }
    IEnumerator getAllISBN_()
    {
        content = new WWW("http://localhost/unitydatabaseAPI/getAllIsbn.php");
        yield return content;
        if (content.error == "NULL" || content.error != null)
        {
            print("There was an error: " + content.error);
        }
        else
        {
            string info = content.text;
            string[] strarr = info.Split(',');
            returning = strarr;
        }
    }


    // get all titles
    public string[] getAllTitles()
    {
        returning = null;
        StartCoroutine(getAllTitles_());
        return returning;
    }
    IEnumerator getAllTitles_()
    {
        content = new WWW("http://localhost/unitydatabaseAPI/getAllTitles.php");
        yield return content;
        if (content.error == "NULL" || content.error != null)
        {
            print("There was an error: " + content.error);
        }
        else
        {
            string info = content.text;
            string[] strarr = info.Split(',');
            returning = strarr;
        }
    }


    // get all subjectcodes
    public string[] getAllSubjectCodes()
    {
        returning = null;
        StartCoroutine(getAllSubjectCodes_());
        return returning;
    }
    IEnumerator getAllSubjectCodes_()
    {
        content = new WWW("http://localhost/unitydatabaseAPI/getAllSubjectCodes.php");
        yield return content;
        if (content.error == "NULL" || content.error != null)
        {
            print("There was an error: " + content.error);
        }
        else
        {
            string info = content.text;
            string[] strarr = info.Split(',');
            returning = strarr;
        }
    }

    // get tilte by name
    public string[] getTitleByName(string name_)
    {
        returning = null;
        StartCoroutine(getTitleByName_(name_));
        return returning;
    }
    IEnumerator getTitleByName_(string name_)
    {
        content = new WWW("http://localhost/unitydatabaseAPI/getTitleByName.php?name=" + name_);
        yield return content;
        if (content.error == "NULL" || content.error != null)
        {
            print("There was an error: " + content.error);
        }
        else
        {
            string info = content.text;
            string[] strarr = info.Split(',');
            returning = strarr;
        }
    }


    // get all data by title
    public string[] getAllDataByName(string name_)
    {
        returning = null;
        StartCoroutine(getAllDataByName_(name_));
        return returning;
    }
    IEnumerator getAllDataByName_(string name_)
    {
        content = new WWW("http://localhost/unitydatabaseAPI/getAllDataByName.php?name=" + name_);
        print(name_);
        yield return content;
        if (content.error == "NULL" || content.error != null)
        {
            print("There was an error: " + content.error);
        }
        else
        {
            string info = content.text;
            string[] strarr = info.Split(',');
            returning = strarr;
        }
    }


    // get all data by isbn
    public string[] getAllDataByISBN(string name_)
    {
        returning = null;
        StartCoroutine(getAllDataByISBN_(name_));
        return returning;
    }
    IEnumerator getAllDataByISBN_(string name_)
    {
        content = new WWW("http://localhost/unitydatabaseAPI/getAllDataByISBN.php?isbn" + name_);
        print(name_);
        yield return content;
        if (content.error == "NULL" || content.error != null)
        {
            print("There was an error: " + content.error);
        }
        else
        {
            string info = content.text;
            string[] strarr = info.Split(',');
            returning = strarr;
        }
    }

    // normal loader

    // check if content is done
    public bool isDone()
    {
        return content.isDone;
    }

    // get the content
    public string[] getContent()
    {
        if (returning != null)
        {
            return returning;
        }
        else
        {
            return null;
        }

    }
}
