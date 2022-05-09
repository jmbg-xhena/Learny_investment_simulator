using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManufacturingUI : MonoBehaviour
{
    [Header("Manufacturing Main Menu")]
    [SerializeField] private Text manu_mm_Title1;
    [SerializeField] private Text manu_mm_Title2;
    [SerializeField] private Text manu_mm_ButtonDescription;
    [SerializeField] private Text manu_ButtonCashFlow;
    [SerializeField] private Text manu_ButtonFinantialIndicators;

    [Header("Manufacturing Project Description Menu")]
    [SerializeField] private Text manu_pd_Title;
    [SerializeField] private Text manu_pd_Icon;
    [SerializeField] private Text manu_pd_Objective_title;
    [SerializeField] private Text manu_pd_Objective;
    [SerializeField] private Text manu_pd_InitialInvestment_title;
    [SerializeField] private Text manu_pd_InitialInvestment;
    [SerializeField] private Text manu_pd_Oportunity_Cost_title;
    [SerializeField] private Text manu_pd_Oportunity_Cost;
    [SerializeField] private Text manu_pd_Annual_Production_title;
    [SerializeField] private Text manu_pd_Annual_Production;
    [SerializeField] private Text manu_pd_unit_sell_title;
    [SerializeField] private Text manu_pd_unit_sell;
    [SerializeField] private Text manu_pd_Unit_cost_title;
    [SerializeField] private Text manu_pd_Unit_cost;
    [SerializeField] private Text manu_pd_Annual_Operating_Expenses_title;
    [SerializeField] private Text manu_pd_Annual_Operating_Expenses;
    [SerializeField] private Text manu_pd_annual_Advertising_Expenses_title;
    [SerializeField] private Text manu_pd_annual_Advertising_Expenses;

    [Header("Manufacturing Cash Flow Menu1")]
    [SerializeField] private Text manu_cf1_Title;
    [SerializeField] private Text manu_cf1_IncomingsTitle;
    [SerializeField] private Text manu_cf1_Year1_1;
    [SerializeField] private Text manu_cf1_Year1_2;
    [SerializeField] private Text manu_cf1_Year1_3;
    [SerializeField] private Text manu_cf1_SalesTitle;
    [SerializeField] private Text manu_cf1_TotalIncomingTitle;
    [SerializeField] private Text manu_cf1_TotalIncoming1;
    [SerializeField] private Text manu_cf1_TotalIncoming2;
    [SerializeField] private Text manu_cf1_TotalIncoming3;
    [SerializeField] private Text manu_cf1_expenses_title;
    [SerializeField] private Text manu_cf1_Year2_1;
    [SerializeField] private Text manu_cf1_Year2_2;
    [SerializeField] private Text manu_cf1_Year2_3;
    [SerializeField] private Text manu_cf1_ProductionCostTitle;
    [SerializeField] private Text manu_cf1_OperationalExpensesTitle;
    [SerializeField] private Text manu_cf1_Marketing_TaxExpensesTitle;
    [SerializeField] private Text manu_cf1_TotalExpensesTitle;
    [SerializeField] private Text manu_cf1_TotalExpenses1;
    [SerializeField] private Text manu_cf1_TotalExpenses2;
    [SerializeField] private Text manu_cf1_TotalExpenses3;
    [SerializeField] private Text manu_cf1_CashFlow_title;
    [SerializeField] private Text manu_cf1_CashFlow1;
    [SerializeField] private Text manu_cf1_CashFlow2;
    [SerializeField] private Text manu_cf1_CashFlow3;

    [Header("Manufacturing Cash Flow Menu2")]
    [SerializeField] private Text manu_cf2_Title;
    [SerializeField] private Text manu_cf2_IncomingsTitle;
    [SerializeField] private Text manu_cf2_Year1_1;
    [SerializeField] private Text manu_cf2_Year1_2;
    [SerializeField] private Text manu_cf2_Year1_3;
    [SerializeField] private Text manu_cf2_Year1_4;
    [SerializeField] private Text manu_cf2_SalesTitle;
    [SerializeField] private Text manu_cf2_TotalIncomingTitle;
    [SerializeField] private Text manu_cf2_TotalIncoming1;
    [SerializeField] private Text manu_cf2_TotalIncoming2;
    [SerializeField] private Text manu_cf2_TotalIncoming3;
    [SerializeField] private Text manu_cf2_TotalIncoming4;
    [SerializeField] private Text manu_cf2_expenses_title;
    [SerializeField] private Text manu_cf2_Year2_1;
    [SerializeField] private Text manu_cf2_Year2_2;
    [SerializeField] private Text manu_cf2_Year2_3;
    [SerializeField] private Text manu_cf2_Year2_4;
    [SerializeField] private Text manu_cf2_ProductionCostTitle;
    [SerializeField] private Text manu_cf2_OperationalExpensesTitle;
    [SerializeField] private Text manu_cf2_Marketing_TaxExpensesTitle;
    [SerializeField] private Text manu_cf2_TotalExpensesTitle;
    [SerializeField] private Text manu_cf2_TotalExpenses1;
    [SerializeField] private Text manu_cf2_TotalExpenses2;
    [SerializeField] private Text manu_cf2_TotalExpenses3;
    [SerializeField] private Text manu_cf2_TotalExpenses4;
    [SerializeField] private Text manu_cf2_CashFlow_title;
    [SerializeField] private Text manu_cf2_CashFlow1;
    [SerializeField] private Text manu_cf2_CashFlow2;
    [SerializeField] private Text manu_cf2_CashFlow3;
    [SerializeField] private Text manu_cf2_CashFlow4;

    [Header("Manufacturing Cash Flow Menu3")]
    [SerializeField] private Text manu_cf3_Title;
    [SerializeField] private Text manu_cf3_IncomingsTitle;
    [SerializeField] private Text manu_cf3_Year1_1;
    [SerializeField] private Text manu_cf3_Year1_2;
    [SerializeField] private Text manu_cf3_Year1_3;
    [SerializeField] private Text manu_cf3_Year1_4;
    [SerializeField] private Text manu_cf3_Year1_5;
    [SerializeField] private Text manu_cf3_SalesTitle;
    [SerializeField] private Text manu_cf3_TotalIncomingTitle;
    [SerializeField] private Text manu_cf3_TotalIncoming1;
    [SerializeField] private Text manu_cf3_TotalIncoming2;
    [SerializeField] private Text manu_cf3_TotalIncoming3;
    [SerializeField] private Text manu_cf3_TotalIncoming4;
    [SerializeField] private Text manu_cf3_TotalIncoming5;
    [SerializeField] private Text manu_cf3_expenses_title;
    [SerializeField] private Text manu_cf3_Year2_1;
    [SerializeField] private Text manu_cf3_Year2_2;
    [SerializeField] private Text manu_cf3_Year2_3;
    [SerializeField] private Text manu_cf3_Year2_4;
    [SerializeField] private Text manu_cf3_Year2_5;
    [SerializeField] private Text manu_cf3_ProductionCostTitle;
    [SerializeField] private Text manu_cf3_OperationalExpensesTitle;
    [SerializeField] private Text manu_cf3_Marketing_TaxExpensesTitle;
    [SerializeField] private Text manu_cf3_TotalExpensesTitle;
    [SerializeField] private Text manu_cf3_TotalExpenses1;
    [SerializeField] private Text manu_cf3_TotalExpenses2;
    [SerializeField] private Text manu_cf3_TotalExpenses3;
    [SerializeField] private Text manu_cf3_TotalExpenses4;
    [SerializeField] private Text manu_cf3_TotalExpenses5;
    [SerializeField] private Text manu_cf3_CashFlow_title;
    [SerializeField] private Text manu_cf3_CashFlow1;
    [SerializeField] private Text manu_cf3_CashFlow2;
    [SerializeField] private Text manu_cf3_CashFlow3;
    [SerializeField] private Text manu_cf3_CashFlow4;
    [SerializeField] private Text manu_cf3_CashFlow5;

    [Header("Finantial indicators")]
    [SerializeField] private Text manu_fi_Title;
    [SerializeField] private Text manu_fi_TirInterpolateTitle;
    [SerializeField] private Text manu_fi_MinimumRateTitle;
    [SerializeField] private Text manu_fi_MinimumRate;
    [SerializeField] private Text manu_fi_TIRTitle;
    [SerializeField] private Text manu_fi_MaximumRateTitle;
    [SerializeField] private Text manu_fi_MaximumRate;
    [SerializeField] private Text manu_fi_VANOportunity;
    [SerializeField] private Text manu_fi_VANTitle;
    [SerializeField] private Text manu_fi_Save;

    SimulatorRandomData data;

    // Start is called before the first frame update
    void Start()
    {
        data = SimulatorRandomData.instance;

        //Manufacturing Main Menu
        manu_mm_Title1.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Main_Title1']").InnerText;
        manu_mm_Title2.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Main_TitleManu']").InnerText;
        manu_mm_ButtonDescription.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Main_ButtonDescription']").InnerText;
        manu_ButtonCashFlow.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Main_ButtonCashFlow']").InnerText;
        manu_ButtonFinantialIndicators.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Main_ButtonFinantialIndicators']").InnerText;
        //

        //Manufacturing  Project Description Menu
        manu_pd_Title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_Title']").InnerText;
        manu_pd_Icon.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_Icon_Manu']").InnerText;
        manu_pd_Objective_title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_Objective_title']").InnerText;
        manu_pd_Objective.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_Objective_Manu']").InnerText;
        manu_pd_InitialInvestment_title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_InitialInvestment_title']").InnerText;
        manu_pd_InitialInvestment.text = data.manu_initialInvest.ToString();
        manu_pd_Oportunity_Cost_title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_Oportunity_Cost_title']").InnerText;
        manu_pd_Oportunity_Cost.text = data.manu_opportunityCost.ToString();
        manu_pd_Annual_Production_title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_Annual_Production_title']").InnerText;
        manu_pd_Annual_Production.text = data.manu_unitsProducedAnnually.ToString();
        manu_pd_unit_sell_title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_unit_sell_title']").InnerText;
        manu_pd_unit_sell.text = data.manu_unitSellingPrice.ToString();
        manu_pd_Unit_cost_title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_Unit_cost_title']").InnerText;
        manu_pd_Unit_cost.text = data.manu_unitCostProduction.ToString();
        manu_pd_Annual_Operating_Expenses_title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_Annual_Operating_Expenses_title']").InnerText;
        manu_pd_Annual_Operating_Expenses.text = data.manu_annualOperatingExpenses.ToString();
        manu_pd_annual_Advertising_Expenses_title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_Annual_Taxes_Marketing_expenses_title']").InnerText;
        manu_pd_annual_Advertising_Expenses.text = data.manu_annualAdvertisingExpenses.ToString();
        //

        //Manufacturing  Cash Flow Menus
        manu_cf1_Title.text = manu_cf2_Title.text = manu_cf3_Title.text
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_CashFLows_Title']").InnerText;
        manu_cf1_IncomingsTitle.text = manu_cf2_IncomingsTitle.text = manu_cf3_IncomingsTitle.text
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_IncomingsTitle']").InnerText;
        manu_cf1_SalesTitle.text = manu_cf2_SalesTitle.text = manu_cf3_SalesTitle.text
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_SalesTitleTitle']").InnerText;
        manu_cf1_TotalIncomingTitle.text = manu_cf2_TotalIncomingTitle.text = manu_cf3_TotalIncomingTitle.text
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_TotalIncomingTitle']").InnerText;
        manu_cf1_expenses_title.text = manu_cf2_expenses_title.text = manu_cf3_expenses_title.text
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_Expenses_title']").InnerText;
        manu_cf1_ProductionCostTitle.text = manu_cf2_ProductionCostTitle.text = manu_cf3_ProductionCostTitle.text
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_ProductionCostTitle']").InnerText;
        manu_cf1_OperationalExpensesTitle.text = manu_cf2_OperationalExpensesTitle.text = manu_cf3_OperationalExpensesTitle.text
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_OperationalExpensesTitle']").InnerText;
        manu_cf1_Marketing_TaxExpensesTitle.text = manu_cf2_Marketing_TaxExpensesTitle.text = manu_cf3_Marketing_TaxExpensesTitle.text
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_TaxMaketing_ExpensesTitle']").InnerText;
        manu_cf1_TotalExpensesTitle.text = manu_cf2_TotalExpensesTitle.text = manu_cf3_TotalExpensesTitle.text
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_TotalExpenses_title']").InnerText;

        manu_cf1_CashFlow_title.text = manu_cf2_CashFlow_title.text = manu_cf3_CashFlow_title.text
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_CashFlow_title']").InnerText;

        manu_cf1_Year1_1.text = manu_cf2_Year1_1.text = manu_cf3_Year1_1.text =
        manu_cf1_Year2_1.text = manu_cf2_Year2_1.text = manu_cf3_Year2_1.text =
        Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Year1']").InnerText;
        manu_cf1_Year1_2.text = manu_cf2_Year1_2.text = manu_cf3_Year1_2.text =
        manu_cf1_Year2_2.text = manu_cf2_Year2_2.text = manu_cf3_Year2_2.text =
        Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Year2']").InnerText;
        manu_cf1_Year1_3.text = manu_cf2_Year1_3.text = manu_cf3_Year1_3.text =
        manu_cf1_Year2_3.text = manu_cf2_Year2_3.text = manu_cf3_Year2_3.text =
        Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Year3']").InnerText;
        manu_cf2_Year1_4.text = manu_cf3_Year1_4.text = manu_cf2_Year2_4.text = manu_cf3_Year2_4.text =
        Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Year4']").InnerText;
        manu_cf3_Year1_5.text = manu_cf3_Year2_5.text =
        Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Year5']").InnerText;

        manu_cf1_TotalIncoming1.text = manu_cf2_TotalIncoming1.text = manu_cf3_TotalIncoming1.text =
        manu_cf1_TotalIncoming2.text = manu_cf2_TotalIncoming2.text = manu_cf3_TotalIncoming2.text =
        manu_cf1_TotalIncoming3.text = manu_cf2_TotalIncoming3.text = manu_cf3_TotalIncoming3.text =
        manu_cf2_TotalIncoming4.text = manu_cf3_TotalIncoming4.text = manu_cf3_TotalIncoming5.text =
        (data.manu_unitSellingPrice*data.manu_unitsProducedAnnually).ToString();

        manu_cf1_TotalExpenses1.text = manu_cf2_TotalExpenses1.text = manu_cf3_TotalExpenses1.text =
        manu_cf1_TotalExpenses2.text = manu_cf2_TotalExpenses2.text = manu_cf3_TotalExpenses2.text =
        manu_cf1_TotalExpenses3.text = manu_cf2_TotalExpenses3.text = manu_cf3_TotalExpenses3.text =
        manu_cf2_TotalExpenses4.text = manu_cf3_TotalExpenses4.text = manu_cf3_TotalExpenses5.text =
        (data.manu_annualOperatingExpenses + data.manu_annualAdvertisingExpenses + data.manu_unitCostProduction).ToString();

        manu_cf1_CashFlow1.text = manu_cf2_CashFlow1.text = manu_cf3_CashFlow1.text =
        manu_cf1_CashFlow2.text = manu_cf2_CashFlow2.text = manu_cf3_CashFlow2.text =
        manu_cf1_CashFlow3.text = manu_cf2_CashFlow3.text = manu_cf3_CashFlow3.text =
        manu_cf2_CashFlow4.text = manu_cf3_CashFlow4.text = manu_cf3_CashFlow5.text =
        data.manu_annualCashFlow_answer.ToString();
        //

        //Finantial indicators
        manu_fi_Title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='fi_Title']").InnerText;
        manu_fi_TirInterpolateTitle.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='fi_TirInterpolateTitle']").InnerText;
        manu_fi_MinimumRateTitle.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='fi_MinimumRateTitle']").InnerText;
        manu_fi_MinimumRate.text = data.manu_min_tir.ToString(); ;
        manu_fi_TIRTitle.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='fi_TIRTitle']").InnerText;
        manu_fi_MaximumRateTitle.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='fi_MaximumRateTitle']").InnerText;
        manu_fi_MaximumRate.text = data.manu_max_tir.ToString(); ;
        manu_fi_VANOportunity.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='fi_VANOportunity']").InnerText;
        manu_fi_VANTitle.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='fi_VANTitle']").InnerText;
        manu_fi_Save.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='fi_Save']").InnerText;
        //
    }
}
