using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulatorController : MonoBehaviour
{
    [Header("Scriptable Events")]
    [SerializeField] private StateRequestGameEvent onStateChangeRequest;

    [Header("Simulator References")]
    [SerializeField] private GameObject expoEntrance;

    [Header("Over References")]
    [SerializeField] private Sprite expoEntrance_over;

    private string vAN;
    private string tIR;

    void Start()
    {
        onStateChangeRequest.Raise(new StateRequest(State.ExpoEntranace, expoEntrance.transform.Find("Over_container").gameObject, expoEntrance_over));
    }


    public void setVAN(Text _VAN) {
        vAN = _VAN.text;
    }

    public void setTIR(Text _TIR)
    {
        tIR = _TIR.text;
    }

    public void checkVAN_TIR(int project) {
        if (vAN == "" || tIR == "") {
            IncompleteInputs();
            return;
        }

        if ((SimulatorRandomData.Projects)project == SimulatorRandomData.Projects.Technology) {
            if (vAN == SimulatorRandomData.instance.tec_VAN.ToString())
            {
                print("VAN is correct");
            }
            else {
                print("VAN is incorrect");
            }

            if (tIR == SimulatorRandomData.instance.tec_TIR.ToString())
            {
                print("TIR is correct");
            }
            else
            {
                print("TIR is incorrect");
            }
        }

        if ((SimulatorRandomData.Projects)project == SimulatorRandomData.Projects.RealEstate)
        {
            if (vAN == SimulatorRandomData.instance.rS_VAN.ToString())
            {
                print("VAN is correct");
            }
            else
            {
                print("VAN is incorrect");
            }
            if (tIR == SimulatorRandomData.instance.rS_TIR.ToString())
            {
                print("TIR is correct");
            }
            else
            {
                print("TIR is incorrect");
            }
        }

        if ((SimulatorRandomData.Projects)project == SimulatorRandomData.Projects.Manufacturing)
        {
            if (vAN == SimulatorRandomData.instance.manu_VAN.ToString())
            {
                print("VAN is correct");
            }
            else
            {
                print("VAN is incorrect");
            }
            if (tIR == SimulatorRandomData.instance.manu_TIR.ToString())
            {
                print("TIR is correct");
            }
            else
            {
                print("TIR is incorrect");
            }
        }

        if ((SimulatorRandomData.Projects)project == SimulatorRandomData.Projects.Financial)
        {
            if (vAN == SimulatorRandomData.instance.fin_VAN.ToString())
            {
                print("VAN is correct");
            }
            else
            {
                print("VAN is incorrect");
            }
            if (tIR == SimulatorRandomData.instance.fin_TIR.ToString())
            {
                print("TIR is correct");
            }
            else
            {
                print("TIR is incorrect");
            }
        }
    }

    private void IncompleteInputs()
    {
        print("faltan datos por introducir");
    }
}
