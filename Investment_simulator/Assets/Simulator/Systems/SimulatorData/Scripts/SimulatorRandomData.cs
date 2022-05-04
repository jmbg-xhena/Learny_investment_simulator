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
    [SerializeField] private float tec_annualCashFlowRate1 = 2.62f;
    [SerializeField] private float tec_annualCashFlowRate2 = 3.39f;
    [SerializeField] private float tec_annualCashFlowRate3 = 4.1f;
    public float tec_annualPersonnelExpenses; //calcular con las otras variables
    public float tec_annualAdvertisingExpenses; //calcular con las otras variables
    public float tec_annualSales; //calcular con las otras variables
    public float tec_min_tir = 6;
    public float tec_max_tir = 8;
    
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
    [SerializeField] private float rS_annualCashFlowRate1 = 2.4f;
    [SerializeField] private float rS_annualCashFlowRate2 = 3.04f;
    [SerializeField] private float rS_annualCashFlowRate3 = 3.6f;
    public float rS_annualMaintenanceExpenses;//calcular con las otras variables
    public float rS_annualTaxExpense;//calcular con las otras variables
    public float rS_AnnualLease;//calcular con las otras variables
    public float rS_min_tir = 11;
    public float rS_max_tir = 13;

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
    [SerializeField] private float manu_annualCashFlowRate1 = 2.58f;
    [SerializeField] private float manu_annualCashFlowRate2 = 3.31f;
    [SerializeField] private float manu_annualCashFlowRate3 = 3.99f;
    public float manu_unitCostProduction;//calcular con las otras variables
    public float manu_annualOperatingExpenses;//calcular con las otras variables
    public float manu_annualAdvertisingExpenses;//calcular con las otras variables
    public float manu_unitSellingPrice;//calcular con las otras variables
    public float manu_min_tir = 7;
    public float manu_max_tir = 9;

    [Header("Financial")]
    public int fin_initialInvest;
    [SerializeField] private int fin_initialInvest_min;
    [SerializeField] private int fin_initialInvest_max;
    public float fin_opportunityCost; //Esta variable tomará un valor aleatorio dependiendo de las siguientes condiciones
    [SerializeField] private float fin_opportunityCostViable_min;
    [SerializeField] private float fin_opportunityCostViable_max;
    [SerializeField] private float fin_opportunityCostNonViable_min;
    [SerializeField] private float fin_opportunityCostNonViable_max;
    public float fin_annualProduction;
    public float fin_annualProductionRate1 = 2.53f;
    public float fin_annualProductionRate2 = 3.24f;
    public float fin_annualProductionRate3 = 3.89f;
    public float fin_min_tir = 8;
    public float fin_max_tir = 10;


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
        tec_annualCashFlow = GetAnnualCashFLow(tec_initialInvest, tec_annualCashFlowRate1, tec_annualCashFlowRate2, tec_annualCashFlowRate3);
        tec_annualPersonnelExpenses = Mathf.Ceil(tec_annualCashFlow * 3 * 0.75f);
        tec_annualAdvertisingExpenses = (tec_annualCashFlow * 3) - tec_annualPersonnelExpenses;
        tec_annualSales = tec_annualCashFlow * 4;

        //Real estate
        rS_opportunityCost = SetOportunityCost(Projects.RealEstate, rS_opportunityCostViable_min, rS_opportunityCostViable_max, rS_opportunityCostNonViable_min, rS_opportunityCostNonViable_max);
        rS_salePrice = Random.Range(rS_salePrice_min, rS_salePrice_max + 1);
        rS_annualCashFlow = GetAnnualCashFLow(rS_initialInvest - (rS_salePrice / Mathf.Pow(1.12f, expectedProfitTime)), rS_annualCashFlowRate1, rS_annualCashFlowRate2, rS_annualCashFlowRate3);
        rS_annualMaintenanceExpenses = (rS_annualCashFlow / 6.7f) * 0.33f;
        rS_annualTaxExpense = (rS_annualCashFlow / 6.7f) - rS_annualMaintenanceExpenses;
        rS_AnnualLease = rS_annualCashFlow + rS_annualMaintenanceExpenses + rS_annualTaxExpense;

        //Manufacturing
        manu_opportunityCost = SetOportunityCost(Projects.Manufacturing, manu_opportunityCostViable_min, manu_opportunityCostViable_max, manu_opportunityCostNonViable_min, manu_opportunityCostNonViable_max);
        manu_unitsProducedAnnually = Random.Range(manu_unitsProducedAnnually_min, manu_unitsProducedAnnually_max + 1);
        manu_annualCashFlow = GetAnnualCashFLow(manu_initialInvest, manu_annualCashFlowRate1, manu_annualCashFlowRate2, manu_annualCashFlowRate3);
        manu_unitCostProduction = Mathf.Ceil((manu_annualCashFlow / (manu_unitsProducedAnnually * 0.4f)) * 0.8f);
        manu_annualOperatingExpenses = Mathf.Ceil((manu_annualCashFlow / 0.4f) * 0.2f * 0.6f);
        manu_annualAdvertisingExpenses = Mathf.Ceil((manu_annualCashFlow / 0.4f * 0.2f) - manu_annualOperatingExpenses);
        manu_unitSellingPrice = (manu_annualCashFlow + manu_unitsProducedAnnually * manu_unitCostProduction + manu_annualOperatingExpenses + manu_annualAdvertisingExpenses) / manu_unitsProducedAnnually;

        //Financial
        fin_opportunityCost = SetOportunityCost(Projects.Financial, fin_opportunityCostViable_min, fin_opportunityCostViable_max, fin_opportunityCostNonViable_min, fin_opportunityCostNonViable_max);
        fin_annualProduction = GetFinAnnualProduction();
    }

    float GetAnnualCashFLow(float InitialInvest, float cashFlowRate1, float cashFlowRate2, float cashFlowRate3) {
        if (expectedProfitTime == 3) {
            return Mathf.Ceil(InitialInvest / cashFlowRate1);
        }
        if (expectedProfitTime == 4)
        {
            return Mathf.Ceil(InitialInvest / cashFlowRate2);
        }
        if (expectedProfitTime == 5)
        {
            return Mathf.Ceil(InitialInvest / cashFlowRate3);
        }
        return -1;
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
            return (fin_initialInvest / fin_annualProductionRate1);
        }
        if (expectedProfitTime == 4)
        {
            return (fin_initialInvest / fin_annualProductionRate2);
        }
        if (expectedProfitTime == 5)
        {
            return (fin_initialInvest / fin_annualProductionRate3);
        }
        return -1;
    }
}
