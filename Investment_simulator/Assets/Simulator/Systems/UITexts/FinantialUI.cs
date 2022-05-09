using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinantialUI : MonoBehaviour
{
    [Header("Financial Main Menu")]
    [SerializeField] private Text fin_mm_Title1;
    [SerializeField] private Text fin_mm_Title2;
    [SerializeField] private Text fin_mm_ButtonDescription;
    [SerializeField] private Text fin_ButtonCashFlow;
    [SerializeField] private Text fin_ButtonFinantialIndicators;

    [Header("Financial Project Description Menu")]
    [SerializeField] private Text fin_pd_Title;
    [SerializeField] private Text fin_pd_Icon;
    [SerializeField] private Text fin_pd_Objective_title;
    [SerializeField] private Text fin_pd_Objective;
    [SerializeField] private Text fin_pd_InitialInvestment_title;
    [SerializeField] private Text fin_pd_InitialInvestment;
    [SerializeField] private Text fin_pd_Oportunity_Cost_title;
    [SerializeField] private Text fin_pd_Oportunity_Cost;
    [SerializeField] private Text fin_pd_Annual_Production_title;
    [SerializeField] private Text fin_pd_Annual_Production;

    [Header("Financial Cash Flow Menu1")]
    [SerializeField] private Text fin_cf1_Title;
    [SerializeField] private Text fin_cf1_IncomingsTitle;
    [SerializeField] private Text fin_cf1_Year1_1;
    [SerializeField] private Text fin_cf1_Year1_2;
    [SerializeField] private Text fin_cf1_Year1_3;
    [SerializeField] private Text fin_cf1_sharesProduction;
    [SerializeField] private Text fin_cf1_TotalIncomingTitle;
    [SerializeField] private Text fin_cf1_TotalIncoming1;
    [SerializeField] private Text fin_cf1_TotalIncoming2;
    [SerializeField] private Text fin_cf1_TotalIncoming3;
    [SerializeField] private Text fin_cf1_TotalExpenses_title;
    [SerializeField] private Text fin_cf1_TotalExpenses1;
    [SerializeField] private Text fin_cf1_TotalExpenses2;
    [SerializeField] private Text fin_cf1_TotalExpenses3;
    [SerializeField] private Text fin_cf1_CashFlow_title;
    [SerializeField] private Text fin_cf1_CashFlow1;
    [SerializeField] private Text fin_cf1_CashFlow2;
    [SerializeField] private Text fin_cf1_CashFlow3;

    [Header("Financial Cash Flow Menu2")]
    [SerializeField] private Text fin_cf2_Title;
    [SerializeField] private Text fin_cf2_IncomingsTitle;
    [SerializeField] private Text fin_cf2_Year1_1;
    [SerializeField] private Text fin_cf2_Year1_2;
    [SerializeField] private Text fin_cf2_Year1_3;
    [SerializeField] private Text fin_cf2_Year1_4;
    [SerializeField] private Text fin_cf2_sharesProduction;
    [SerializeField] private Text fin_cf2_TotalIncomingTitle;
    [SerializeField] private Text fin_cf2_TotalIncoming1;
    [SerializeField] private Text fin_cf2_TotalIncoming2;
    [SerializeField] private Text fin_cf2_TotalIncoming3;
    [SerializeField] private Text fin_cf2_TotalIncoming4;
    [SerializeField] private Text fin_cf2_TotalExpenses_title;
    [SerializeField] private Text fin_cf2_TotalExpenses1;
    [SerializeField] private Text fin_cf2_TotalExpenses2;
    [SerializeField] private Text fin_cf2_TotalExpenses3;
    [SerializeField] private Text fin_cf2_TotalExpenses4;
    [SerializeField] private Text fin_cf2_CashFlow_title;
    [SerializeField] private Text fin_cf2_CashFlow1;
    [SerializeField] private Text fin_cf2_CashFlow2;
    [SerializeField] private Text fin_cf2_CashFlow3;
    [SerializeField] private Text fin_cf2_CashFlow4;

    [Header("Financial Cash Flow Menu2")]
    [SerializeField] private Text fin_cf3_Title;
    [SerializeField] private Text fin_cf3_IncomingsTitle;
    [SerializeField] private Text fin_cf3_Year1_1;
    [SerializeField] private Text fin_cf3_Year1_2;
    [SerializeField] private Text fin_cf3_Year1_3;
    [SerializeField] private Text fin_cf3_Year1_4;
    [SerializeField] private Text fin_cf3_Year1_5;
    [SerializeField] private Text fin_cf3_sharesProduction;
    [SerializeField] private Text fin_cf3_TotalIncomingTitle;
    [SerializeField] private Text fin_cf3_TotalIncoming1;
    [SerializeField] private Text fin_cf3_TotalIncoming2;
    [SerializeField] private Text fin_cf3_TotalIncoming3;
    [SerializeField] private Text fin_cf3_TotalIncoming4;
    [SerializeField] private Text fin_cf3_TotalIncoming5;
    [SerializeField] private Text fin_cf3_TotalExpenses_title;
    [SerializeField] private Text fin_cf3_TotalExpenses1;
    [SerializeField] private Text fin_cf3_TotalExpenses2;
    [SerializeField] private Text fin_cf3_TotalExpenses3;
    [SerializeField] private Text fin_cf3_TotalExpenses4;
    [SerializeField] private Text fin_cf3_TotalExpenses5;
    [SerializeField] private Text fin_cf3_CashFlow_title;
    [SerializeField] private Text fin_cf3_CashFlow1;
    [SerializeField] private Text fin_cf3_CashFlow2;
    [SerializeField] private Text fin_cf3_CashFlow3;
    [SerializeField] private Text fin_cf3_CashFlow4;
    [SerializeField] private Text fin_cf3_CashFlow5;

    [Header("Finantial indicators")]
    [SerializeField] private Text fin_fi_Title;
    [SerializeField] private Text fin_fi_TirInterpolateTitle;
    [SerializeField] private Text fin_fi_MinimumRateTitle;
    [SerializeField] private Text fin_fi_MinimumRate;
    [SerializeField] private Text fin_fi_TIRTitle;
    [SerializeField] private Text fin_fi_MaximumRateTitle;
    [SerializeField] private Text fin_fi_MaximumRate;
    [SerializeField] private Text fin_fi_VANOportunity;
    [SerializeField] private Text fin_fi_VANTitle;
    [SerializeField] private Text fin_fi_Save;


    SimulatorRandomData data;
    // Start is called before the first frame update
    void Start()
    {
        data = SimulatorRandomData.instance;

        //Financial Main Menu
        fin_mm_Title1.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Main_Title1']").InnerText;
        fin_mm_Title2.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Main_TitleFin']").InnerText;
        fin_mm_ButtonDescription.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Main_ButtonDescription']").InnerText;
        fin_ButtonCashFlow.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Main_ButtonCashFlow']").InnerText;
        fin_ButtonFinantialIndicators.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Main_ButtonFinantialIndicators']").InnerText;
        //

        //Financial Project Description Menu
        fin_pd_Title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_Title']").InnerText;
        fin_pd_Icon.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_Icon_Fin']").InnerText;
        fin_pd_Objective_title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_Objective_title']").InnerText;
        fin_pd_Objective.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_Objective_Fin']").InnerText;
        fin_pd_InitialInvestment_title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_InitialInvestment_title']").InnerText;
        fin_pd_InitialInvestment.text = data.fin_initialInvest.ToString();
        fin_pd_Oportunity_Cost_title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_Oportunity_Cost_title']").InnerText;
        fin_pd_Oportunity_Cost.text = data.fin_opportunityCost.ToString();
        fin_pd_Annual_Production_title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='pd_Annual_Production_Fin_title']").InnerText;
        fin_pd_Annual_Production.text = data.fin_annualProduction.ToString();
        //


        //Financial Cash Flow Menus
        fin_cf1_Title.text = fin_cf2_Title.text = fin_cf3_Title.text
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_CashFLows_Title']").InnerText;
        fin_cf1_IncomingsTitle.text = fin_cf2_IncomingsTitle.text = fin_cf3_IncomingsTitle.text
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_IncomingsTitle']").InnerText;
        fin_cf1_sharesProduction.text = fin_cf2_sharesProduction.text = fin_cf3_sharesProduction.text
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_SalesTitleTitle']").InnerText;
        fin_cf1_TotalIncomingTitle.text = fin_cf2_TotalIncomingTitle.text = fin_cf3_TotalIncomingTitle.text
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_TotalIncomingTitle']").InnerText;
        fin_cf1_TotalExpenses_title.text = fin_cf2_TotalExpenses_title.text = fin_cf3_TotalExpenses_title.text
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_TotalExpenses_title']").InnerText;
        fin_cf1_CashFlow_title.text = fin_cf2_CashFlow_title.text = fin_cf3_CashFlow_title.text
        = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='cf_CashFlow_title']").InnerText;

        fin_cf1_Year1_1.text = fin_cf2_Year1_1.text = fin_cf3_Year1_1.text =
        Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Year1']").InnerText;
        fin_cf1_Year1_2.text = fin_cf2_Year1_2.text = fin_cf3_Year1_2.text =
        Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Year2']").InnerText;
        fin_cf1_Year1_3.text = fin_cf2_Year1_3.text = fin_cf3_Year1_3.text =
        Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Year3']").InnerText;
        fin_cf2_Year1_4.text = fin_cf3_Year1_4.text =
        Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Year4']").InnerText;
        fin_cf3_Year1_5.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Year5']").InnerText;

        fin_cf1_TotalIncoming1.text = fin_cf2_TotalIncoming1.text = fin_cf3_TotalIncoming1.text =
        fin_cf1_TotalIncoming2.text = fin_cf2_TotalIncoming2.text = fin_cf3_TotalIncoming2.text =
        fin_cf1_TotalIncoming3.text = fin_cf2_TotalIncoming3.text = fin_cf3_TotalIncoming3.text =
        fin_cf2_TotalIncoming4.text = fin_cf3_TotalIncoming4.text = fin_cf3_TotalIncoming5.text =
        data.fin_annualProduction.ToString();

        fin_cf1_TotalExpenses1.text = fin_cf2_TotalExpenses1.text = fin_cf3_TotalExpenses1.text =
        fin_cf1_TotalExpenses2.text = fin_cf2_TotalExpenses2.text = fin_cf3_TotalExpenses2.text =
        fin_cf1_TotalExpenses3.text = fin_cf2_TotalExpenses3.text = fin_cf3_TotalExpenses3.text =
        fin_cf2_TotalExpenses4.text = fin_cf3_TotalExpenses4.text = fin_cf3_TotalExpenses5.text =
        data.fin_expenses.ToString();

        fin_cf1_CashFlow1.text = fin_cf2_CashFlow1.text = fin_cf3_CashFlow1.text =
        fin_cf1_CashFlow2.text = fin_cf2_CashFlow2.text = fin_cf3_CashFlow2.text =
        fin_cf1_CashFlow3.text = fin_cf2_CashFlow3.text = fin_cf3_CashFlow3.text =
        fin_cf2_CashFlow4.text = fin_cf3_CashFlow4.text = fin_cf3_CashFlow5.text =
        data.fin_annualCashFlow_answer.ToString();
        //

        //Finantial indicators
        fin_fi_Title.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='fi_Title']").InnerText;
        fin_fi_TirInterpolateTitle.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='fi_TirInterpolateTitle']").InnerText;
        fin_fi_MinimumRateTitle.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='fi_MinimumRateTitle']").InnerText;
        fin_fi_MinimumRate.text = data.fin_min_tir.ToString(); ;
        fin_fi_TIRTitle.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='fi_TIRTitle']").InnerText;
        fin_fi_MaximumRateTitle.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='fi_MaximumRateTitle']").InnerText;
        fin_fi_MaximumRate.text = data.fin_max_tir.ToString(); ;
        fin_fi_VANOportunity.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='fi_VANOportunity']").InnerText;
        fin_fi_VANTitle.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='fi_VANTitle']").InnerText;
        fin_fi_Save.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='fi_Save']").InnerText;
        //
    }
}
