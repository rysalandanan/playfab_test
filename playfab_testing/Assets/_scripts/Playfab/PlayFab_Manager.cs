using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using System;

public class PlayFab_Manager : MonoBehaviour
{
    private int PlayerScore;
    public TextMeshProUGUI MessageText;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_InputField nameInput;

    public GameObject logInPanel;

    public GameObject PlayerObject;

    public void RegisterButton()
    {
        PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
        }, result =>
        {
            MessageText.text = "You have successfully registered";
        }, Error => MessageText.text = "Invalid Email or Password must be more than 5 characters");
    }
    
    public void LogInButton()
    {
        PlayFabClientAPI.LoginWithEmailAddress(new LoginWithEmailAddressRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
        }, result =>
        {
            MessageText.text = "You have successfully log in";
            logInPanel.SetActive(false);
            GetPlayerData(result.PlayFabId);
        }, Error => MessageText.text = "Account canno't be found or check your email and password");
            
    }
    public void ResetPasswordButton()
    {
        PlayFabClientAPI.SendAccountRecoveryEmail(new SendAccountRecoveryEmailRequest
        {
            Email = emailInput.text,
            TitleId = "DA917"
        }, result =>
        {
            MessageText.text = "An password reset link has been sent to your email";
        }, Error => MessageText.text = "Invalid email address");
    }
    public void StoreName()
    {
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = nameInput.text
        }, result =>
        {
            MessageText.text = "Successfuly registered the name!";
        }, Error => MessageText.text = "Name is taken");
    }
    public void SavePlayerData()
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
        {
            Data = new Dictionary<string,string>
            {
                {"Player.x Position", PlayerObject.transform.position.x.ToString()},
                {"Player.y Position", PlayerObject.transform.position.y.ToString()},
            }
        },result =>
        {
            MessageText.text = "DATA UPDATED";
        },Error => MessageText.text ="DATA UPDATE FAILED"); 
    }
    public void GetPlayerData(string myPlayFabId)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest
        {
            PlayFabId = myPlayFabId,
            Keys = null
        }, result =>
        {
            MessageText.text = "Successfully recieved data";
            var posX = float.Parse(result.Data["Player.x Position"].Value);
            var posY = float.Parse(result.Data["Player.y Position"].Value);
            PlayerObject.transform.position = new Vector2(posX, posY); 
        }, Error => MessageText.text = "No data found");
    }
}
