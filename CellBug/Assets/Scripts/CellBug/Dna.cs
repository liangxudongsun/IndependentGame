//���Ļ���
using System;

public class Dna
{
	//������
    private int[] dnaLineOne = new int[Const.DnaLineLength];
    private int[] dnaLineTwo = new int[Const.DnaLineLength];
	//������һ����
    private int[] DnaLineChange = new int[Const.DnaLineLength];
    public CellBug cellbug;

	public void SetDna(int[] DnaLineOne,int[] DnaLineTwo)
	{
		for(int i = 0; i < Const.DnaLineLength; i++)
		{
			this.dnaLineOne[i] = DnaLineOne[i];
			this.dnaLineTwo[i] = DnaLineTwo[i];
		}
	}
	
	public int[] GetDnaLine(Const.DnaLineEnum dnaLine)
	{
        if (dnaLine == Const.DnaLineEnum.OneEnum)
        {
            return dnaLineOne;
        }
        else if (dnaLine == Const.DnaLineEnum.TwoEnum)
        {
            return dnaLineTwo;
        }
        return null;
	}
	
	public int GetDnaIndex(Const.DnaLineEnum dnaLine,Const.GenesEnum index)
	{
        if (dnaLine == Const.DnaLineEnum.OneEnum)
        {
            return dnaLineOne[(int)index];
        }
        else if (dnaLine == Const.DnaLineEnum.TwoEnum)
        {
            return dnaLineTwo[(int)index];
        }
        return 0;
	}
	
	//�������ʱ�ų���DNA��
	public int[] DnaVariation()
	{
		int seed = (int)DateTime.Now.Ticks + 10 * cellbug.GetAbility().id;
		//����������
		Random random = new Random(seed);
		int numWhatLine = random.Next(1,3);
		
		if(numWhatLine == 1)
		{
			for(int i = 0; i < Const.DnaLineLength; i++)
			{
				DnaLineChange[i] = dnaLineOne[i];
			}
		}
		else if(numWhatLine == 2)
		{
			for(int i = 0; i < Const.DnaLineLength; i++)
			{
				DnaLineChange[i] = dnaLineTwo[i];
			}
		}

		//��û�з���ͻ��
		int numCanChange = random.Next(1,20);
		
		if(numCanChange >= 1 && numCanChange <= 10)
		{
			//ͻ��
			int numWhatDNA = random.Next(1,Const.DnaLineLength + 1);
			DnaLineChange[numWhatDNA - 1] = Math.Abs(DnaLineChange[numWhatDNA - 1] - 1);
		}
		
		return DnaLineChange;
	}
}