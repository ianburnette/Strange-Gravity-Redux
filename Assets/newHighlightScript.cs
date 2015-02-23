using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class newHighlightScript : MonoBehaviour {

	
	public Image controlImage;
	public Image selectRingP1, selectRingP2;
	public Color[] selectColors;
	public Shader myShader, highlightShader;
	public Transform planetBody;
	bool selected1 = false;
	bool selected2 = false;
	public float thickOutline, thinOutline;

	// Use this for initialization
	void Start () {
		myShader = planetBody.renderer.material.shader;
	}
	
	// Update is called once per frame
	public void StopHighlighting(int playerNumber) {
		if (playerNumber==0){
			if (!selected1){
				selectRingP1.enabled=false;
			}
		}
		if (playerNumber==1){
			if (!selected2){
				selectRingP2.enabled=false;
			}
		}
	}
	
	public void StopSelecting(int playerNumber) {
		if (playerNumber == 0){
			selected1 = false;
		}else {
			selected2 = false;
		}
	}
	
	public void Highlight(int degree,int playerNumber){
		if (playerNumber == 0){
			selectRingP1.enabled=true;
			if (degree == 1 && !selected1){
				selectRingP1.color = selectColors[degree];
			}	
			if (degree == 2 || degree == 3){
				selectRingP1.color = selectColors[degree];
				selected1 = true;
			}
			
			if (degree == 4 || degree == 5){
				selectRingP1.color = selectColors[degree];
			}
		}
		if (playerNumber == 1){
			selectRingP2.enabled=true;
			if (degree == 1 && !selected2){
				selectRingP2.color = selectColors[degree];
			}	
			if (degree == 2 || degree == 3){
				selectRingP2.color = selectColors[degree];
				selected2 = true;
			}
			
			if (degree == 4 || degree == 5){
				selectRingP2.color = selectColors[degree];
			}
		}
		
		
	/* 	if (degree==0){
			 if (selectRing.color == selectColors[1]){
				selectRing.color = selectColors[degree];
			}
			else if (selectRing.color == selectColors[4]){
				selectRing.color = selectColors[degree];
			}
			else if (selectRing.color == selectColors[3]){
				selectRing.color = selectColors[degree];
			}
		}
		else if (degree==1){
			 if (selectRing.color != selectColors[2] && selectRing.color != selectColors[3]){
				selectRing.color = selectColors[degree];
			}
		}
		else if (degree!=5){
			selectRing.color = selectColors[degree];
			selected = true;
		}
		else{
			selectRing.color = selectColors[0];
		} */
		
		
		
		/* if (degree!=0){
			selectRing.color = selectColors[degree];
		}
		else */
		
		//if (degree==1){
			
		//}
	}
	
	/* public void Highlight(int degree){
		print ("highlight");
		if (planetBody.renderer.material.shader != highlightShader){
			planetBody.renderer.material.shader = highlightShader;
		}
		if (degree == 1){
			if (!selected){
				planetBody.renderer.material.SetColor ("_OutlineColor", Color.green);
				planetBody.renderer.material.SetFloat ("_Outline", thinOutline);
			}
		}
		if (degree == 2){
			planetBody.renderer.material.SetColor ("_OutlineColor", Color.blue);
			planetBody.renderer.material.SetFloat ("_Outline", thickOutline);
			selected = true;
		}
		if (degree == 3){
			planetBody.renderer.material.SetColor ("_OutlineColor", Color.yellow);
			planetBody.renderer.material.SetFloat ("_Outline", thickOutline);
			selected = true;
		}
		if (degree == 4){
			planetBody.renderer.material.SetColor ("_OutlineColor", Color.red);
			planetBody.renderer.material.SetFloat ("_Outline", thickOutline);
		}
	} */
}
