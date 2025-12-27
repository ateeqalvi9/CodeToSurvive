using CodeToSurvive.DLL;

public static class JobService
{
    public static void UpdateJobLevel(Player p)
    {
        if (p.CodingSkill >= 25)
            p.JobLevel = JobLevel.SeniorDeveloper;
        else if (p.CodingSkill >= 15)
            p.JobLevel = JobLevel.SeniorDeveloper;
        else if (p.CodingSkill >= 8)
            p.JobLevel = JobLevel.MidDeveloper;
        else if (p.CodingSkill >= 3)
            p.JobLevel = JobLevel.JuniorDeveloper;
    }

    public static int CalculateSalary(Player p)
    {
        return p.JobLevel switch
        {
            JobLevel.JuniorDeveloper => 300,
            JobLevel.MidDeveloper => 500,
            JobLevel.SeniorDeveloper => 800,
            _ => 0
        };
    }
}