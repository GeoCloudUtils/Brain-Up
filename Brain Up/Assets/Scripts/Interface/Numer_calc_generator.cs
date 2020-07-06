using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Numer_calc_generator : MonoBehaviour
{
    public TextMeshProUGUI number_ui_text;
    public GameObject[] grid;
    public float wait_time = .5f;
    public const int min_numbers = 2;
    public int max_numbers = 2;

    public string calc = "";

    private TextMeshProUGUI txt;
    private List<int> used_positions = new List<int>();
    private void Start()
    {
        StartCoroutine(Get_calc());
    }

    private IEnumerator Get_calc()
    {
        int i_pos = -1;
        int result = 0;
        for (int i = 0; i < max_numbers; i++)
        {
            do
            {
                i_pos = UnityEngine.Random.Range(0, grid.Length);
            } while (used_positions.Contains(i_pos));
  
            RectTransform spawn_parent = grid[i_pos].GetComponent<RectTransform>();
            used_positions.Add(i_pos);
            txt = Instantiate(number_ui_text, Vector3.zero, Quaternion.identity) as TextMeshProUGUI;
            txt.GetComponent<RectTransform>().SetParent(spawn_parent);
            txt.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            txt.GetComponent<RectTransform>().localScale = Vector3.zero;
            
            int number = UnityEngine.Random.Range(1, 10);
            txt.SetText(number.ToString());
            result += number;
            if (i < max_numbers-1)
                calc += number.ToString() + "+";
            else
                calc += number.ToString() + " = " + result;     
            yield return new WaitForSeconds(wait_time);
        }
    }
}
