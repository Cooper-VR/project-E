using UnityEngine;

public class AugmentMenu : MonoBehaviour
{
    private Animation anim;

    public GameObject Menu;



    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
    }

    public void OpenMenu()
    {
        Menu.SetActive(true);
        anim.Play("OpenMenu");
    }

    public void CloseMenu()
    {
        anim.Play("CloseMenu");
        Menu.SetActive(false);
    }
}
