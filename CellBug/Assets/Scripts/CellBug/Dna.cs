//核心机制

public class Dna
{
	//两条链
	private int[] DnaLineOne = new int[14]{0,0,0,0,0,0,0,0,0,0,0,0,0,0}; 
	private int[] DnaLineTwo = new int[14]{0,0,0,0,0,0,0,0,0,0,0,0,0,0};
	//变异后的一条链
	private int[] DnaLineChange = new int[14]{0,0,0,0,0,0,0,0,0,0,0,0,0,0};
	
	public void SetDna(int[] DnaLineOne,int[] DnaLineTwo)
	{
		for(int i = 0; i < 14; i++)
		{
			this.DnaLineOne[i] = DnaLineOne[i];
			this.DnaLineTwo[i] = DnaLineTwo[i];
		}
	}
	
	public int[] GetDna()
	{
		return DnaLineOne,DnaLineTwo;
	}
	
	public int GetDnaIndex(int index)
	{
		return DnaLineOne[index],DnaLineTwo[index];
	}
	
	//产生后代时放出的DNA链
	private int[] DnaVariation()
	{
		int seed = unchecked((int)DateTime.Now.Ticks;
		//那条链返回
		Random ranWhatLine = new Random(seed);
		int numWhatLine = ranWhatLine.Next(1,3);		
		if(numWhatLine == 1)
		{
			for(int i = 0; i < 14; i++)
			{
				DnaLineChange[i] = DnaLineOne[i];
			}
		}
		else if(numWhatLine == 1)
		{
			for(int i = 0; i < 14; i++)
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
			int numWhatDNA = ranWhatDNA.Next(1,15);
			DnaLineChange[numWhatDNA] = Math.abs(DnaLineChange[numWhatDNA] - 1);
		}
		
		return DnaLineChange;
	}
}