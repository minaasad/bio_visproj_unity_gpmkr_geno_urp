using System;
namespace AssemblyCSharp
{
	
	public class Patient : Person
	{
		float _position_X, _position_Y,_position_Z;
		
		public float Position_X  { get { return _position_X; } }
		public float Position_Y  { get { return _position_Y; } }
		public float Position_Z  { get { return _position_Z; } }
		
		public String ID { get; set; }
		public bool HasSurvived { get; set; }
		public int Cluster { get; set; }
		public bool HasRelapsed { get; set; }
		public String Chrom_Structure { get; set; }
		public String Cytogenics { get; set; }
		public String FISH { get; set; }
		public bool PH_rear { get; set; } //positive = true
		public bool TEL_AML1 { get; set; } //positive = true
		public bool MLL_AF4 { get; set; } //positive = true
		public bool E2A_PBX1 { get; set; } //positive = true
		public String Initial_WBCx109 { get; set; }
		public String Initial_Plateletx109 { get; set; }
		public String Initial_Blastsx109 { get; set; }
		public String Blasts_Percent { get; set; }
		public String Splenomegaly { get; set; }
		public String Phenotype { get; set; }
		public String Immunophenotype { get; set; }
		public bool CNS_Involvement { get; set; } //Yes = true
		public bool Mediastinal_Involvement { get; set; } //Yes = true
		public String Date_of_Diagnosis { get; set; }
		
		public Patient(){}
		
		public Patient(String iD){ID = iD;}
		
		public Patient(float x, float y, float z) {
			_position_X = x;_position_Y = y;_position_Z = z;
		}
		
		public Patient(float x, float y, float z, String iD)
			: this (iD)
		{
			_position_X = x;
			_position_Y = y;
			_position_Z = z;
		}
		
		public Patient(float pos_x, float pos_y, float pos_z, String iD, GenderType sex, Boolean is_alive)
			: this (pos_x,pos_y,pos_z)
		{
			ID = iD;
			Gender = sex;
		}
		
		public Patient(float pos_x, float pos_y, float pos_z, String iD, GenderType sex, Boolean is_alive, 
		               int cluster, String ethnicity, bool relapsed, String chrom_struct, String cytogenics,
		               String fish, bool phrear, bool telaml1, bool mllaf4, bool e2apbx1, String initwbcx109,
		               String initplateletx109, String initblastsx109, String blastspercent, String splenomegaly,
		               String phenotype, String immunophenotype, bool cnsinvolvement, bool mediastinalinvolvement)
			: this (pos_x,pos_y,pos_z,iD, sex, is_alive)                                                                     
		{                                                                                                  
			Cluster = cluster;
			Ethnicity = ethnicity;
			HasRelapsed = relapsed;
			Chrom_Structure = chrom_struct;
			Cytogenics = cytogenics;
			FISH = fish;
			PH_rear = phrear;
			TEL_AML1 = telaml1;
			MLL_AF4 = mllaf4;
			E2A_PBX1 = e2apbx1;
			Initial_WBCx109 = initwbcx109;
			Initial_Plateletx109 = initplateletx109;
			Initial_Blastsx109 = initblastsx109;
			Blasts_Percent = blastspercent;
			Splenomegaly = splenomegaly;
			Phenotype = phenotype;
			Immunophenotype = immunophenotype;
			CNS_Involvement = cnsinvolvement;
			Mediastinal_Involvement = mediastinalinvolvement;
		}
		
		public Patient(float pos_x, float pos_y, float pos_z, String iD, GenderType sex, Boolean is_alive, 
		               int cluster, String ethnicity, bool relapsed, String chrom_struct, String cytogenetics,
		               String fish, bool phrear, bool telaml1, bool mllaf4, bool e2apbx1, String initwbcx109,
		               String initplateletx109, String initblastsx109, String blastspercent, String splenomegaly,
		               String phenotype, String immunophenotype, bool cnsinvolvement, bool mediastinalinvolvement,
		               String BirthDate, String DiagnosisDate)
			: this (pos_x,pos_y,pos_z,iD, sex, is_alive, cluster, ethnicity, relapsed, chrom_struct, cytogenetics,
			        fish, phrear, telaml1, mllaf4, e2apbx1, initwbcx109, initplateletx109, initblastsx109, blastspercent,
			        splenomegaly, phenotype, immunophenotype, cnsinvolvement, mediastinalinvolvement)                                                                     
		{                                                
			Date_of_Birth = BirthDate;
			Date_of_Diagnosis = DiagnosisDate;
		}
		
	}                                                                                                      
}                                                                                                          











