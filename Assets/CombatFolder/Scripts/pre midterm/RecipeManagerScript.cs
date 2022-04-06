using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RecipeManagerScript : MonoBehaviour
{
    public List<Recipe> recipeList;
    public GameObject player;
    public TextMeshProUGUI instruction;
    public TextMeshProUGUI match;
    public TextMeshProUGUI argyi;
    public TextMeshProUGUI nail;
    public TextMeshProUGUI bossMat;
    public Image outcomeIcon;
    public Image matInv;
    public Image leftInv;
    public Image rightInv;
    public Image upInv;
    public Image downInv;

    public List<GameObject> possibleCombinations;

    private void Update()
    {
        if(outcomeIcon.sprite == null)
        {
            outcomeIcon.color = new Color32(255, 255, 255, 0);
        }
        else
        {
            outcomeIcon.color = new Color32(255, 255, 255, 255);
        }
    }

    public void SearchRecipeForMats(List<GameObject> choosenMats)
    {
        for (int i = 0; i < recipeList.Count; i++)
        {
            List<GameObject> recipeMats = new List<GameObject>();
            foreach (var mat in recipeList[i].materials)
            {
                recipeMats.Add(mat.Mats);
            }
            if (CompareList(i, choosenMats, recipeMats))
            {
                player.SendMessage("ChangeSpell", recipeList[i]);
                instruction.text = "";
                /*instruction.text = "Selected Materials: ";
                foreach (var mat in recipeList[i].Materials)
                {z
                    instruction.text += mat.name + "\n";
                }
                */
                outcomeIcon.sprite = recipeList[i].Outcome.GetComponent<MatScript>().Icon;
                instruction.text += recipeList[i].Outcome.name;
                return;
            }
            if (i == recipeList.Count - 1)
            {
                player.SendMessage("RefreshChoosenMats");
                outcomeIcon.sprite = choosenMats[choosenMats.Count - 1].GetComponent<MatScript>().Icon;
                if (choosenMats[choosenMats.Count - 1].GetComponent<MatScript>().matCastType == SpellCtrlScript.CastType.none) 
                    instruction.text = choosenMats[choosenMats.Count - 1].name;
                player.SendMessage("RefreshSpell", choosenMats[choosenMats.Count - 1]);
            }
        }
    }

    public void SearchForCombinations(List<GameObject> matList)
    {
        possibleCombinations.Clear();
        match.color = new Color32(255, 255, 255, 255);
        argyi.color = new Color32(255, 255, 255, 255);
        nail.color = new Color32(255, 255, 255, 255);
        bossMat.color = new Color32(255, 255, 255, 255);
        leftInv.color = new Color32(255, 255, 255, 255);
        rightInv.color = new Color32(255, 255, 255, 255);
        upInv.color = new Color32(255, 255, 255, 255);
        downInv.color = new Color32(255, 255, 255, 255);

        //combination.text = "Possible Combination:\n";
        for (int i = 0; i < recipeList.Count; i++)
        {
            List<GameObject> recipeMats = new List<GameObject>();
            foreach (var mat in recipeList[i].materials)
            {
                recipeMats.Add(mat.Mats);
            }
            if (ContainsList(i, matList, recipeMats))
            {
                for (int c = 0; c < recipeList[i].materials.Count; c++)
                {
                    possibleCombinations.Add(recipeList[i].materials[c].Mats);
                }
                for (int c = 0; c < possibleCombinations.Count; c++)
                {
                    for (int d = 0; d < matList.Count; d++)
                    {
                        if (matList[d].name == "Match - Low Damage Bullet")
                        {
                            match.color = new Color32(87, 212, 197, 255);
                            leftInv.color = new Color32(87, 212, 197, 255);
                        }
                        if (matList[d].name == "Argyi - Self Healing")
                        {
                            argyi.color = new Color32(87, 212, 197, 255);
                            upInv.color = new Color32(87, 212, 197, 255);
                        }
                        if (matList[d].name == "Copper Nail - Drop Material Bullet")
                        {
                            nail.color = new Color32(87, 212, 197, 255);
                            rightInv.color = new Color32(87, 212, 197, 255);
                        }
                        if (matList[d].name == "Cotton" || matList[d].name == "Tear")
                        {
                            bossMat.color = new Color32(87, 212, 197, 255);
                            downInv.color = new Color32(87, 212, 197, 255);
                        }
                        if (possibleCombinations[c].name == matList[d].name)
                        {
                            possibleCombinations.RemoveAt(c);
                        }
                    }
                    //谜之bug用了魔法解决问题...我真的不行了
                    if (player.GetComponent<PlayerScript>().currentMat.name != "Match - Low Damage Bullet" &&
                        player.GetComponent<PlayerScript>().currentMat.name != "Argyi - Self Healing" &&
                        player.GetComponent<PlayerScript>().currentMat.name != "Copper Nail - Drop Material Bullet" &&
                        player.GetComponent<PlayerScript>().currentMat.name != "Cotton" &&
                        player.GetComponent<PlayerScript>().currentMat.name != "Tear")
                    {
                        if (possibleCombinations.Count > 0)
						{
                            possibleCombinations.Clear();
                        }
                    }
                }
            }
        }
        foreach (var mat in possibleCombinations)
        {
            //combination.text += mat.name + "\n";
            if (mat.name == "Match - Low Damage Bullet")
            {
                match.color = new Color32(215, 140, 90, 255);
                leftInv.color = new Color32(215, 140, 90, 255);
            }
            if (mat.name == "Argyi - Self Healing")
            {
                argyi.color = new Color32(215, 140, 90, 255);
                upInv.color = new Color32(215, 140, 90, 255);
            }
            if (mat.name == "Copper Nail - Drop Material Bullet")
            {
                nail.color = new Color32(215, 140, 90, 255);
                rightInv.color = new Color32(215, 140, 90, 255);
            }
            if (player.GetComponent<PlayerScript>().tempInventory[3].Mats != null && mat == player.GetComponent<PlayerScript>().tempInventory[3].Mats)
            {
                bossMat.color = new Color32(215, 140, 90, 255);
                downInv.color = new Color32(215, 140, 90, 255);
            }
        }
    }

    private bool ContainsList(int i, List<GameObject> matList, List<GameObject> requiredMats)
    {
        for (int a = 0; a < matList.Count; a++)
        {
            if (!requiredMats.Contains(matList[a]))
            {
                return false;
            }
        }
        return true;
    }

    private bool CompareList(int i, List<GameObject> matList, List<GameObject> requiredMats)
    {
        if (recipeList[i].materials.Count != matList.Count)
        {
            return false;
        }
        requiredMats.Sort(CompareSort);
        matList.Sort(CompareSort);
        for (int b = 0; b < requiredMats.Count; b++)
        {
            if (requiredMats[b] != matList[b])
                return false;
        }
        return true;
    }

    private int CompareSort(GameObject x, GameObject y)
    {
        return x.name.CompareTo(y.name);
    }


}
