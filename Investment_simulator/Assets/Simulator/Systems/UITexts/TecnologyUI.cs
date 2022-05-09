using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TecnologyUI : MonoBehaviour
{
    [Header("Tecnology Main Menu")]
    [SerializeField] private Text tec_mm_Title1;
    [SerializeField] private Text tec_mm_Title2;
    [SerializeField] private Text tec_mm_ButtonDescription;
    [SerializeField] private Text tec_ButtonCashFlow;
    [SerializeField] private Text tec_ButtonFinantialIndicators;

    [Header("Tecnology Project Description Menu")]
    [SerializeField] private Text tec_pd_Title;
    [SerializeField] private Text tec_pd_Icon;
    [SerializeField] private Text tec_pd_Objective_title;
    [SerializeField] private Text tec_pd_Objective;
    [SerializeField] private Text tec_pd_InitialInvestment_title;
    [SerializeField] private Text tec_pd_InitialInvestment;
    [SerializeField] private Text tec_pd_Oportunity_Cost_title;
    [SerializeField] private Text tec_pd_Oportunity_Cost;
    [SerializeField] private Text tec_pd_Annual_Sale_title;
    [SerializeField] private Text tec_pd_Annual_Sale;
    [SerializeField] private Text tec_pd_personnel_expenses_title;
    [SerializeField] private Text tec_pd_personnel_expenses;
    [SerializeField] private Text tec_pd_taxes_marketing_expenses_title;
    [SerializeField] private Text tec_pd_taxes_marketing_expenses;

    [Header("Tecnology Cash Flow Menu1")]
    [SerializeField] private Text tec_cf1_Title;
    [SerializeField] private Text tec_cf1_IncomingsTitle;
    [SerializeField] private Text tec_cf1_Year1_1;
    [SerializeField] private Text tec_cf1_Year1_2;
    [SerializeField] private Text tec_cf1_Year1_3;
    [SerializeField] private Text tec_cf1_SalesTitle;
    [SerializeField] private Text tec_cf1_TotalIncomingTitle;
    [SerializeField] private Text tec_cf1_TotalIncoming1;
    [SerializeField] private Text tec_cf1_TotalIncoming2;
    [SerializeField] private Text tec_cf1_TotalIncoming3;
    [SerializeField] private Text tec_cf1_expenses_title;
    [SerializeField] private Text tec_cf1_Year2_1;
    [SerializeField] private Text tec_cf1_Year2_2;
    [SerializeField] private Text tec_cf1_Year2_3;
    [SerializeField] private Text tec_cf1_PersonnelExpensesTitle;
    [SerializeField] private Text tec_cf1_TaxExpensesTitle;
    [SerializeField] private Text tec_cf1_TotalExpenses_title;
    [SerializeField] private Text tec_cf1_TotalExpenses1;
    [SerializeField] private Text tec_cf1_TotalExpenses2;
    [SerializeField] private Text tec_cf1_TotalExpenses3;
    [SerializeField] private Text tec_cf1_CashFlow_title;
    [SerializeField] private Text tec_cf1_CashFlow1;
    [SerializeField] private Text tec_cf1_CashFlow2;
    [SerializeField] private Text tec_cf1_CashFlow3;

    [Header("Tecnology Cash Flow Menu2")]
    [SerializeField] private Text tec_cf2_Title;
    [SerializeField] private Text tec_cf2_IncomingsTitle;
    [SerializeField] private Text tec_cf2_Year1_1;
    [SerializeField] private Text tec_cf2_Year1_2;
    [SerializeField] private Text tec_cf2_Year1_3;
    [SerializeField] private Text tec_cf2_Year1_4;
    [SerializeField] private Text tec_cf2_SalesTitle;
    [SerializeField] private Text tec_cf2_TotalIncomingTitle;
    [SerializeField] private Text tec_cf2_TotalIncoming1;
    [SerializeField] private Text tec_cf2_TotalIncoming2;
    [SerializeField] private Text tec_cf2_TotalIncoming3;
    [SerializeField] private Text tec_cf2_TotalIncoming4;
    [SerializeField] private Text tec_cf2_expenses_title;
    [SerializeField] private Text tec_cf2_Year2_1;
    [SerializeField] private Text tec_cf2_Year2_2;
    [SerializeField] private Text tec_cf2_Year2_3;
    [SerializeField] private Text tec_cf2_Year2_4;
    [SerializeField] private Text tec_cf2_PersonnelExpensesTitle;
    [SerializeField] private Text tec_cf2_TaxExpensesTitle;
    [SerializeField] private Text tec_cf2_TotalExpenses_title;
    [SerializeField] private Text tec_cf2_TotalExpenses1;
    [SerializeField] private Text tec_cf2_TotalExpenses2;
    [SerializeField] private Text tec_cf2_TotalExpenses3;
    [SerializeField] private Text tec_cf2_TotalExpenses4;
    [SerializeField] private Text tec_cf2_CashFlow_title;
    [SerializeField] private Text tec_cf2_CashFlow1;
    [SerializeField] private Text tec_cf2_CashFlow2;
    [SerializeField] private Text tec_cf2_CashFlow3;
    [SerializeField] private Text tec_cf2_CashFlow4;

    [Header("Tecnology Cash Flow Menu3")]
    [SerializeField] private Text tec_cf3_Title;
    [SerializeField] private Text tec_cf3_IncomingsTitle;
    [SerializeField] private Text tec_cf3_Year1_1;
    [SerializeField] private Text tec_cf3_Year1_2;
    [SerializeField] private Text tec_cf3_Year1_3;
    [SerializeField] private Text tec_cf3_Year1_4;
    [SerializeField] private Text tec_cf3_Year1_5;
    [SerializeField] private Text tec_cf3_SalesTitle;
    [SerializeField] private Text tec_cf3_TotalIncomingTitle;
    [SerializeField] private Text tec_cf3_TotalIncoming1;
    [SerializeField] private Text tec_cf3_TotalIncoming2;
    [SerializeField] private Text tec_cf3_TotalIncoming3;
    [SerializeField] private Text tec_cf3_TotalIncoming4;
    [SerializeField] private Text tec_cf3_TotalIncoming5;
    [SerializeField] private Text tec_cf3_expenses_title;
    [SerializeField] private Text tec_cf3_Year2_1;
    [SerializeField] private Text tec_cf3_Year2_2;
    [SerializeField] private Text tec_cf3_Year2_3;
    [SerializeField] private Text tec_cf3_Year2_4;
    [SerializeField] private Text tec_cf3_Year2_5;
    [SerializeField] private Text tec_cf3_PersonnelExpensesTitle;
    [SerializeField] private Text tec_cf3_TaxExpensesTitle;
    [SerializeField] private Text tec_cf3_TotalExpenses_title;
    [SerializeField] private Text tec_cf3_TotalExpenses1;
    [SerializeField] private Text tec_cf3_TotalExpenses2;
    [SerializeField] private Text tec_cf3_TotalExpenses3;
    [SerializeField] private Text tec_cf3_TotalExpenses4;
    [SerializeField] private Text tec_cf3_TotalExpenses5;
    [SerializeField] private Text tec_cf3_CashFlow_title;
    [SerializeField] private Text tec_cf3_CashFlow1;
    [SerializeField] private Text tec_cf3_CashFlow2;
    [SerializeField] private Text tec_cf3_CashFlow3;
    [SerializeField] private Text tec_cf3_CashFlow4;
    [SerializeField] private Text tec_cf3_CashFlow5;

    [Header("Finantial indicators")]
    [SerializeField] private Text tec_fi_Title;
    [SerializeField] private Text tec_fi_TirInterpolateTitle;
    [SerializeField] private Text tec_fi_MinimumRateTitle;
    [SerializeField] private Text tec_fi_MinimumRate;
    [SerializeField] private Text tec_fi_TIRTitle;
    [SerializeField] private Text tec_fi_MaximumRateTitle;
    [SerializeField] private Text tec_fi_MaximumRate;
    [SerializeField] private Text tec_fi_VANOportunity;
    [SerializeField] private Text tec_fi_VANTitle;
    [SerializeField] private Text tec_fi_Save;

    SimulatorRandomData data;

    // Start is called before the first frame update
    void Start()
    {
        data = SimulatorRandomData.instance;

        //Tecnology Main Menu
        tec_mm_Title1.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Main_Title1']").InnerText;
        tec_mm_Title2.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Main_TitleTec']").InnerText;
        tec_mm_ButtonDescription.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Main_ButtonDescription']").InnerText;
        tec_ButtonCashFlow.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Main_ButtonCashFlow']").InnerText;
        tec_ButtonFinantialIndicators.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Main_ButtonFinantialIndicators']").InnerText;
        //

        //Tecnology Project Description Menu
        tec_pd_Title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_Title']").InnerText;
        tec_pd_Icon.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_Icon_Tec']").InnerText;
        tec_pd_Objective_title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_Objective_title']").InnerText;
        tec_pd_Objective.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_Objective_Tec']").InnerText;
        tec_pd_InitialInvestment_title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_InitialInvestment_title']").InnerText;
        tec_pd_InitialInvestment.text = data.tec_initialInvest.ToString();
        tec_pd_Oportunity_Cost_title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_Oportunity_Cost_title']").InnerText;
        tec_pd_Oportunity_Cost.text = data.tec_opportunityCost.ToString();
        tec_pd_Annual_Sale_title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_Annual_Sale_title']").InnerText;
        tec_pd_Annual_Sale.text = data.tec_annualSales.ToString();
        tec_pd_personnel_expenses_title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_Annual_Personnel_expenses_title']").InnerText;
        tec_pd_personnel_expenses.text = data.tec_annualPersonnelExpenses.ToString();
        tec_pd_taxes_marketing_expenses_title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_Annual_Taxes_Marketing_expenses_title']").InnerText;
        tec_pd_taxes_marketing_expenses.text = data.tec_annualAdvertisingExpenses.ToString();
        //

        //Tecnology Cash Flow Menus
        tec_cf1_Title.text = tec_cf2_Title.text = tec_cf3_Title.text
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_CashFLows_Title']").InnerText;
        tec_cf1_IncomingsTitle.text = tec_cf2_IncomingsTitle.text = tec_cf3_IncomingsTitle.text
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_IncomingsTitle']").InnerText;
        tec_cf1_SalesTitle.text = tec_cf2_SalesTitle.text = tec_cf3_SalesTitle.text
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_SalesTitleTitle']").InnerText;
        tec_cf1_TotalIncomingTitle.text = tec_cf2_TotalIncomingTitle.text = tec_cf3_TotalIncomingTitle.text
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_TotalIncomingTitle']").InnerText;
        tec_cf1_expenses_title.text = tec_cf2_expenses_title.text = tec_cf3_expenses_title.text
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_Expenses_title']").InnerText;
        tec_cf1_PersonnelExpensesTitle.text = tec_cf2_PersonnelExpensesTitle.text = tec_cf3_PersonnelExpensesTitle.text
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_PersonnelExpensesTitle']").InnerText;
        tec_cf1_TaxExpensesTitle.text = tec_cf2_TaxExpensesTitle.text = tec_cf3_TaxExpensesTitle.text
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_TaxMaketing_ExpensesTitle']").InnerText;
        tec_cf1_TotalExpenses_title.text = tec_cf2_TotalExpenses_title.text = tec_cf3_TotalExpenses_title.text
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_TotalExpenses_title']").InnerText;
        tec_cf1_CashFlow_title.text = tec_cf2_CashFlow_title.text = tec_cf3_CashFlow_title.text
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_CashFlow_title']").InnerText;

        tec_cf1_Year1_1.text = tec_cf2_Year1_1.text = tec_cf3_Year1_1.text =
        tec_cf1_Year2_1.text = tec_cf2_Year2_1.text = tec_cf3_Year2_1.text =
        Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Year1']").InnerText;
        tec_cf1_Year1_2.text = tec_cf2_Year1_2.text = tec_cf3_Year1_2.text =
        tec_cf1_Year2_2.text = tec_cf2_Year2_2.text = tec_cf3_Year2_2.text =
        Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Year2']").InnerText;
        tec_cf1_Year1_3.text = tec_cf2_Year1_3.text = tec_cf3_Year1_3.text =
        tec_cf1_Year2_3.text = tec_cf2_Year2_3.text = tec_cf3_Year2_3.text =
        Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Year3']").InnerText;
        tec_cf2_Year1_4.text = tec_cf3_Year1_4.text = tec_cf2_Year2_4.text = tec_cf3_Year2_4.text =
        Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Year4']").InnerText;
        tec_cf3_Year1_5.text = tec_cf3_Year2_5.text =
        Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Year5']").InnerText;

        tec_cf1_TotalIncoming1.text = tec_cf2_TotalIncoming1.text = tec_cf3_TotalIncoming1.text =
        tec_cf1_TotalIncoming2.text = tec_cf2_TotalIncoming2.text = tec_cf3_TotalIncoming2.text =
        tec_cf1_TotalIncoming3.text = tec_cf2_TotalIncoming3.text = tec_cf3_TotalIncoming3.text =
        tec_cf2_TotalIncoming4.text = tec_cf3_TotalIncoming4.text = tec_cf3_TotalIncoming5.text =
        (data.tec_annualSales).ToString();

        tec_cf1_TotalExpenses1.text = tec_cf2_TotalExpenses1.text = tec_cf3_TotalExpenses1.text =
        tec_cf1_TotalExpenses2.text = tec_cf2_TotalExpenses2.text = tec_cf3_TotalExpenses2.text =
        tec_cf1_TotalExpenses3.text = tec_cf2_TotalExpenses3.text = tec_cf3_TotalExpenses3.text =
        tec_cf2_TotalExpenses4.text = tec_cf3_TotalExpenses4.text = tec_cf3_TotalExpenses5.text =
        (data.tec_annualPersonnelExpenses + data.tec_annualAdvertisingExpenses).ToString();

        tec_cf1_CashFlow1.text = tec_cf2_CashFlow1.text = tec_cf3_CashFlow1.text =
        tec_cf1_CashFlow2.text = tec_cf2_CashFlow2.text = tec_cf3_CashFlow2.text =
        tec_cf1_CashFlow3.text = tec_cf2_CashFlow3.text = tec_cf3_CashFlow3.text =
        tec_cf2_CashFlow4.text = tec_cf3_CashFlow4.text = tec_cf3_CashFlow5.text =
        data.tec_annualCashFlow_answer.ToString();
        //

        //Finantial indicators
        tec_fi_Title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='fi_Title']").InnerText;
        tec_fi_TirInterpolateTitle.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='fi_TirInterpolateTitle']").InnerText;
        tec_fi_MinimumRateTitle.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='fi_MinimumRateTitle']").InnerText;
        tec_fi_MinimumRate.text = data.tec_min_tir.ToString(); ;
        tec_fi_TIRTitle.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='fi_TIRTitle']").InnerText;
        tec_fi_MaximumRateTitle.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='fi_MaximumRateTitle']").InnerText;
        tec_fi_MaximumRate.text = data.tec_max_tir.ToString(); ;
        tec_fi_VANOportunity.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='fi_VANOportunity']").InnerText;
        tec_fi_VANTitle.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='fi_VANTitle']").InnerText;
        tec_fi_Save.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='fi_Save']").InnerText;
        //
    }
}
