using CodeToSurvive.DLL;

public static class UniversityService
{
    public static bool CanEnroll(Player p, Course c)
    {
        return p.Energy >= c.EnergyCost;
    }

    public static void CompleteCourse(Player p, Course c)
    {
        p.CodingSkill += c.SkillGain;
        p.Energy -= c.EnergyCost;
        p.CurrentDay += c.DurationDays;
    }
}