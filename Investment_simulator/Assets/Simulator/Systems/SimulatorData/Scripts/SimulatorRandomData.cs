using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulatorRandomData : MonoBehaviour
{
    public enum Projects {Technology, RealEstate, Manufacturing, Financial};

    [SerializeField] private Projects viableProject1;
    [SerializeField] private Projects viableProject2;
    [SerializeField] private int viableProject1InitialInvest;
    [SerializeField]private int viableProject2InitialInvest;

    [Header("Expected Profit")]
    public int expectedProfitTime;
    [SerializeField] private int expectedProfitTime_min;
    [SerializeField] private int expectedProfitTime_max;

    [Header("Min Percentage Of Profit")]
    public int minPercentageOfProfit;
    [SerializeField] private int minPercentageOfProfit_min;
    [SerializeField] private int minPercentageOfProfit_max;

    [Header("Capital Available")]
    [SerializeField] public float capitalAvailable;//calcular con las otra variables

    [Header("Total Debt")]
    [SerializeField] public float totalDebt; //calcular con las otras variables

    [Header("Technology")]
    public int tec_initialInvest;
    [SerializeField] private int tec_initialInvest_min;
    [SerializeField] private int tec_initialInvest_max;
    public float tec_opportunityCost;
    [SerializeField] private float tec_opportunityCostViable_min;
    [SerializeField] private float tec_opportunityCostViable_max;
    [SerializeField] private float tec_opportunityCostNonViable_min;
    [SerializeField] private float tec_opportunityCostNonViable_max;
    public float tec_annualCashFlow;//calcular con las otras variables
    public float tec_annualPersonnelExpenses; //calcular con las otras variables
    public float tec_annualAdvertisingExpenses; //calcular con las otras variables
    public float tec_annualSales; //calcular con las otras variables
    
    [Header("Real estate")]
    public int rS_initialInvest;
    [SerializeField] private int rS_initialInvest_min;
    [SerializeField] private int rS_initialInvest_max;
    public float rS_opportunityCost;
    [SerializeField] private float rS_opportunityCostViable_min;
    [SerializeField] private float rS_opportunityCostViable_max;
    [SerializeField] private float rS_opportunityCostNonViable_min;
    [SerializeField] private float rS_opportunityCostNonViable_max;
    public int rS_salePrice;
    [SerializeField] private int rS_salePrice_min;
    [SerializeField] private int rS_salePrice_max;
    public float rS_annualCashFlow;//calcular con las otras variables
    public float rS_annualMaintenanceExpenses;//calcular con las otras variables
    public float rS_annualTaxExpense;//calcular con las otras variables
    public float rS_AnnualLease;//calcular con las otras variables

    [Header("Manufacturing")]
    public int manu_initialInvest;
    [SerializeField] private int manu_initialInvest_min;
    [SerializeField] private int manu_initialInvest_max;
    public float manu_opportunityCost;
    [SerializeField] private float manu_opportunityCostViable_min;
    [SerializeField] private float manu_opportunityCostViable_max;
    [SerializeField] private float manu_opportunityCostNonViable_min;
    [SerializeField] private float manu_opportunityCostNonViable_max;
    public float manu_unitsProducedAnnually;
    [SerializeField] private int manu_unitsProducedAnnually_min;
    [SerializeField] private int manu_unitsProducedAnnually_max;
    public float manu_annualCashFlow;//calcular con las otras variables
    public float manu_unitCostProduction;//calcular con las otras variables
    public float manu_annualOperatingExpenses;//calcular con las otras variables
    public float manu_annualAdvertisingExpenses;//calcular con las otras variables
    public float manu_unitSellingPrice;//calcular con las otras variables

    [Header("Financial")]
    public int fin_initialInvest;
    [SerializeField] private int fin_initialInvest_min;
    [SerializeField] private int fin_initialInvest_max;
    public float fin_opportunityCost; //Esta variable tomará un valor aleatorio dependiendo de las siguientes condiciones
    [SerializeField] private float fin_opportunityCostViable_min;
    [SerializeField] private float fin_opportunityCostViable_max;
    [SerializeField] private float fin_opportunityCostNonViable_min;
    [SerializeField] private float fin_opportunityCostNonViable_max;
    public float fin_annualProduction;//calcular con las otras variables

    public static SimulatorRandomData instance;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            print("Random data already exists");
        }
        else {
            instance = this.gameObject.GetComponent<SimulatorRandomData>();
        }

        //viable projects
        tec_initialInvest = Random.Range(tec_initialInvest_min, tec_initialInvest_max + 1);
        rS_initialInvest = Random.Range(rS_initialInvest_min, rS_initialInvest_max + 1);
        manu_initialInvest = Random.Range(manu_initialInvest_min, manu_initialInvest_max + 1);
        fin_initialInvest = Random.Range(fin_initialInvest_min, fin_initialInvest_max + 1);
        fin_initialInvest = Random.Range(fin_initialInvest_min, fin_initialInvest_max + 1);

        SetViableProjects();

        //Expected profit
        expectedProfitTime = Random.Range(expectedProfitTime_min, expectedProfitTime_max + 1);

        //Min Percentage Of Profit
        minPercentageOfProfit = Random.Range(minPercentageOfProfit_min, minPercentageOfProfit_max + 1);

        //Capital Available
        capitalAvailable = Mathf.Ceil((viableProject1InitialInvest + viableProject2InitialInvest) * 1.005f);

        //Total Debt


        //Technology
        tec_opportunityCost = SetOportunityCost(Projects.Technology, tec_opportunityCostViable_min, tec_opportunityCostViable_max, tec_opportunityCostNonViable_min, tec_opportunityCostNonViable_max);

        //Real estate
        rS_opportunityCost = SetOportunityCost(Projects.RealEstate, rS_opportunityCostViable_min, rS_opportunityCostViable_max, rS_opportunityCostNonViable_min, rS_opportunityCostNonViable_max);
        rS_salePrice = Random.Range(rS_salePrice_min, rS_salePrice_max + 1);

        //Manufacturing
        manu_opportunityCost = SetOportunityCost(Projects.Manufacturing, manu_opportunityCostViable_min, manu_opportunityCostViable_max, manu_opportunityCostNonViable_min, manu_opportunityCostNonViable_max);
        manu_unitsProducedAnnually = Random.Range(manu_unitsProducedAnnually_min, manu_unitsProducedAnnually_max + 1);

        //Financial
        fin_opportunityCost = SetOportunityCost(Projects.Financial, fin_opportunityCostViable_min, fin_opportunityCostViable_max, fin_opportunityCostNonViable_min, fin_opportunityCostNonViable_max);
        fin_annualProduction = GetFinAnnualProduction();
    }

    float  SetOportunityCost(Projects proj, float opportunityCostViable_min, float opportunityCostViable_max, float opportunityCostNonViable_min, float opportunityCostNonViable_max) {
        float opportunityCost = 0;
        if (viableProject1 == proj || viableProject2 == proj)
        { //if one of the viable proyects is Technology
            opportunityCost = Random.Range(opportunityCostViable_min, opportunityCostViable_max);
        }
        else
        {
            opportunityCost = Random.Range(opportunityCostNonViable_min, opportunityCostNonViable_max);
        }
        return opportunityCost = Mathf.Round(opportunityCost * 10.0f) * 0.1f;//round to one decimal
    }


    public void SetViableProjects() {
        viableProject1 = (Projects)Random.Range(0, 4);
        viableProject1InitialInvest = SetviableProjectInitialInvest(viableProject1);

        viableProject2 = viableProject1;
        while (viableProject2 == viableProject1)
        {
            viableProject2 = (Projects)Random.Range(0, 4);
        }
        viableProject2InitialInvest = SetviableProjectInitialInvest(viableProject2);
    }


    int SetviableProjectInitialInvest(Projects proj)
    {
        int initInvest = 0;

        if (proj == Projects.Financial)
        {
            initInvest = fin_initialInvest;
        }
        if (proj == Projects.Manufacturing)
        {
            initInvest = manu_initialInvest;
        }
        if (proj == Projects.RealEstate)
        {
            initInvest = rS_initialInvest;
        }
        if (proj == Projects.Technology)
        {
            initInvest = tec_initialInvest;
        }
        return initInvest;
    }

    float GetFinAnnualProduction() {
        if (expectedProfitTime == 3) {
            return (fin_initialInvest / 2.53f);
        }
        if (expectedProfitTime == 4)
        {
            return (fin_initialInvest / 3.24f);
        }
        if (expectedProfitTime == 5)
        {
            return (fin_initialInvest / 3.89f);
        }
        return -1;
    }
}
