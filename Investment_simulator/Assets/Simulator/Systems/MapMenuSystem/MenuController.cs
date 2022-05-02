using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject MapStand;
    public GameObject MapStandCloseUp;
    public GameObject currentMenu;
    public List<GameObject> lastMenus;

    public void LoadMenu(GameObject menu) {
        if (currentMenu != null) {
            lastMenus.Add(currentMenu);
        }
        currentMenu = menu;
        menu.SetActive(true);
    }

    public void LoadCashFLow(GameObject cashflowsContoiner) {
        cashflowsContoiner.SetActive(true);
        lastMenus.Add(currentMenu);
        currentMenu = cashflowsContoiner.transform.GetChild(SimulatorRandomData.instance.expectedProfitTime - 3).gameObject;
        currentMenu.SetActive(true);
    }

    public void CloseMenu() {
        currentMenu.SetActive(false);
        if (lastMenus.Count > 0)
        {
            currentMenu = lastMenus[lastMenus.Count - 1];
            lastMenus.Remove(currentMenu);
            currentMenu.SetActive(true);
        }
    }

    public void ShowCloseUP()
    {
        MapStandCloseUp.SetActive(true);
    }

    public void CloseAllMenus() {
        MapStand.SetActive(false);
    }
}
