using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : Singleton<CraftingSystem>
{
    public GameObject craftingScreenUI;
    public GameObject toolsScreenUI;

    public List<string> inventoryItemList = new List<string>();

    //Category Buttons
    private Button toolsBTN;

    //Craft Buttons
    private Button craftAxeBTN;

    //Requirement Text
    private Text AxeReq1, AxeReq2;

    public bool isOpen;

    //All Blueprint
    public Blueprint AxeBLP = new Blueprint("Axe",2,"Stone", 3, "Stick", 3);

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        toolsBTN = craftingScreenUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBTN.onClick.AddListener(delegate { OpenToolsCategory(); });

        //Axe
        AxeReq1 = toolsScreenUI.transform.Find("Axe").transform.Find("req1").GetComponent<Text>();
        AxeReq2 = toolsScreenUI.transform.Find("Axe").transform.Find("req2").GetComponent<Text>();

        craftAxeBTN = toolsScreenUI.transform.Find("Axe").transform.Find("Button").GetComponent<Button>();
        craftAxeBTN.onClick.AddListener(delegate { CraftAnyItem(AxeBLP); });
    }

    void CraftAnyItem(Blueprint blueprintToCraft)
    {
        InventorySystem.Instance.AddToInventory(blueprintToCraft.itemName);

        if (blueprintToCraft.numOfRequirements == 1)
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req1, blueprintToCraft.Req1amount);
        }

        else if (blueprintToCraft.numOfRequirements == 2)
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req1, blueprintToCraft.Req1amount);
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req2, blueprintToCraft.Req2amount);
        }

        StartCoroutine(calculate());

    }

    public IEnumerator calculate()
    {
        yield return 0;
        InventorySystem.Instance.ReCalculateList();
        RefreshNeededItems();
    }
    void OpenToolsCategory()
    {
        craftingScreenUI.SetActive(false);
        toolsScreenUI.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {
            craftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            toolsScreenUI.SetActive(false);
            if (!InventorySystem.Instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            isOpen = false;
        }
    }

    public void RefreshNeededItems()
    {
        int stone_count = 0;
        int stick_count = 0;

        inventoryItemList = InventorySystem.Instance.itemList;

        foreach (string itemName in inventoryItemList)
        {
            switch (itemName)
            {
                case "Stone":
                    stone_count += 1;
                    break;

                case "Stick":
                    stick_count += 1;
                    break;
            }
        }

        //----AXE----//

        AxeReq1.text = "3 Stone [" + stone_count + "]";
        AxeReq2.text = "3 Stick [" + stick_count + "]";

        if (stone_count >= 3 && stick_count >= 3)
        {
            craftAxeBTN.gameObject.SetActive(true);
        }
        else
        {
            craftAxeBTN.gameObject.SetActive(false);
        }
    }
}