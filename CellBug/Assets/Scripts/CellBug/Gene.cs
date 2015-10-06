//核心机制

public class Gene
{
    private int GeneIndex;
    public void SetGeneIndex(int GeneIndex){this.GeneIndex = GeneIndex;}
    public int GetGeneIndex(int GeneIndex) { return this.GeneIndex; }

    //需要时间来驱动的
    public virtual void TimeDrive(float deltaTime) {};
}