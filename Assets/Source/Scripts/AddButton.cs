using UnityEngine;
using TMPro;
public class AddButton : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField InputField;
    [SerializeField]
    private GameManager GameManager;
    public void ButtonClick()
    {
        string tmp = InputField.text;
        //InputField.text = string.Empty;
        int val = int.Parse(tmp);
        GameManager.CreatePopulation(val);
    }
}
