using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonJoinGame : BaseButtonController
{
    [SerializeField]
    private InputField inputFieldAddres;

    protected override void ButtonClicked()
    {
        GameNetworkManager.singleton.networkAddress = inputFieldAddres.text;
        GameNetworkManager.singleton.StartClient();
    }

    protected override void Awake()
    {
        base.Awake();
        inputFieldAddres.text = GameNetworkManager.singleton.networkAddress + ":" + GameNetworkManager.singleton.networkPort;
        inputFieldAddres.onEndEdit.AddListener(CheckInput);
    }

    private void CheckInput(string address)
    {

        if (CheckAddres(address))
        {
            GameNetworkManager.singleton.networkAddress = address;
        }
    }

    private bool CheckAddres(string address)
    {
        //Крутая валидация аддреса
        return address != "";
    }
}
