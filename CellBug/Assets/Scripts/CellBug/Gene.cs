//���Ļ���

public class Gene
{
    private int GeneIndex;
    public void SetGeneIndex(int GeneIndex){this.GeneIndex = GeneIndex;}
    public int GetGeneIndex(int GeneIndex) { return this.GeneIndex; }

    //��Ҫʱ����������
    public virtual void TimeDrive(float deltaTime) {};
}