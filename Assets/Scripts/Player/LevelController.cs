using System.Collections.Generic;

public class LevelController
{
    public int CurrentLvl { get; private set; } = 1;
    public int CurrentExp { get; private set; }
    public int SkillPoints { get; private set; }

    public float ExpMultimply { get; set; } = 1; // ���������� ����� ��������(������). �� �����������

    public delegate void Handler(bool newLvl);
    public event Handler LvlUpdateEvent;

    private readonly int maxLvl = 5;
    // ����������� ���������� ����� ��� ������
    private readonly Dictionary<int, int> expToUpgrade = new Dictionary<int, int>
    {
        {2, 5 },// 25
        {3, 10 },//70
        {4, 200 },
        {5, 500 }
    };


    public void TakeExp(int exp)
    {
        CurrentExp += (int)(exp * ExpMultimply);
        LvlUpgrade();
    }

    public void SetLvlAndEx(int lvl, int ex, int skillPoints)
    {
        CurrentLvl = lvl;
        CurrentExp = ex;
        SkillPoints = skillPoints;
        // ���� ���� ������������� ����������� ���������� ������� ��� ������ ����� ������
        if (skillPoints > 0)
        {
            ReferencesToObjects.Canvas.GetComponent<LvlUpVisualizer>().Start();
            LvlUpdateEvent(false);
        }
        
    }

    public int GetMaxExp()
    {
        return expToUpgrade[CurrentLvl + 1];
    } 

    public void ReduceSkillPoint()
    {
        SkillPoints--;
    }

    private void LvlUpgrade()
    {

        if(CurrentLvl != maxLvl && expToUpgrade[CurrentLvl + 1] <= CurrentExp)
        {
            CurrentExp -= expToUpgrade[CurrentLvl + 1];
            CurrentLvl++;
            SkillPoints++;

            LvlUpdateEvent(true);

            ReferencesToObjects.ItemController.OpenItemMenu();
        }
    }
}
