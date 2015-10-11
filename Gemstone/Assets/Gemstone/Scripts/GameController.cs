using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public Gemstone gemstone;

    public int rowNum; //行数
    public int columnNum; //列数

    public ArrayList gemstoneList = new ArrayList();

    public AudioClip match3Clip;
    public AudioClip swapClip;
    public AudioClip errorClip;

    private Gemstone currentGemstone;
    private ArrayList matchesGemstone = new ArrayList();

    // Use this for initialization
    void Start()
    {
        for (int rowIndex = 0; rowIndex < rowNum; rowIndex++)
        {
            ArrayList temp = new ArrayList();
            for (int columnIndex = 0; columnIndex < columnNum; columnIndex++)
            {
                Gemstone c = AddGemstone(rowIndex, columnIndex);
                temp.Add(c);
            }
            gemstoneList.Add(temp);
        }

        if (CheckHorizontalMatches() || CheckVerticalMatches() || CheckSkewRightMatches() || CheckSkewLeftMatches())
        {
            RemoveMatches();
        }
    }

    public Gemstone AddGemstone(int rowIndex, int columnIndex)
    {  
        //生成宝石
        Gemstone c = Instantiate(gemstone) as Gemstone;
        c.transform.parent = this.transform;
        c.GetComponent<Gemstone>().RandomCreateGemstoneBg();
        c.GetComponent<Gemstone>().UpdatePosition(rowIndex, columnIndex);
        return c;
    }

    public Gemstone GetGemstone(int rowIndex, int columnIndex)
    {    //通过行号和列号，取得所对应位置的宝石
        ArrayList temp = gemstoneList[rowIndex] as ArrayList;
        Gemstone c = temp[columnIndex] as Gemstone;
        return c;
    }

    public void Select(Gemstone c)
    {
        if (currentGemstone == null)
        {
            currentGemstone = c;
            currentGemstone.isSelected = true;
            return;
        }
        else
        {
            if (Mathf.Abs(currentGemstone.rowIndex - c.rowIndex) + Mathf.Abs(currentGemstone.columnIndex - c.columnIndex) == 1)
            {
                StartCoroutine(ExangeAndMatches(currentGemstone, c));
            }
            else
            {
                audio.PlayOneShot(errorClip);
            }

            currentGemstone.isSelected = false;
            currentGemstone = null;
        }
    }

    IEnumerator ExangeAndMatches(Gemstone c1, Gemstone c2)
    {  
        //实现宝石交换并且检测是否匹配
        Exchange(c1, c2);
        yield return new WaitForSeconds(0.5f);
        if (CheckHorizontalMatches() || CheckVerticalMatches() || CheckSkewRightMatches() || CheckSkewLeftMatches())
        {
            RemoveMatches();
        }
        else
        {
            Debug.Log("没有检测到相同的宝石，交换回来！！");
            Exchange(c1, c2);
        }
    }

    bool CheckHorizontalMatches()
    {   
        //实现检测水平方向的匹配
        bool isMatches = false;
        for (int rowIndex = 0; rowIndex < rowNum; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < columnNum - 2; columnIndex++)
            {
                if ((GetGemstone(rowIndex, columnIndex).gemstoneType == GetGemstone(rowIndex, columnIndex + 1).gemstoneType) 
                    && (GetGemstone(rowIndex, columnIndex).gemstoneType == GetGemstone(rowIndex, columnIndex + 2).gemstoneType))
                {
                    Debug.Log("发现行相同的宝石");
                    AddMatches(GetGemstone(rowIndex, columnIndex));
                    AddMatches(GetGemstone(rowIndex, columnIndex + 1));
                    AddMatches(GetGemstone(rowIndex, columnIndex + 2));
                    isMatches = true;
                }
            }
        }
        return isMatches;
    }

    bool CheckVerticalMatches()
    {   
        //实现检测垂直方向的匹配
        bool isMatches = false;
        for (int columnIndex = 0; columnIndex < columnNum; columnIndex++)
        {
            for (int rowIndex = 0; rowIndex < rowNum - 2; rowIndex++)
            {
                if ((GetGemstone(rowIndex, columnIndex).gemstoneType == GetGemstone(rowIndex + 1, columnIndex).gemstoneType) 
                    && (GetGemstone(rowIndex, columnIndex).gemstoneType == GetGemstone(rowIndex + 2, columnIndex).gemstoneType))
                {
                    Debug.Log("发现列相同的宝石");
                    AddMatches(GetGemstone(rowIndex, columnIndex));
                    AddMatches(GetGemstone(rowIndex + 1, columnIndex));
                    AddMatches(GetGemstone(rowIndex + 2, columnIndex));
                    isMatches = true;
                }
            }
        }
        return isMatches;
    }


    bool CheckSkewRightMatches()
    {
        bool isMatches = false;
        for (int rowIndex = 0; rowIndex < rowNum - 2; rowIndex++)
        {
           for (int columnIndex = 0; columnIndex < columnNum - 2; columnIndex++)
            {
                if ((GetGemstone(rowIndex, columnIndex).gemstoneType == GetGemstone(rowIndex + 1, columnIndex + 1).gemstoneType)
                    && (GetGemstone(rowIndex, columnIndex).gemstoneType == GetGemstone(rowIndex + 2, columnIndex + 2).gemstoneType))
                {
                    Debug.Log("发现r宝石");
                    AddMatches(GetGemstone(rowIndex, columnIndex));
                    AddMatches(GetGemstone(rowIndex + 1, columnIndex + 1));
                    AddMatches(GetGemstone(rowIndex + 2, columnIndex + 2));
                    isMatches = true;
                }
            }
        }
        return isMatches;
    }

    bool CheckSkewLeftMatches()
    {
        bool isMatches = false;
        for (int rowIndex = rowNum - 1; rowIndex > 1; rowIndex--)
        {
            for (int columnIndex = 0; columnIndex < columnNum - 2; columnIndex++)
            {
                if ((GetGemstone(rowIndex, columnIndex).gemstoneType == GetGemstone(rowIndex - 1, columnIndex + 1).gemstoneType)
                    && (GetGemstone(rowIndex, columnIndex).gemstoneType == GetGemstone(rowIndex - 2, columnIndex + 2).gemstoneType))
                {
                    Debug.Log("发现l宝石");
                    AddMatches(GetGemstone(rowIndex, columnIndex));
                    AddMatches(GetGemstone(rowIndex - 1, columnIndex + 1));
                    AddMatches(GetGemstone(rowIndex - 2, columnIndex + 2));
                    isMatches = true;
                }
            }
        }
        return isMatches;
    }
    void AddMatches(Gemstone c)
    {
        if (matchesGemstone == null)
            matchesGemstone = new ArrayList();
        int index = matchesGemstone.IndexOf(c);  //检测该宝石是否已在数组当中
        if (index == -1)
        {
            matchesGemstone.Add(c);
        }
    }


    void RemoveMatches()
    {    //删除匹配的宝石
        for (int i = 0; i < matchesGemstone.Count; i++)
        {
            Gemstone c = matchesGemstone[i] as Gemstone;
            RemoveGemstone(c);
        }
        matchesGemstone = new ArrayList();
        StartCoroutine(WaitForCheckMatchesAgain());
    }

    IEnumerator WaitForCheckMatchesAgain()
    {
        yield return new WaitForSeconds(0.5f);
        if (CheckHorizontalMatches() || CheckVerticalMatches() || CheckSkewRightMatches() || CheckSkewLeftMatches())
        {
            RemoveMatches();
        }
    }

    void RemoveGemstone(Gemstone c)
    {  
        //删除宝石
        c.Dispose();
        audio.PlayOneShot(match3Clip);
        for (int i = c.rowIndex + 1; i < rowNum; i++)
        {
            Gemstone temGemstone = GetGemstone(i, c.columnIndex);
            temGemstone.rowIndex--;
            SetGemstone(temGemstone.rowIndex, temGemstone.columnIndex, temGemstone);
            temGemstone.TweenToPostion(temGemstone.rowIndex, temGemstone.columnIndex);
        }

        Gemstone newGemstone = AddGemstone(rowNum, c.columnIndex);
        newGemstone.rowIndex--;
        SetGemstone(newGemstone.rowIndex, newGemstone.columnIndex, newGemstone);
        newGemstone.TweenToPostion(newGemstone.rowIndex, newGemstone.columnIndex);
    }

    public void Exchange(Gemstone c1, Gemstone c2)
    {  //实现宝石之间交换位置
        audio.PlayOneShot(swapClip);
        SetGemstone(c1.rowIndex, c1.columnIndex, c2);
        SetGemstone(c2.rowIndex, c2.columnIndex, c1);

        //交换c1,c2的行号
        int tempRowIndex;
        tempRowIndex = c1.rowIndex;
        c1.rowIndex = c2.rowIndex;
        c2.rowIndex = tempRowIndex;

        //交换c1,c2的列号
        int tempColumnIndex;
        tempColumnIndex = c1.columnIndex;
        c1.columnIndex = c2.columnIndex;
        c2.columnIndex = tempColumnIndex;

        c1.TweenToPostion(c1.rowIndex, c1.columnIndex);
        c2.TweenToPostion(c2.rowIndex, c2.columnIndex);
    }

    public void SetGemstone(int rowIndex, int columnIndex, Gemstone c)
    {
        //设备所对应列号和列号位置的宝石
        ArrayList temp = gemstoneList[rowIndex] as ArrayList;
        temp[columnIndex] = c;
    }
}
