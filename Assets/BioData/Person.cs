using System;
namespace AssemblyCSharp
{
	public enum GenderType
	{
		male,female
	}
	
	public class Person
	{    
		public String Ethnicity { get; set; }
		public GenderType Gender { get; set; }
		public String Date_of_Birth { get; set; }
		
		public Person (){}
		
		public Person(GenderType sex, String ethnicity, String DOB) {
			Gender = sex;
			Ethnicity = ethnicity;
			Date_of_Birth = DOB;
		}
	}
}


