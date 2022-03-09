﻿using System;
using Xunit.Abstractions;
using RestSharp;
using System.Text.Json;
using AutomacaoAltoroJRestAPI.Models;
namespace AutomacaoAltoroJRestAPI.Helpers
{
    public class AccountNoAPIActions
    {
        ITestOutputHelper LoggerOutput;
        public ITestOutputHelper Logger
        {
            get { return LoggerOutput; }
            set { LoggerOutput = value; }
        }
        public AccountNoAPIActions(ITestOutputHelper LoggerOutput)
        {
            this.LoggerOutput = LoggerOutput;
        }
        public GetAccountNo_Response Get_AccountNo(string Token, string AccountId)
        {
            RestClient restClient = new RestClient();
            RestRequest restRequest = new RestRequest(Method.GET);
            IRestResponse restResponse;
            restRequest.AddParameter("Authorization", string.Format("Bearer " + Token), ParameterType.HttpHeader);
            restClient.BaseUrl = new Uri(APIMethods.Account +"/"+ AccountId);
            restResponse = restClient.Execute(restRequest);
            if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                LoggerOutput.WriteLine("Verificação bem sucedida!");
                return JsonSerializer.Deserialize<GetAccountNo_Response>(restResponse.Content);
            }
            else
            {
                LoggerOutput.WriteLine("Verificação Falhou! Estava esperando o cod:" + System.Net.HttpStatusCode.OK + ",mas foi retornado:" + restResponse.StatusCode);
                return null;
            }

        }
    }
}
