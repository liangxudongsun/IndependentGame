//核心机制

using System;
public class Dna
{
	//两条链
    private int[] DnaLineOne = new int[Const.DnaLineLength];
    private int[] DnaLineTwo = new int[Const.DnaLineLength];
	//变异后的一条链
    private int[] DnaLineChange = new int[Const.DnaLineLength];
	
	public void SetDna(int[] DnaLineOne,int[] DnaLineTwo)
	{
		for(int i = 0; i < Const.DnaLineLength; i++)
		{
			this.DnaLineOne[i] = DnaLineOne[i];
			this.DnaLineTwo[i] = DnaLineTwo[i];
		}
	}
	
	public int[] GetDnaLine(Const.DnaLineEnum dnaLine)
	{
        if (dnaLine == Const.DnaLineEnum.OneEnum)
        {
            return DnaLineOne;
        }
        else if (dnaLine == Const.DnaLineEnum.TwoEnum)
        {
            return DnaLineTwo;
        }
        return null;
	}
	
	public int GetDnaIndex(Const.DnaLineEnum dnaLine,Const.GenesEnum index)
	{
        if (dnaLine == Const.DnaLineEnum.OneEnum)
        {
            return DnaLineOne[(int)index];
        }
        else if (dnaLine == Const.DnaLineEnum.TwoEnum)
        {
            return DnaLineTwo[(int)index];
        }
        return 0;
	}
	
	//产生后代时放出的DNA链
	private int[] DnaVariation()
	{
		int seed = DateTime.Now.Millisecond;
		//那条链返回
		Random ranWhatLine = new Random(seed);
		int numWhatLine = ranWhatLine.Next(1,3);
		
		if(numWhatLine == 1)
		{
			for(int i = 0; i < Const.DnaLineLength; i++)
			{
				DnaLineChange[i] = DnaLineOne[i];
			}
		}
		else if(numWhatLine == 1)
		{
			for(int i = 0; i < Const.DnaLineLength; i++)
			{
				DnaLineChange[i] = DnaLineTwo[i];
			}
		}

		//有没有发生突变
		Random ranCanChange = new Random(seed + 1000);
		int numCanChange = ranCanChange.Next(1,1000);
		
		if(numCanChange >= 1 && numCanChange <= 10)
		{
			//突变
			Random ranWhatDNA = new Random(seed + 2000);
			int numWhatDNA = ranWhatDNA.Next(1,Const.DnaLineLength + 1);
			DnaLineChange[numWhatDNA] = Math.Abs(DnaLineChange[numWhatDNA] - 1);
		}
		
		return DnaLineChange;
	}
}