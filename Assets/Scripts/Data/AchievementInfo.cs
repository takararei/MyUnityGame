using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
[Serializable]
public class AchievementInfo
{
    public int id;
    public string name;
    public string introduce;
    public int Count;
    public string unFinishSprite;
    public string FinshedSprite;
}

public enum AcType
{
    FirstGold,
}