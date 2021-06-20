
//Global variables
float4 _SeerPosition[64];
float _SeerRadius[64];
float _SeerGradient[64];
float4 _SeerColor[64];
float3 _SeerSize[64];
float _SeerHider[64];
int _Seers;

///-------------------------
/// Calculate if this pixel actually receive contribution from a Seer
///-------------------------
void Calculate_Reveal_float(in float4 Albedo, in float3 WorldPosition, in bool Hidable, in float Resistance, out float4 FinalColor)
{
	//Close enough to at least one Seer ?	
	bool Visible = false;
	float SavedVisibility = 1.0f;
    float4 SavedColorGradient;
    
	for (int Indice = 0; Indice < _Seers; Indice++)
	{
		//Extract Seer's data
		float3 SeerPosition = _SeerPosition[Indice];
		float SeerRadius = _SeerRadius[Indice];
		float3 SeerSize = _SeerSize[Indice];
		float SeerGradient = _SeerGradient[Indice];
        float4 SeerColor = _SeerColor[Indice];
		float SeerHider = _SeerHider[Indice];

		//Hidable material :  looking only for hider seer
		//Revealable material : looking only for normal seer
		if (Hidable != SeerHider) continue;
		
		//Calculating if this pixel lend into a Seer max radius	
		float Distance = 0;
		float DistanceLimit = SeerRadius;
		
		//Simple sphere
		Distance = distance(SeerPosition, WorldPosition) * Resistance;

		//Found a Seer
		if (Distance < DistanceLimit)
		{			
			Visible = true;
                        
			//Gradient
			float GradientStart = DistanceLimit - SeerGradient;
			float Visibility = max(0, ((Distance - GradientStart) / SeerGradient));				//Negative if inside enough to not be in the gradient zone
			Visibility = max(0, Visibility);

			//More visible than what we had before ?
			if (Visibility < SavedVisibility)
			{
				SavedVisibility = Visibility;
				SavedColorGradient = SeerColor;
			}
		}
	}

	//Apply effects if one Seer was close enough
    //float4 FinalColor;
	if (Visible == true)
	{	
		//Hidable : more transparent when close to seer
		if (Hidable == true)
		{	
			FinalColor.a = Albedo.a * (SavedVisibility);
			float FactorGradient = SavedVisibility * SavedColorGradient.a;
			if (FactorGradient < 0) FactorGradient = 0;
			if (FactorGradient > 1) FactorGradient = 1;
			FinalColor.rgb = Albedo.rgb * (1.0f - FactorGradient) + SavedColorGradient.rgb * FactorGradient;
		}
		else
		{
			FinalColor = Albedo * (1.0f - SavedVisibility) + SavedColorGradient * (SavedVisibility);	//Mix gradient and actual color
		}
	}
	//Nothing to display
	else
	{
		//Hidable : visible when far from seer
		if (Hidable == true)
		{
			FinalColor = Albedo;
			FinalColor.a = 1.0f;
		}
		else
		{
			FinalColor = Albedo;
			FinalColor.a = 0;
		}
	}
	
	//return FinalColor;
}