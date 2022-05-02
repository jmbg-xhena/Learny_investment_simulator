using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenusUI : MonoBehaviour
{
    [Header("Main Map")]
    [SerializeField] private Text mm_title;
    [SerializeField] private Text mm_technology;
    [SerializeField] private Text mm_realState;
    [SerializeField] private Text mm_manufacturing;
    [SerializeField] private Text mm_financial;
    [SerializeField] private Text mm_finalInvestment;

    [Header("Real State Main Menu")]
    [SerializeField] private Text rS_mm_Title1;
    [SerializeField] private Text rS_mm_Title2;
    [SerializeField] private Text rS_mm_ButtonDescription;
    [SerializeField] private Text rS_ButtonCashFlow;
    [SerializeField] private Text rS_ButtonFinantialIndicators;

    [Header("Real State Project Description Menu")]
    [SerializeField] private Text rS_pd_Title;
    [SerializeField] private Text rS_pd_Icon;
    [SerializeField] private Text rS_pd_Objective_title;
    [SerializeField] private Text rS_pd_Objective;
    [SerializeField] private Text rS_pd_InitialInvestment_title;
    [SerializeField] private Text rS_pd_InitialInvestment;
    [SerializeField] private Text rS_pd_Oportunity_Cost_title;
    [SerializeField] private Text rS_pd_Oportunity_Cost;
    [SerializeField] private Text rS_pd_Annual_Lease_title;
    [SerializeField] private Text rS_pd_Annual_Lease;
    [SerializeField] private Text rS_pd_apartment_sell_title;
    [SerializeField] private Text rS_pd_apartment_sell;
    [SerializeField] private Text rS_pd_apartment_AnnualMaintenance_title;
    [SerializeField] private Text rS_pd_apartment_AnnualMaintenance;
    [SerializeField] private Text rS_pd_apartment_AnnualTaxes_title;
    [SerializeField] private Text rS_pd_apartment_AnnualTaxes;

    [Header("Real State Cash Flow Menu1")]
    [SerializeField] private Text rS_cf1_Title;
    [SerializeField] private Text rS_cf1_IncomingsTitle;
    [SerializeField] private Text rS_cf1_Year1_1;
    [SerializeField] private Text rS_cf1_Year1_2;
    [SerializeField] private Text rS_cf1_Year1_3;
    [SerializeField] private Text rS_cf1_leaseTitle;
    [SerializeField] private Text rS_cf1_SalePriceTitle;
    [SerializeField] private Text rS_cf1_TotalIncomingTitle;
    [SerializeField] private Text rS_cf1_TotalIncoming1;
    [SerializeField] private Text rS_cf1_TotalIncoming2;
    [SerializeField] private Text rS_cf1_TotalIncoming3;
    [SerializeField] private Text rS_cf1_expenses_title;
    [SerializeField] private Text rS_cf1_Year2_1;
    [SerializeField] private Text rS_cf1_Year2_2;
    [SerializeField] private Text rS_cf1_Year2_3;
    [SerializeField] private Text rS_cf1_MaintenanceExpensesTitle;
    [SerializeField] private Text rS_cf1_TaxExpensesTitle;
    [SerializeField] private Text rS_cf1_TotalExpenses_title;
    [SerializeField] private Text rS_cf1_TotalExpenses1;
    [SerializeField] private Text rS_cf1_TotalExpenses2;
    [SerializeField] private Text rS_cf1_TotalExpenses3;
    [SerializeField] private Text rS_cf1_CashFlow_title;
    [SerializeField] private Text rS_cf1_CashFlow1;
    [SerializeField] private Text rS_cf1_CashFlow2;
    [SerializeField] private Text rS_cf1_CashFlow3;

    [Header("Real State Cash Flow Menu2")]
    [SerializeField] private Text rS_cf2_Title;
    [SerializeField] private Text rS_cf2_IncomingsTitle;
    [SerializeField] private Text rS_cf2_Year1_1;
    [SerializeField] private Text rS_cf2_Year1_2;
    [SerializeField] private Text rS_cf2_Year1_3;
    [SerializeField] private Text rS_cf2_Year1_4;
    [SerializeField] private Text rS_cf2_leaseTitle;
    [SerializeField] private Text rS_cf2_SalePriceTitle;
    [SerializeField] private Text rS_cf2_TotalIncomingTitle;
    [SerializeField] private Text rS_cf2_TotalIncoming1;
    [SerializeField] private Text rS_cf2_TotalIncoming2;
    [SerializeField] private Text rS_cf2_TotalIncoming3;
    [SerializeField] private Text rS_cf2_TotalIncoming4;
    [SerializeField] private Text rS_cf2_expenses_title;
    [SerializeField] private Text rS_cf2_Year2_1;
    [SerializeField] private Text rS_cf2_Year2_2;
    [SerializeField] private Text rS_cf2_Year2_3;
    [SerializeField] private Text rS_cf2_Year2_4;
    [SerializeField] private Text rS_cf2_MaintenanceExpensesTitle;
    [SerializeField] private Text rS_cf2_TaxExpensesTitle;
    [SerializeField] private Text rS_cf2_TotalExpenses_title;
    [SerializeField] private Text rS_cf2_TotalExpenses1;
    [SerializeField] private Text rS_cf2_TotalExpenses2;
    [SerializeField] private Text rS_cf2_TotalExpenses3;
    [SerializeField] private Text rS_cf2_TotalExpenses4;
    [SerializeField] private Text rS_cf2_CashFlow_title;
    [SerializeField] private Text rS_cf2_CashFlow1;
    [SerializeField] private Text rS_cf2_CashFlow2;
    [SerializeField] private Text rS_cf2_CashFlow3;
    [SerializeField] private Text rS_cf2_CashFlow4;

    [Header("Real State Cash Flow Menu3")]
    [SerializeField] private Text rS_cf3_Title;
    [SerializeField] private Text rS_cf3_IncomingsTitle;
    [SerializeField] private Text rS_cf3_Year1_1;
    [SerializeField] private Text rS_cf3_Year1_2;
    [SerializeField] private Text rS_cf3_Year1_3;
    [SerializeField] private Text rS_cf3_Year1_4;
    [SerializeField] private Text rS_cf3_Year1_5;
    [SerializeField] private Text rS_cf3_leaseTitle;
    [SerializeField] private Text rS_cf3_SalePriceTitle;
    [SerializeField] private Text rS_cf3_TotalIncomingTitle;
    [SerializeField] private Text rS_cf3_TotalIncoming1;
    [SerializeField] private Text rS_cf3_TotalIncoming2;
    [SerializeField] private Text rS_cf3_TotalIncoming3;
    [SerializeField] private Text rS_cf3_TotalIncoming4;
    [SerializeField] private Text rS_cf3_TotalIncoming5;
    [SerializeField] private Text rS_cf3_expenses_title;
    [SerializeField] private Text rS_cf3_Year2_1;
    [SerializeField] private Text rS_cf3_Year2_2;
    [SerializeField] private Text rS_cf3_Year2_3;
    [SerializeField] private Text rS_cf3_Year2_4;
    [SerializeField] private Text rS_cf3_Year2_5;
    [SerializeField] private Text rS_cf3_MaintenanceExpensesTitle;
    [SerializeField] private Text rS_cf3_TaxExpensesTitle;
    [SerializeField] private Text rS_cf3_TotalExpenses_title;
    [SerializeField] private Text rS_cf3_TotalExpenses1;
    [SerializeField] private Text rS_cf3_TotalExpenses2;
    [SerializeField] private Text rS_cf3_TotalExpenses3;
    [SerializeField] private Text rS_cf3_TotalExpenses4;
    [SerializeField] private Text rS_cf3_TotalExpenses5;
    [SerializeField] private Text rS_cf3_CashFlow_title;
    [SerializeField] private Text rS_cf3_CashFlow1;
    [SerializeField] private Text rS_cf3_CashFlow2;
    [SerializeField] private Text rS_cf3_CashFlow3;
    [SerializeField] private Text rS_cf3_CashFlow4;
    [SerializeField] private Text rS_cf3_CashFlow5;

    [Header("Finantial indicators")]
    [SerializeField] private Text rS_fi_Title;
    [SerializeField] private Text rS_fi_TirInterpolateTitle;
    [SerializeField] private Text rS_fi_MinimumRateTitle;
    [SerializeField] private Text rS_fi_MinimumRate;
    [SerializeField] private Text rS_fi_TIRTitle;
    [SerializeField] private Text rS_fi_MaximumRateTitle;
    [SerializeField] private Text rS_fi_MaximumRate;
    [SerializeField] private Text rS_fi_VANOportunity;
    [SerializeField] private Text rS_fi_VANTitle;
    [SerializeField] private Text rS_fi_Save;


    SimulatorRandomData data;

    // Start is called before the first frame update
    private void Start()
    {
        data = SimulatorRandomData.instance;

        //Main Map
        mm_title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Main_Map_Title']").InnerText;
        mm_technology.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Main_Map_Tech']").InnerText;
        mm_realState.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Main_Map_RS']").InnerText;
        mm_manufacturing.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Main_Map_Manu']").InnerText;
        mm_financial.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Main_Map_Fin']").InnerText;
        mm_finalInvestment.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Main_Map_EndInv']").InnerText;
        //

        //Real state Main Menu
        rS_mm_Title1.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Main_Title1']").InnerText;
        rS_mm_Title2.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Main_Title2']").InnerText;
        rS_mm_ButtonDescription.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Main_ButtonDescription']").InnerText;
        rS_ButtonCashFlow.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Main_ButtonCashFlow']").InnerText;
        rS_ButtonFinantialIndicators.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Main_ButtonFinantialIndicators']").InnerText;
        //

        //Real State Project Description Menu
        rS_pd_Title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_Title']").InnerText;
        rS_pd_Icon.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_Icon']").InnerText;
        rS_pd_Objective_title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_Objective_title']").InnerText;
        rS_pd_Objective.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_Title']").InnerText;
        rS_pd_InitialInvestment_title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_InitialInvestment_title']").InnerText;
        rS_pd_InitialInvestment.text = data.rS_initialInvest.ToString();
        rS_pd_Oportunity_Cost_title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_Oportunity_Cost_title']").InnerText;
        rS_pd_Oportunity_Cost.text = data.rS_opportunityCost.ToString();
        rS_pd_Annual_Lease_title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_Annual_Lease_title']").InnerText;
        rS_pd_Annual_Lease.text = data.rS_AnnualLease.ToString();
        rS_pd_apartment_sell_title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_Apartment_sell_title']").InnerText;
        rS_pd_apartment_sell.text = data.rS_salePrice.ToString();
        rS_pd_apartment_AnnualMaintenance_title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_Apartment_AnnualMaintenance_title']").InnerText;
        rS_pd_apartment_AnnualMaintenance.text = data.rS_annualMaintenanceExpenses.ToString();
        rS_pd_apartment_AnnualTaxes_title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_Apartment_AnnualTaxes_title']").InnerText;
        rS_pd_apartment_AnnualTaxes.text = data.rS_annualTaxExpense.ToString();
        //

        //Real State Cash Flow Menus
        rS_cf1_Title.text = rS_cf2_Title.text = rS_cf3_Title.text 
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_CashFLows_Title']").InnerText;
        rS_cf1_IncomingsTitle.text = rS_cf2_IncomingsTitle.text = rS_cf3_IncomingsTitle.text
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_IncomingsTitle']").InnerText;
        rS_cf1_leaseTitle.text = rS_cf2_leaseTitle.text = rS_cf3_leaseTitle.text 
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_LeaseTitle']").InnerText;
        rS_cf1_SalePriceTitle.text = rS_cf2_SalePriceTitle.text = rS_cf3_SalePriceTitle.text 
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_SalePriceTitle']").InnerText;
        rS_cf1_TotalIncomingTitle.text = rS_cf2_TotalIncomingTitle.text = rS_cf3_TotalIncomingTitle.text
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_TotalIncomingTitle']").InnerText;
        rS_cf1_expenses_title.text = rS_cf2_expenses_title.text = rS_cf3_expenses_title.text
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_Expenses_title']").InnerText;
        rS_cf1_MaintenanceExpensesTitle.text = rS_cf2_MaintenanceExpensesTitle.text = rS_cf3_MaintenanceExpensesTitle.text
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_MaintenanceExpensesTitle']").InnerText;
        rS_cf1_TaxExpensesTitle.text = rS_cf2_TaxExpensesTitle.text = rS_cf3_TaxExpensesTitle.text
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_TaxExpensesTitle']").InnerText;
        rS_cf1_TotalExpenses_title.text = rS_cf2_TotalExpenses_title.text = rS_cf3_TotalExpenses_title.text
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_TotalExpenses_title']").InnerText;
        rS_cf1_CashFlow_title.text = rS_cf2_CashFlow_title.text = rS_cf3_CashFlow_title.text
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_CashFlow_title']").InnerText;

        rS_cf1_Year1_1.text = rS_cf2_Year1_1.text = rS_cf3_Year1_1.text = 
        rS_cf1_Year2_1.text = rS_cf2_Year2_1.text = rS_cf3_Year2_1.text =
        Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Year1']").InnerText;
        rS_cf1_Year1_2.text = rS_cf2_Year1_2.text = rS_cf3_Year1_2.text =
        rS_cf1_Year2_2.text = rS_cf2_Year2_2.text = rS_cf3_Year2_2.text =
        Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Year2']").InnerText;
        rS_cf1_Year1_3.text = rS_cf2_Year1_3.text = rS_cf3_Year1_3.text = 
        rS_cf1_Year2_3.text = rS_cf2_Year2_3.text = rS_cf3_Year2_3.text =
        Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Year3']").InnerText;
        rS_cf2_Year1_4.text = rS_cf3_Year1_4.text = rS_cf2_Year2_4.text = rS_cf3_Year2_4.text =
        Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Year4']").InnerText;
        rS_cf3_Year1_5.text = rS_cf3_Year2_5.text =
        Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Year5']").InnerText;

        rS_cf1_TotalIncoming1.text = rS_cf2_TotalIncoming1.text = rS_cf3_TotalIncoming1.text =
        rS_cf1_TotalIncoming2.text = rS_cf2_TotalIncoming2.text = rS_cf3_TotalIncoming2.text =
        rS_cf1_TotalIncoming3.text = rS_cf2_TotalIncoming3.text = rS_cf3_TotalIncoming3.text =
        rS_cf2_TotalIncoming4.text = rS_cf3_TotalIncoming4.text = rS_cf3_TotalIncoming5.text = 
        ((data.rS_salePrice + data.rS_AnnualLease) / data.expectedProfitTime).ToString();

        rS_cf1_TotalExpenses1.text = rS_cf2_TotalExpenses1.text = rS_cf3_TotalExpenses1.text =
        rS_cf1_TotalExpenses2.text = rS_cf2_TotalExpenses2.text = rS_cf3_TotalExpenses2.text =
        rS_cf1_TotalExpenses3.text = rS_cf2_TotalExpenses3.text = rS_cf3_TotalExpenses3.text =
        rS_cf2_TotalExpenses4.text = rS_cf3_TotalExpenses4.text = rS_cf3_TotalExpenses5.text =
        ((data.rS_annualMaintenanceExpenses + data.rS_annualTaxExpense) / data.expectedProfitTime).ToString();
        
        rS_cf1_CashFlow1.text = rS_cf2_CashFlow1.text = rS_cf3_CashFlow1.text =
        rS_cf1_CashFlow2.text = rS_cf2_CashFlow2.text = rS_cf3_CashFlow2.text =
        rS_cf1_CashFlow3.text = rS_cf2_CashFlow3.text = rS_cf3_CashFlow3.text =
        rS_cf2_CashFlow4.text = rS_cf3_CashFlow4.text = rS_cf3_CashFlow5.text =
        (data.rS_annualCashFlow / data.expectedProfitTime).ToString();
        //

        //Finantial indicators
        rS_fi_Title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='fi_Title']").InnerText;
        rS_fi_TirInterpolateTitle.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='fi_TirInterpolateTitle']").InnerText;
        rS_fi_MinimumRateTitle.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='fi_MinimumRateTitle']").InnerText;
        rS_fi_MinimumRate.text = "0";
        rS_fi_TIRTitle.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='fi_TIRTitle']").InnerText;
        rS_fi_MaximumRateTitle.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='fi_MaximumRateTitle']").InnerText;
        rS_fi_MaximumRate.text = "0";
        rS_fi_VANOportunity.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='fi_VANOportunity']").InnerText;
        rS_fi_VANTitle.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='fi_VANTitle']").InnerText;
        rS_fi_Save.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='fi_Save']").InnerText;
        //
    }
}
