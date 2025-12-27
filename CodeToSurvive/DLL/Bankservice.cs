using CodeToSurvive.DLL;

public static class BankService
{
    public static int GetMaxLoan(Player p)
    {
        return p.Money == 0 ? 100 : (int)(p.Money * 0.10);
    }

    public static void ApplyLoan(Player p, int amount)
    {
        p.ActiveLoan = amount + (amount / 10);
        p.Money += amount;
        p.CreditScore -= 50;
    }

    public static void RepayLoan(Player p)
    {
        p.Money -= p.ActiveLoan;
        p.ActiveLoan = 0;
        p.CreditScore += 30;
    }
}